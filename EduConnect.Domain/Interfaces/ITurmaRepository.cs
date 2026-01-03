using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<(IEnumerable<Turma>, int TotalRegistro)> GetByFilters(FiltroTurma filtro);
    Task<List<string>> GetInformativos();
    Task<Turma?> GetTurmaByIdAsync(int id);
    Task AddTurmaAsync(Turma turma);
    Task UpdateTurmaAsync(Turma turma);
    Task DeleteTurmaAsync(int id);
}
