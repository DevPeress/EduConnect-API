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

        var (turmas, total) = await _turmaRepository.GetByFilters(filtro, id, cargo);

        List<TurmaDTO> turmaDTO = [.. turmas.Select(turmas => new TurmaDTO(turmas)
        {
            Registro = turmas.Registro,
            Nome = turmas.Nome,
            Turno = turmas.Turno,
            Professor = turmas.ProfessorResponsavel,
            Horario = $"{turmas.Inicio} - {turmas.Fim}",
            Sala = turmas.Sala,
            Capacidade = turmas.Capacidade,
        })];

        return (turmaDTO, total);
    }

    public async Task<Result<Turma>> GetLastTurma()
    {
        var turma = await _turmaRepository.GetLastTurma();
        if (turma == null)
            return Result.Fail("Nenhuma turma encontrada.");
        
        return turma;
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
        var turma = await _turmaRepository.GetTurmaByIdAsync(id);
        if (turma == null)
            return Result.Fail("Nenhuma turma encontrada.");

        return turma;
    }

    public async Task<Result<bool>> AddTurmaAsync(TurmaCadastroDTO turmaDTO)
    {
        var turmaExisting = await _turmaRepository.GetTurmaByDados(turmaDTO.Registro, turmaDTO.AnoEletivo);
        if (turmaExisting == null)
            return Result.Fail("Já existe uma turma com o mesmo registro e ano letivo.");

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
        var turmaExisting = await _turmaRepository.GetTurmaByDados(turmaDTO.Registro, turmaDTO.AnoEletivo);
        if (turmaExisting == null)
            return Result.Fail("Não existe uma turma com esse registro!");

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
        var turma = await _turmaRepository.GetTurmaByIdAsync(id);
        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return await _turmaRepository.DeleteTurmaAsync(turma);
    }
}
