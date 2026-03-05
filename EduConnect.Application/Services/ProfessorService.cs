using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using FluentResults;

namespace EduConnect.Application.Services;

public class ProfessorService(IProfessorRepository repo)
{
    private readonly IProfessorRepository _professorRepository = repo;

    public async Task<Result<(List<ProfessorDTO>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var (professores, total) = await _professorRepository.GetByFilters(filtro, id, cargo);

        List<ProfessorDTO> professoresDTO = professores.Select(professores => new ProfessorDTO(professores)
        {
            Registro = professores.Registro,
            Nome = professores.Nome,
            Email = professores.Email,
            Telefone = professores.Telefone,
            Status = professores.Status,
            Nasc = professores.Nasc,
            Endereco = professores.Endereco,
            Cpf = professores.Cpf,
            ContatoEmergencia = professores.ContatoEmergencia,
            Foto = professores.Foto
        }).ToList();
       
        return (professoresDTO, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _professorRepository.GetInformativos();
    }

    public async Task<Result<Professor>> GetProfessorByIdAsync(string Registro)
    {
        var professor = await _professorRepository.GetByIdAsync(Registro);
        if (professor == null)
            return Result.Fail("Professor não encontrado.");

        return professor;
    }

    public async Task<Result<Professor>> GetLastProfessorAsync()
    {
        var professor = await _professorRepository.GetLastPessoaAsync();
        if (professor == null)
            return Result.Fail("Registro não encontrado.");

        return professor;
    }

    public async Task<Result<bool>> AddProfessorAsync(ProfessorCadastroDTO ProfessorDTO)
    {
        var existingProfessor = await _professorRepository.GetByIdAsync(ProfessorDTO.Registro);
        if (existingProfessor == null)
            return Result.Fail<bool>("Registro de professor já existe.");

        var conta = new Conta
        {
            Registro = ProfessorDTO.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };

        var professor = new Professor
        {
            Registro = ProfessorDTO.Registro,
            Nome = ProfessorDTO.Nome,
            Email = ProfessorDTO.Email,
            Telefone = ProfessorDTO.Telefone,
            Status = ProfessorDTO.Status,
            Nasc = ProfessorDTO.Nasc,
            Endereco = ProfessorDTO.Endereco,
            Cpf = ProfessorDTO.CPF,
            NomeEmergencia = ProfessorDTO.NomeEmergencia,
            ContatoEmergencia = ProfessorDTO.ContatoEmergencia,
            Turmas = [],
            Foto = ProfessorDTO.Foto,
            Formacao = ProfessorDTO.Formacao,
            Contratacao = DateOnly.FromDateTime(DateTime.Now),
            Salario = 0m
        };

        return await _professorRepository.AddAsync(professor, conta);
    }

    public async Task<Result<bool>> UpdateProfessorAsync(ProfessorUpdateDTO ProfessorDTO, DateOnly DataContrato)
    {
        var disciplinas = await _professorRepository.GetDisciplinasByProfessorAsync(ProfessorDTO.Registro);
        if (disciplinas == null)
            return Result.Fail("Disciplinas do professor não encontradas.");

        ICollection<ProfessorDisciplina> disciplinasDoProfessor = disciplinas != null! ? disciplinas : [];
         
        var turmas = await _professorRepository.GetTurmasByProfessorAsync(ProfessorDTO.Registro);
        if (turmas == null)
            return Result.Fail("Turmas do professor não encontradas.");

        ICollection<Turma> turmasDoProfessor = turmas != null! ? turmas : [];

        var professor = new Professor
        {
            Registro = ProfessorDTO.Registro,
            Nome = ProfessorDTO.Nome,
            Email = ProfessorDTO.Email,
            Telefone = ProfessorDTO.Telefone,
            Status = ProfessorDTO.Status,
            Nasc = ProfessorDTO.Nasc,
            Endereco = ProfessorDTO.Endereco,
            Cpf = ProfessorDTO.CPF,
            NomeEmergencia  = ProfessorDTO.NomeEmergencia,
            ContatoEmergencia = ProfessorDTO.ContatoEmergencia,
            Turmas = turmasDoProfessor,
            ProfessorDisciplinas = disciplinasDoProfessor,
            Foto = ProfessorDTO.Foto,
            Formacao = ProfessorDTO.Formacao,
            Contratacao = DataContrato,
            Salario = ProfessorDTO.Salario
        };

        return await _professorRepository.UpdateAsync(professor);
    }

    public async Task<Result<bool>> DeleteProfessorAsync(string Registro)
    {
        var professor = await _professorRepository.GetByIdAsync(Registro);
        if (professor == null)
            return Result.Fail("Professor não encontrado");

        return await _professorRepository.DeleteAsync(professor);
    }
}
