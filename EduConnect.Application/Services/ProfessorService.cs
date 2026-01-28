using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class ProfessorService(IProfessorRepository repo)
{
    private readonly IProfessorRepository _professorRepository = repo;

    public async Task<Result<(List<ProfessorDTO>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var result = await _professorRepository.GetByFilters(filtro);
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        var (professores, total) = result.Value;
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
        return await _professorRepository.GetByIdAsync(Registro);
    }

    public async Task<Result<Professor>> GetLastProfessorAsync()
    {
        return await _professorRepository.GetLastPessoaAsync();
    }

    public async Task<Result<bool>> AddProfessorAsync(ProfessorCadastroDTO ProfessorDTO)
    {
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

        return await _professorRepository.AddAsync(professor);
    }

    public async Task<Result<bool>> UpdateProfessorAsync(ProfessorUpdateDTO ProfessorDTO, DateOnly DataContrato)
    {
        var disciplinas = await _professorRepository.GetDisciplinasByProfessorAsync(ProfessorDTO.Registro);
        if (disciplinas.IsFailed)
            return Result.Fail("Disciplinas do professor não encontradas.");

        ICollection<ProfessorDisciplina> disciplinasDoProfessor = disciplinas != null! ? disciplinas.Value : [];
         
        var turmas = await _professorRepository.GetTurmasByProfessorAsync(ProfessorDTO.Registro);
        if (turmas.IsFailed)
            return Result.Fail("Turmas do professor não encontradas.");

        ICollection<Turma> turmasDoProfessor = turmas != null! ? turmas.Value : [];

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
        return await _professorRepository.DeleteAsync(Registro);
    }
}
