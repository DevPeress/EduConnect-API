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

    public async Task<Funcionario?> GetFuncionarioByIdAsync(string Registro)
    {
        return await _funcionarioRepository.GetByIdAsync(Registro);
    }

    public async Task<Funcionario?> GetLastFuncionarioAsync()
    {
        return await _funcionarioRepository.GetLastPessoaAsync();
    }

    public async Task AddFuncionarioAsync(FuncionarioCadastroDTO funcionarioDTO)
    {
        var funcionario = new Funcionario
        {
            Nome = funcionarioDTO.Nome,
            Email = funcionarioDTO.Email,
            Telefone = funcionarioDTO.Telefone,
            Status = funcionarioDTO.Status,
            Nasc = funcionarioDTO.Nasc,
            Endereco = funcionarioDTO.Endereco,
            Cpf = funcionarioDTO.CPF,
            NomeEmergencia = funcionarioDTO.NomeEmergencia,
            ContatoEmergencia = funcionarioDTO.ContatoEmergencia,
            Registro = funcionarioDTO.Registro,
            Cargo = funcionarioDTO.Cargo,
            DataAdmissao = DateOnly.FromDateTime(DateTime.Now),
            Salario = 0m,
            Departamento = funcionarioDTO.Departamento,
            Supervisor = funcionarioDTO.Supervisor,
            Turno = funcionarioDTO.Turno,
            Foto = funcionarioDTO.Foto
        };
        await _funcionarioRepository.AddAsync(funcionario);
    }

    public async Task UpdateFuncionarioAsync(FuncionarioUpdateDTO funcionarioDTO, DateOnly admissao)
    {
        var funcionario = new Funcionario
        {
            Nome = funcionarioDTO.Nome,
            Email = funcionarioDTO.Email,
            Telefone = funcionarioDTO.Telefone,
            Status = funcionarioDTO.Status,
            Nasc = funcionarioDTO.Nasc,
            Endereco = funcionarioDTO.Endereco,
            Cpf = funcionarioDTO.CPF,
            NomeEmergencia = funcionarioDTO.NomeEmergencia,
            ContatoEmergencia = funcionarioDTO.ContatoEmergencia,
            Registro = funcionarioDTO.Registro,
            Cargo = funcionarioDTO.Cargo,
            DataAdmissao = admissao,
            Salario = funcionarioDTO.Salario,
            Departamento = funcionarioDTO.Departamento,
            Supervisor = funcionarioDTO.Supervisor,
            Turno = funcionarioDTO.Turno,
            Foto = funcionarioDTO.Foto
        };
        await _funcionarioRepository.UpdateAsync(funcionario);
    }

    public async Task DeleteFuncionarioAsync(string Registro)
    {
        await _funcionarioRepository.DeleteAsync(Registro);
    }
}
