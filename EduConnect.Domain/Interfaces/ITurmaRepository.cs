using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface ITurmaRepository
{
    Task<Result<(List<Turma>, int TotalRegistro)>> GetByFilters(FiltroTurma filtro, string id, string cargo);
    Task<Result<Turma>> GetLastTurma();
    Task<Result<List<string>>> GetTurmasValidasAsync();
    Task<Result<List<string>>> GetInformativos();
    Task<Result<Turma>> GetTurmaByIdAsync(string id);
    Task<Result<bool>> AddTurmaAsync(Turma turma, List<string> disciplinas);
    Task<Result<bool>> UpdateTurmaAsync(Turma turma, List<string> disciplinas);
    Task<Result<bool>> DeleteTurmaAsync(string id);
}
