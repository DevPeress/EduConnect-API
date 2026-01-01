using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;
public class TurmaService(ITurmaRepository repo)
{
    private readonly ITurmaRepository _turmaRepository = repo;

    public async Task<(List<TurmaDTO>, int TotalRegistro)> GetByFilters(FiltroDTO filtrodto)
    {
        var filtro = new Filtro
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Turno = filtrodto.Turno
        };

        var (turmas, total) = await _turmaRepository.GetByFilters(filtro);
        List<TurmaDTO> turmaDTO = turmas.Select(turmas => new TurmaDTO(turmas)
        {
            Registro = turmas.Registro,
            Nome = turmas.Nome,
            Turno = turmas.Turno,
            Horario = turmas.Horario,
            Status = turmas.Status,
        }).ToList();

        return (turmaDTO, total);
    }

    public async Task<Turma?> GetTurmaByIdAsync(int id)
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
            Horario = turmaDTO.Horario,
            Capacidade = turmaDTO.Capacidade,
            AnoLetivo = turmaDTO.AnoLetivo,
            DataCriacao = turmaDTO.DataCriacao,
            Status = turmaDTO.Status,
            ProfessorId = turmaDTO.ProfessorID,
            Alunos = turmaDTO.Alunos,
            DisciplinaID = turmaDTO.DisciplinaID,
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
            Horario = turmaDTO.Horario,
            Capacidade = turmaDTO.Capacidade,
            AnoLetivo = turmaDTO.AnoLetivo,
            DataCriacao = turmaDTO.DataCriacao,
            Status = turmaDTO.Status,
            ProfessorId = turmaDTO.ProfessorID,
            Alunos = turmaDTO.Alunos,
            DisciplinaID = turmaDTO.DisciplinaID,
        };
        await _turmaRepository.UpdateTurmaAsync(turma);
    }

    public async Task DeleteTurmaAsync(int id)
    {
        await _turmaRepository.DeleteTurmaAsync(id);
    }
}
