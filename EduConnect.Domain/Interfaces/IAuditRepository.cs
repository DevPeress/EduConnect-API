using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IAuditRepository
{
    Task<Result<bool>> AddAsync(Registro registro);
}
