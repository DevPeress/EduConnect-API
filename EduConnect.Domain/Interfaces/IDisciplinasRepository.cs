using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDisciplinasRepository
{
    Task<(IEnumerable<Disciplinas>, int TotalRegistro)> GetDisciplinas(FiltroBase filtro);
    Task<Disciplinas?> GetLastDisciplina();
    Task<Disciplinas> CreateDisciplina(Disciplinas disciplina);
}
