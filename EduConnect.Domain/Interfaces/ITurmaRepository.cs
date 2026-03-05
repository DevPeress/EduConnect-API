using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<(List<Turma>, int TotalRegistro)> GetByFilters(FiltroTurma filtro, string id, string cargo);
    Task<Turma?> GetLastTurma();
    Task<List<string>> GetTurmasValidasAsync();
    Task<List<string>> GetInformativos();
    Task<Turma?> GetTurmaByIdAsync(string id);
    Task<Turma?> GetTurmaByDados(string Registro, string AnoLetivo);
    Task<bool> AddTurmaAsync(Turma turma, List<string> disciplinas);
    Task<bool> UpdateTurmaAsync(Turma turma, List<string> disciplinas);
    Task<bool> DeleteTurmaAsync(Turma turma);
}
