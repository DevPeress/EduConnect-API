using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDisciplinasRepository
{
    Task<(IEnumerable<Disciplinas>, int TotalRegistro)> GetDisciplinas(FiltroDisciplinas filtro);
    Task<List<Disciplinas>> GetAllDisciplinas();
    Task<Disciplinas?> GetDisciplinaById(string Registro);
    Task<Disciplinas?> GetLastDisciplina();
    Task<bool> CreateDisciplina(Disciplinas disciplina);
    Task<bool> DeleteDisciplina(Disciplinas disciplina);
}
