using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EduConnect.Application.Services;

public class AuditoriaService(IAuditRepository repo, IHttpContextAccessor httpContext)
{
    private readonly IAuditRepository _auditRepository = repo;

    public async Task LogAsync(Registro registro)
    {
        await _auditRepository.AddAsync(registro);
    }
}
