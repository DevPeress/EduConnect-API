using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;
public interface IProfessorRepository : IPessoaRepository<Professor>
{
    Task<Result<List<Turma>>> GetTurmasByProfessorAsync(string Registro);
    Task<Result<List<ProfessorDisciplina>>> GetDisciplinasByProfessorAsync(string Registro);
}