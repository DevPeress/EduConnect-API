using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<(IEnumerable<Turma>, int TotalRegistro)> GetByFilters(FiltroTurma filtro);
    Task<Turma?> GetLastTurma();
    Task<List<string>> GetTurmasValidasAsync();
    Task<List<string>> GetInformativos();
    Task<Turma?> GetTurmaByIdAsync(string id);
    Task AddTurmaAsync(Turma turma);
    Task UpdateTurmaAsync(Turma turma);
    Task DeleteTurmaAsync(string id);
}
