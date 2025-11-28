using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class FuncionarioService(IFuncionarioRepository repo)
{
    private readonly IFuncionarioRepository _funcionarioRepository = repo;
    public async Task<List<Funcionario>> GetAllFuncionariosAsync()
    {
        return await _funcionarioRepository.GetAllAsync();
    }
    public async Task<Funcionario?> GetFuncionarioByIdAsync(Guid id)
    {
        return await _funcionarioRepository.GetByIdAsync(id);
    }
    public async Task AddFuncionarioAsync(Funcionario funcionario)
    {
        await _funcionarioRepository.AddAsync(funcionario);
    }
    public async Task UpdateFuncionarioAsync(Funcionario funcionario)
    {
        await _funcionarioRepository.UpdateAsync(funcionario);
    }
    public async Task DeleteFuncionarioAsync(Guid id)
    {
        await _funcionarioRepository.DeleteAsync(id);
    }
}
