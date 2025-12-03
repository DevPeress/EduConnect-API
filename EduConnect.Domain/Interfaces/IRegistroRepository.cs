using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IRegistroRepository
{
    Task<List<Registro>> GetRegistrosAsync();
    Task<List<Registro>> GetLastRegistrosAync();
    Task<Registro?> GetRegistroByIdAsync(int registro);
    Task AddRegistroAsync(Registro registro);
    Task UpdateRegistroAsync(Registro registro);
    Task DeleteRegistroAsync(int registro);
}
