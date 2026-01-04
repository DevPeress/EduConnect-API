using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;
public interface IProfessorRepository : IPessoaRepository<Professor>
{
    Task<List<Turma>?> GetTurmasByProfessorAsync(string Registro);
    Task<List<ProfessorDisciplina>?> GetDisciplinasByProfessorAsync(string Registro);
}