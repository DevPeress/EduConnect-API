using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<List<Turma>> GetTurmasAsync();
    Task<Turma?> GetTurmaByIdAsync(int id);
    Task AddTurmaAsync(Turma turma);
    Task UpdateTurmaAsync(Turma turma);
    Task DeleteTurmaAsync(int id);
}
