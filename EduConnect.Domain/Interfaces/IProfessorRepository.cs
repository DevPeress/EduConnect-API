using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;
public interface IProfessorRepository
{
    Task<List<Professor>> GetAllAsync();
    Task<(IEnumerable<Professor>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro);
    Task<Professor?> GetByIdAsync(int id);
    Task<Professor?> GetLastProfessorAsync();
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(int id);
}
