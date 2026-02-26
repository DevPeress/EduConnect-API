using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;
public class TurmaService(ITurmaRepository repo)
{
    private readonly ITurmaRepository _turmaRepository = repo;

    public async Task<Result<(List<TurmaDTO>, int TotalRegistro)>> GetByFilters(FiltroTurmaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroTurma
        {
            Turno = filtrodto.Turno,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Page = filtrodto.Page,
            Pesquisa = filtrodto.Pesquisa
        };

        var result = await _turmaRepository.GetByFilters(filtro, id, cargo);
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        var (turmas, total) = result.Value;
        List<TurmaDTO> turmaDTO = turmas.Select(turmas => new TurmaDTO(turmas)
        {
            Registro = turmas.Registro,
            Nome = turmas.Nome,
            Turno = turmas.Turno,
            Professor = turmas.ProfessorResponsavel,
            Horario = $"{turmas.Inicio} - {turmas.Fim}",
            Sala = turmas.Sala,
            Capacidade = turmas.Capacidade,
        }).ToList();

        return (turmaDTO, total);
    }

    public async Task<Result<Turma>> GetLastTurma()
    {
        return await _turmaRepository.GetLastTurma();
    }

    public async Task<Result<List<string>>> GetTurmasValidasAsync()
    {
        return await _turmaRepository.GetTurmasValidasAsync();
    }

    public async Task<Result<List<string>>> GetInformativos()
    {
        return await _turmaRepository.GetInformativos();
    }

    public async Task<Result<Turma>> GetTurmaByIdAsync(string id)
    {
        return await _turmaRepository.GetTurmaByIdAsync(id);
    }

    public async Task<Result<bool>> AddTurmaAsync(TurmaCadastroDTO turmaDTO)
    {
        var turma = new Turma
        {
            Registro = turmaDTO.Registro,
            Nome = turmaDTO.Nome,
            Turno = turmaDTO.Turno,
            Inicio = turmaDTO.Inicio,
            Fim = turmaDTO.Fim,
            Sala = turmaDTO.Sala,
            Capacidade = turmaDTO.Capacidade,
            AnoLetivo = turmaDTO.AnoEletivo,
            DataCriacao = DateOnly.FromDateTime(DateTime.Now),
            Status = turmaDTO.Status,
            ProfessorResponsavel = turmaDTO.ProfessorResponsavel,
            Alunos = [],
            TurmaDisciplinas = [],
            Dias = turmaDTO.Dias,
            Deletado = false,
        };

        return await _turmaRepository.AddTurmaAsync(turma, turmaDTO.Disciplinas);
    }

    public async Task<Result<bool>> UpdateTurmaAsync(TurmaUpdateDTO turmaDTO)
    {
        var turma = new Turma
        {
            Registro = turmaDTO.Registro,
            Nome = turmaDTO.Nome,
            Turno = turmaDTO.Turno,
            Inicio = turmaDTO.Inicio,
            Fim = turmaDTO.Fim,
            Sala = turmaDTO.Sala,
            Capacidade = turmaDTO.Capacidade,
            AnoLetivo = turmaDTO.AnoEletivo,
            DataCriacao = DateOnly.FromDateTime(DateTime.Now),
            Status = turmaDTO.Status,
            ProfessorResponsavel = turmaDTO.ProfessorResponsavel,
            Alunos = turmaDTO.Alunos,
            Dias = turmaDTO.Dias,
            TurmaDisciplinas = [],
            Deletado = false,
        };

        return await _turmaRepository.UpdateTurmaAsync(turma, turmaDTO.TurmaDisciplina);
    }

    public async Task<Result<bool>> DeleteTurmaAsync(string id)
    {
        return await _turmaRepository.DeleteTurmaAsync(id);
    }
}
