using EduConnect.Application.Common.Auditing;
using EduConnect.Application.Services;
using EduConnect.Domain.Enums;

namespace EduConnect.MiddleWares;

public class AuditConfiguration(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static readonly HashSet<PathString> RotasBloqueadas =
    [
        new PathString("/api/auth")
    ];

    public async Task InvokeAsync(HttpContext context)
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

        var auditService = context.RequestServices
            .GetRequiredService<AuditService>();

        var auditContext = context.RequestServices
            .GetRequiredService<IAuditContext>();

        if (!auditContext.IsAuthenticated)
            return;

        var action = context.Items.TryGetValue(AuditKeys.Action, out var act)
            ? act as AuditAction?
            : null;

        var entity = context.Items.TryGetValue(AuditKeys.Entity, out var ent)
            ? ent?.ToString()
            : null;

        var entityId = context.Items.TryGetValue(AuditKeys.EntityId, out var entId)
            ? entId?.ToString()
            : null;

        var detalhes = context.Items.TryGetValue(AuditKeys.Details, out var det)
            ? det?.ToString()
            : null;

        if (action is null || entity is null || entityId is null || detalhes is null)
            return;

        try
        {
            await auditService.LogAsync(
                action: action.Value,
                entity: entity,
                entityId: entityId,
                detalhes: detalhes
            );
        } 
        catch (Exception)
        {
            // Log exception if needed
        }
    }
}
