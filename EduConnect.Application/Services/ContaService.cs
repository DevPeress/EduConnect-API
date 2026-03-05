using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class ContaService(IContaRepository contaRepository)
{
    private readonly IContaRepository _contaRepository = contaRepository;

    public async Task<Result<(bool, int)>> VerifyLogin(string registro, string senha, int maxTentativas)
    {
        var (result, tentativas) = await _contaRepository.VerifyLogin(registro, senha, maxTentativas);

        if (result == null)
            return Result.Fail("Registro não encontrado.");

        return Result.Ok((result.Value, tentativas));
    }

    public async Task<Result<(string nome, string foto)>> GetInfos(string cargo, string registro)
    {
        var (nome, foto) = await _contaRepository.GetInfos(cargo, registro);
        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(foto))
            return Result.Fail("Informações do usuário não encontradas.");

        return (nome, foto);
    }

    public async Task<Result<bool>> ChancePassword(string registro, string senhaNova)
    {
        var conta = await _contaRepository.GetConta(registro);
        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        return await _contaRepository.ChancePassword(conta, senhaNova);
    }

    public async Task<Result<Conta>> GetConta(string registro)
    {
        var conta = await _contaRepository.GetConta(registro);
        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        return conta;
    }

    public async Task<Result<bool>> DeleteContaAsync(string registro)
    {
        var conta = await _contaRepository.GetConta(registro);
        if (conta == null)
            return Result.Fail("Conta não encontrada.");

        return await _contaRepository.DeleteContaAsync(conta);
    }
}
