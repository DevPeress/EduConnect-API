using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class ContaService(IContaRepository contaRepository)
{
    private readonly IContaRepository _contaRepository = contaRepository;

    public async Task<Result<(bool, int)>> VerifyLogin(string registro, string senha, int maxTentativas)
    {
        return await _contaRepository.VerifyLogin(registro, senha, maxTentativas);
    }

    public async Task<Result<(string nome, string foto)>> GetInfos(string cargo, string registro)
    {
        return await _contaRepository.GetInfos(cargo, registro);
    }

    public async Task<Result<bool>> ChancePassword(string registro, string senhaNova)
    {
        return await _contaRepository.ChancePassword(registro, senhaNova);
    }

    public async Task<Result<Conta>> GetConta(string registro)
    {
        return await _contaRepository.GetConta(registro);
    }

    public async Task<Result<bool>> DeleteContaAsync(int id)
    {
        return await _contaRepository.DeleteContaAsync(id);
    }
}
