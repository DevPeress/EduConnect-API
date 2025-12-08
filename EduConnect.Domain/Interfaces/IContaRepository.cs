using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IContaRepository
{
    Task<Conta> GetConta(string email, string senha);
    Task<bool> EmailExistsAsync(string email);
    Task AddContaAsync(Conta conta);
    Task DeleteContaAsync(int id);
}
