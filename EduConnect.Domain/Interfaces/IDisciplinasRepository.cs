using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IDisciplinasRepository
{
    Task<Result<(IEnumerable<Disciplinas>, int TotalRegistro)>> GetDisciplinas(FiltroDisciplinas filtro);
    Task<Result<List<Disciplinas>>> GetAllDisciplinas();
    Task<Result<Disciplinas>> GetLastDisciplina();
    Task<Result<Disciplinas>> CreateDisciplina(Disciplinas disciplina);
    Task<Result<bool>> DeleteDisciplina(string Registro);
}
