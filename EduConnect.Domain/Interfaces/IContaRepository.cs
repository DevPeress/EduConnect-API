using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IContaRepository
{
    Task<Conta?> GetConta(string registro, string senha);
    Task<bool> EmailExistsAsync(string registro);
    Task AddContaAsync(Conta conta);
    Task DeleteContaAsync(int id);
}
