using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using FluentResults;

namespace EduConnect.Infra.Data.Repositories;

public class AuditRepository(EduContext context) : IAuditRepository
{
    private readonly EduContext _context = context;

    public async Task<Result<bool>> AddAsync(Registro registro)
    {
        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
