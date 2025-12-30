using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AuditoriaService(IAuditRepository repo)
{
    private readonly IAuditRepository _auditRepository = repo;

    public async Task LogAsync(Registro registro)
    {
        await _auditRepository.AddAsync(registro);
    }
}
