using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using FluentResults;

namespace EduConnect.Application.Services;

public class FuncionarioService(IFuncionarioRepository repo)
{
    private readonly IFuncionarioRepository _funcionarioRepository = repo;

    public async Task<Result<(List<Funcionario>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        return await _funcionarioRepository.GetByFilters(filtro, id, cargo);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _funcionarioRepository.GetInformativos();
    }

    public async Task<Result<Funcionario>> GetFuncionarioByIdAsync(string Registro)
    {
        return await _funcionarioRepository.GetByIdAsync(Registro);
    }

    public async Task<Result<Funcionario>> GetLastFuncionarioAsync()
    {
        return await _funcionarioRepository.GetLastPessoaAsync();
    }

    public async Task<Result<bool>> AddFuncionarioAsync(FuncionarioCadastroDTO funcionarioDTO)
    {
        var alunoExiting = await _funcionarioRepository.GetByIdAsync(funcionarioDTO.Registro);
        if (alunoExiting != null)
            return Result.Fail("Já existe um Funcionário com esse Registro!.");

        var conta = new Conta
        {
            Registro = funcionarioDTO.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };

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

        return await _funcionarioRepository.AddAsync(funcionario, conta);
    }

    public async Task<Result<bool>> UpdateFuncionarioAsync(FuncionarioUpdateDTO funcionarioDTO, DateOnly admissao)
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

        return await _funcionarioRepository.UpdateAsync(funcionario);
    }

    public async Task<Result<bool>> DeleteFuncionarioAsync(string Registro)
    {
        return await _funcionarioRepository.DeleteAsync(Registro);
    }
}
