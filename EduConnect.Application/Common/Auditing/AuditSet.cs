using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EduConnect.Application.Common.Auditing;

public static class AuditSet
{
    public static void SetAudit(this HttpContext context, AuditAction action, string entity, object entityId, string details)
    {
        context.Items[AuditKeys.Action] = action;
        context.Items[AuditKeys.Entity] = entity;
        context.Items[AuditKeys.EntityId] = entityId.ToString();
        context.Items[AuditKeys.Details] = details;
    }
}
