using EduConnect.Application.Common.Auditing;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AuditService(IAuditContext context, IAuditRepository repo)
{
    private readonly IAuditContext _context = context;
    private readonly IAuditRepository _repo = repo;

    public async Task LogAsync(AuditAction action, string entity, string entityId, string detalhes)
    {
        var registro = new Registro
        {
            UserId = _context.UserId,
            UserName = _context.UserName,
            UserRole = _context.UserRole,
            IpAddress = _context.IpAddress,

            Action = action,
            Entity = entity,
            EntityId = entityId,
            Detalhes = detalhes,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(registro);
    }
}
