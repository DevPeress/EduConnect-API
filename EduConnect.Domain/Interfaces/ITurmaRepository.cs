using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<List<Turma>> GetTurmasAsync();
    Task<Turma?> GetTurmaByIdAsync(Guid id);
    Task AddTurmaAsync(Turma turma);
    Task UpdateTurmaAsync(Turma turma);
    Task DeleteTurmaAsync(Guid id);
}
