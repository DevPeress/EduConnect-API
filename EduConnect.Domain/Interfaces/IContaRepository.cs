using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IContaRepository
{
    Task<(bool?, int)> VerifyLogin(string registro, string senha, int maxTentativas);
    Task<Conta?> GetConta(string registro);
    Task<bool> EmailExistsAsync(string registro);
    Task<(string? nome, string? foto)> GetInfos(string cargo, string registro);
    Task<bool> ChancePassword(Conta conta, string senhaNova);
    Task<bool> AddContaAsync(Conta conta);
    Task<bool> DeleteContaAsync(Conta conta);
}
