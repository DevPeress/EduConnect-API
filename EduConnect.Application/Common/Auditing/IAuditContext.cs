using EduConnect.Domain.Enums;

namespace EduConnect.Application.Common.Auditing;

public interface IAuditContext
{
    bool IsAuthenticated { get; }
    string UserId { get; }
    string UserName { get; }
    string UserRole { get; }
    string IpAddress { get; }

}
