using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class ContaService(IContaRepository contaRepository)
{
    private readonly IContaRepository _contaRepository = contaRepository;

    public async Task<(bool, int)> VerifyLogin(string registro, string senha, int maxTentativas)
    {
        return await _contaRepository.VerifyLogin(registro, senha, maxTentativas);
    }

    public async Task<(string nome, string foto)> GetInfos(string cargo, string registro)
    {
        return await _contaRepository.GetInfos(cargo, registro);
    }

    public async Task<bool> ChancePassword(string registro, string senhaNova)
    {
        return await _contaRepository.ChancePassword(registro, senhaNova);
    }

    public async Task<Conta?> GetConta(string registro)
    {
        return await _contaRepository.GetConta(registro);
    }

    public async Task DeleteContaAsync(int id)
    {
        await _contaRepository.DeleteContaAsync(id);
    }
}
