using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;
public class TurmaService(ITurmaRepository repo)
{
    private readonly ITurmaRepository _turmaRepository = repo;
    public async Task<List<Turma>> GetAllTurmasAsync()
    {
        return await _turmaRepository.GetTurmasAsync();
    }
    public async Task<Turma?> GetTurmaByIdAsync(int id)
    {
        return await _turmaRepository.GetTurmaByIdAsync(id);
    }
    public async Task AddTurmaAsync(TurmaDTO turmadto)
    {
        var turma = new Turma
        {
            Nome = turmadto.Nome,
            Turno = turmadto.Turno,
            ProfessorID = turmadto.ProfessorID,
            Alunos = turmadto.Alunos,
            SalaID = turmadto.SalaID,
            DisciplinaID = turmadto.DisciplinaID,
            Horario = turmadto.Horario,
            Capacidade = turmadto.Capacidade,
            AnoLetivo = turmadto.AnoLetivo
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
            ProfessorID = turmaDTO.ProfessorID,
            Alunos = turmaDTO.Alunos,
            SalaID = turmaDTO.SalaID,
            DisciplinaID = turmaDTO.DisciplinaID,
            Horario = turmaDTO.Horario,
            Capacidade = turmaDTO.Capacidade,
            AnoLetivo = turmaDTO.AnoLetivo
        };
        await _turmaRepository.UpdateTurmaAsync(turma);
    }
    public async Task DeleteTurmaAsync(int id)
    {
        await _turmaRepository.DeleteTurmaAsync(id);
    }
}
