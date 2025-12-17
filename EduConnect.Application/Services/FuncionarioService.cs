using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class FuncionarioService(IFuncionarioRepository repo)
{
    private readonly IFuncionarioRepository _funcionarioRepository = repo;

    public async Task<(List<FuncionarioDTO>, int TotalRegistro)> GetByFilters(FiltroDTO filtrodto)
    {
        var filtro = new Filtro
        {
            Page = filtrodto.Page,
            Status = filtrodto.Status
        };

        var (funcionarios, totalRegistro) = await _funcionarioRepository.GetByFilters(filtro);
        List<FuncionarioDTO> funcionarioDTO = funcionarios.Select(f => new FuncionarioDTO(f)
        {
            Id = f.Id,
            Nome = f.Nome,
            Email = f.Email,
            Telefone = f.Telefone,
            Status = f.Status,
            Nasc = f.Nasc,
            Endereco = f.Endereco,
            Cpf = f.Cpf,
            ContatoEmergencia = f.ContatoEmergencia,
            Registro = f.Registro,
            Foto = f.Foto
        }).ToList();

        return (funcionarioDTO, totalRegistro);
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
