using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IAuditRepository
{
    Task AddAsync(Registro registro);
}
