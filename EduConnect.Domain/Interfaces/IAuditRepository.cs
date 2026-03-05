using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IAuditRepository
{
    Task<bool> AddAsync(Registro registro);
}
