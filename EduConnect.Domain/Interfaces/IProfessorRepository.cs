namespace EduConnect.Domain.Interfaces;

public interface IProfessorRepository
{
    Task<List<Professor>> GetAllAsync();
    Task<Professor?> GetByIdAsync(string matricula);
    Task<Professor?> GetLastProfessorAsync();
    Task AddAsync(Professor professor);
    Task UpdateAsync(Professor professor);
    Task DeleteAsync(string matricula);
}
