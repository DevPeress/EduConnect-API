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
            Page = filtrodto.Page
        };

        var (turmas, total) = await _turmaRepository.GetByFilters(filtro);
        List<TurmaDTO> turmaDTO = turmas.Select(turmas => new TurmaDTO(turmas)
        {
            Registro = turmas.Registro,
            Nome = turmas.Nome,
            Turno = turmas.Turno,
            Inicio = turmas.Inicio,
            Fim = turmas.Fim,
            Sala = turmas.Sala,
            Status = turmas.Status,
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

    public async Task AddTurmaAsync(TurmaDTO turmaDTO)
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
            AnoLetivo = turmaDTO.AnoLetivo,
            DataCriacao = turmaDTO.DataCriacao,
            Status = turmaDTO.Status,
            ProfessorId = turmaDTO.ProfessorID,
            Alunos = turmaDTO.Alunos,
        };
        await _turmaRepository.AddTurmaAsync(turma);
    }

    public async Task UpdateTurmaAsync(TurmaDTO turmaDTO)
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
            AnoLetivo = turmaDTO.AnoLetivo,
            DataCriacao = turmaDTO.DataCriacao,
            Status = turmaDTO.Status,
            ProfessorId = turmaDTO.ProfessorID,
            Alunos = turmaDTO.Alunos,
        };
        await _turmaRepository.UpdateTurmaAsync(turma);
    }

    public async Task DeleteTurmaAsync(string id)
    {
        await _turmaRepository.DeleteTurmaAsync(id);
    }
}
