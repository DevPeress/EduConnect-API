using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IRegistroRepository
{
    Task<List<Registro>> GetRegistrosAsync();
    Task<List<Registro>> GetLastRegistrosAync();
    Task<Registro?> GetRegistroByIdAsync(Guid id);
    Task AddRegistroAsync(Registro registro);
    Task UpdateRegistroAsync(Registro registro);
    Task DeleteRegistroAsync(Guid id);
}
