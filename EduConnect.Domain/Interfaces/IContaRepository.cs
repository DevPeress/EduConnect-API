using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IContaRepository
{
    Task<(bool, int)> VerifyLogin(string registro, string senha, int maxTentativas);
    Task<Conta?> GetConta(string registro);
    Task<bool> EmailExistsAsync(string registro);
    Task<(string nome, string foto)> GetInfos(string cargo, string registro);
    Task<bool> ChancePassword(string registro, string senhaNova);
    Task AddContaAsync(Conta conta);
    Task DeleteContaAsync(int id);
}
