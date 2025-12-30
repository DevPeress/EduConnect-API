using EduConnect.Application.Common.Auditing;
using EduConnect.Application.Services;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using System.Security.Claims;

namespace EduConnect.MiddleWares;

public class AuditoriaConfiguration(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static readonly HashSet<PathString> RotasBloqueadas =
    [
        new PathString("/api/auth/login"),
        new PathString("/api/auth/refresh-token")
    ];

    public async Task InvokeAsync(HttpContext context, AuditoriaService auditoriaService)
    {
        await _next(context);

        if (RotasBloqueadas.Any(r =>
            context.Request.Path.StartsWithSegments(r)))
            return;

        var method = context.Request.Method;
        if (method is not ("POST" or "PUT" or "DELETE"))
            return;

        if (context.Response.StatusCode >= 400)
            return;

        if (!context.User.Identity?.IsAuthenticated ?? true)
            return;


        var user = context.User;
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        var userName = user.Identity?.Name;
        var userRole = user.FindFirst(ClaimTypes.Role);

        if (userIdClaim == null || userName == null || userRole == null)
            return;

        var action = context.Items.TryGetValue(AuditKeys.Action, out var act) && act is AuditAction auditAction
            ? auditAction
            : 0;

        var entity = context.Items.TryGetValue(AuditKeys.Entity, out var ent)
            ? ent?.ToString()
            : null;

        var entityId = context.Items.TryGetValue(AuditKeys.EntityId, out var entId)
            ? entId?.ToString()
            : null;

        var detalhes = context.Items.TryGetValue(AuditKeys.Details, out var det)
            ? det?.ToString()
            : null;

        if (entity == null || entityId == null || detalhes == null)
            return;

        var audit = new Registro
        {
            UserId = userIdClaim.Value,
            UserName = userName,
            UserRole = userRole.Value,
            Action = action,
            Entity = entity,
            EntityId = entityId,
            Detalhes = detalhes,

            CreatedAt = DateTime.UtcNow,
            IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "-"
        };

        try
        {
            await auditoriaService.LogAsync(audit);
        } 
        catch (Exception)
        {
            // Log exception if needed
        }
    }
}
