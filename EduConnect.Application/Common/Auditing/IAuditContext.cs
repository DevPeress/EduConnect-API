using EduConnect.Domain.Enums;

namespace EduConnect.Application.Common.Auditing;

public interface IAuditContext
{
    void Set(AuditAction action, string entity, object entityId, string? details = null);
}
