using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IContaRepository
{
    Task<Result<(bool, int)>> VerifyLogin(string registro, string senha, int maxTentativas);
    Task<Result<Conta>> GetConta(string registro);
    Task<Result<bool>> EmailExistsAsync(string registro);
    Task<Result<(string nome, string foto)>> GetInfos(string cargo, string registro);
    Task<Result<bool>> ChancePassword(string registro, string senhaNova);
    Task<Result<bool>> AddContaAsync(Conta conta);
    Task<Result<bool>> DeleteContaAsync(int id);
}
