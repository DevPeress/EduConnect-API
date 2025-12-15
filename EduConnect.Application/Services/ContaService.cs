using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class ContaService(IContaRepository contaRepository)
{
    private readonly IContaRepository _contaRepository = contaRepository;
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _contaRepository.EmailExistsAsync(email);
    }
    public async Task AddContaAsync(ContaDTO contadtp)
    {
        var conta = new Conta
        {
            Registro = contadtp.Registro,
            Senha = contadtp.Senha
        };
        await _contaRepository.AddContaAsync(conta);
    }
    public async Task<Conta?> GetConta(string email, string senha)
    {
        return await _contaRepository.GetConta(email, senha);
    }
    public async Task DeleteContaAsync(int id)
    {
        await _contaRepository.DeleteContaAsync(id);
    }
}
