using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using EduConnect.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace EduConnect.Application.Services;

public class AuditoriaService(IAuditRepository repo, IHttpContextAccessor httpContext)
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly IAuditRepository _auditRepository = repo;

    public async Task LogAsync(AuditAction action, string entity, string entityId, string description)
    {
        var context = _httpContext.HttpContext!;
        var user = context.User;

        var audit = new Registro
        {
            UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
            UserName = user.Identity?.Name ?? "Sistema",
            UserRole = user.FindFirst(ClaimTypes.Role)?.Value ?? "N/A",
            Action = action,
            Entity = entity,
            EntityId = entityId,
            Detalhes = description,
            CreatedAt = DateTime.UtcNow,
            IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "-"
        };

        await _auditRepository.AddAsync(audit);
    }
}
