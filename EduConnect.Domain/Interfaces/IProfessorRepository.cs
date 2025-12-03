using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;
public interface IProfessorRepository
{
    Task<List<Professor>> GetAllAsync();
    Task<Professor?> GetByIdAsync(int id);
    Task<Professor?> GetLastProfessorAsync();
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(int id);
}
