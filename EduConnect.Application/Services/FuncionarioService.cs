using EduConnect.Application.DTO;
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
    public async Task AddFuncionarioAsync(FuncionarioDTO dto)
    {
        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Cargo = dto.Cargo,
            DataAdmissao = dto.DataAdmissao,
            Salario = dto.Salario,
            Departamento = dto.Departamento,
            Supervisor = dto.Supervisor,
            Turno = dto.Turno
        };
        await _funcionarioRepository.AddAsync(funcionario);
    }
    public async Task UpdateFuncionarioAsync(FuncionarioDTO dto)
    {
        var funcionario = new Funcionario
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Cargo = dto.Cargo,
            DataAdmissao = dto.DataAdmissao,
            Salario = dto.Salario,
            Departamento = dto.Departamento,
            Supervisor = dto.Supervisor,
            Turno = dto.Turno
        };
        await _funcionarioRepository.UpdateAsync(funcionario);
    }
    public async Task DeleteFuncionarioAsync(Guid id)
    {
        await _funcionarioRepository.DeleteAsync(id);
    }
}
