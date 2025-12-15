using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;
public interface IProfessorRepository
{
    Task<(IEnumerable<Professor>, int TotalRegistro)> GetByFilters(Filtro filtro);
    Task<Professor?> GetByIdAsync(int id);
    Task<Professor?> GetLastProfessorAsync();
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(int id);
}
