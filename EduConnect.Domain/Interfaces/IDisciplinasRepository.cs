using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDisciplinasRepository
{
    Task<List<Disciplinas>> GetAllDisciplinas();
}
