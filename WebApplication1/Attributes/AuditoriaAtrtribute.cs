using EduConnect.Domain.Enums;

namespace EduConnect.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class AuditAttribute(AuditAction action, string entity) : Attribute
{
    public AuditAction Action { get; } = action;
    public string Entity { get; } = entity;
}
