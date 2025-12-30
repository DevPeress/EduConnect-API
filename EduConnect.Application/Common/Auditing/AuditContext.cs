using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EduConnect.Application.Common.Auditing;

public class AuditContext : IAuditContext
{
    private readonly IHttpContextAccessor _http;

    public AuditContext(IHttpContextAccessor http)
    {
        _http = http;
    }

    public void Set(AuditAction action, string entity, object entityId, string? details = null)
    {
        var items = _http.HttpContext?.Items;
        if (items == null) return;

        items[AuditKeys.Action] = action;
        items[AuditKeys.Entity] = entity;
        items[AuditKeys.EntityId] = entityId;
        items[AuditKeys.Details] = details;
    }
}
