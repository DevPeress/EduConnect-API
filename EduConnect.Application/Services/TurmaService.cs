using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;
public class TurmaService(ITurmaRepository repo)
{
    private readonly ITurmaRepository _turmaRepository = repo;

    public async Task<(List<TurmaDTO>, int TotalRegistro)> GetByFilters(FiltroTurmaDTO filtrodto)
    {
        var filtro = new FiltroTurma
        {
            Turno = filtrodto.Turno,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Page = filtrodto.Page,
            Pesquisa = filtrodto.Pesquisa
        };

        var (turmas, total) = await _turmaRepository.GetByFilters(filtro);
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

    public async Task<Turma?> GetLastTurma()
    {
        return await _turmaRepository.GetLastTurma();
    }

    public async Task<List<string>> GetTurmasValidasAsync()
    {
        return await _turmaRepository.GetTurmasValidasAsync();
    }

    public async Task<List<string>> GetInformativos()
    {
        return await _turmaRepository.GetInformativos();
    }

    public async Task<Turma?> GetTurmaByIdAsync(string id)
    {
        return await _turmaRepository.GetTurmaByIdAsync(id);
    }

    public async Task AddTurmaAsync(TurmaCadastroDTO turmaDTO)
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
        await _turmaRepository.AddTurmaAsync(turma, turmaDTO.Disciplinas);
    }

    public async Task UpdateTurmaAsync(TurmaUpdateDTO turmaDTO)
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
        await _turmaRepository.UpdateTurmaAsync(turma, turmaDTO.TurmaDisciplina);
    }

    public async Task DeleteTurmaAsync(string id)
    {
        await _turmaRepository.DeleteTurmaAsync(id);
    }
}
