using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class FuncionarioService(IFuncionarioRepository repo)
{
    private readonly IFuncionarioRepository _funcionarioRepository = repo;

    public async Task<(IEnumerable<Funcionario>, int TotalRegistro)> GetByFilters(FiltroPessoaDTO filtrodto)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano
        };

        return await _funcionarioRepository.GetByFilters(filtro);
    }

    public async Task<(List<string>, List<string>?)> GetInformativos()
    {
        return await _funcionarioRepository.GetInformativos();
    }

    public async Task<Funcionario?> GetFuncionarioByIdAsync(int id)
    {
        return await _funcionarioRepository.GetByIdAsync(id);
    }

    public async Task<Funcionario?> GetLastFuncionarioAsync()
    {
        return await _funcionarioRepository.GetLastPessoaAsync();
    }

    public async Task AddFuncionarioAsync(FuncionarioDTO dto)
    {
        var funcionario = new Funcionario
        {
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
            Turno = dto.Turno,
            Foto = dto.Foto
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
            Turno = dto.Turno,
            Foto = dto.Foto
        };
        await _funcionarioRepository.UpdateAsync(funcionario);
    }

    public async Task DeleteFuncionarioAsync(int id)
    {
        await _funcionarioRepository.DeleteAsync(id);
    }
}
