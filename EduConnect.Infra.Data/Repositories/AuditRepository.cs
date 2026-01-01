using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;

namespace EduConnect.Infra.Data.Repositories;

public class AuditRepository(EduContext context) : IAuditRepository
{
    private readonly EduContext _context = context;

    public async Task AddAsync(Registro registro)
    {
        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();
    }
}
