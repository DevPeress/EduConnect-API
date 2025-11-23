namespace EduConnect.Domain.Interfaces;

public interface IProfessorRepository
{
    Task<List<Professor>> GetAllAsync();
    Task<Professor?> GetByIdAsync(string matricula);
    Task AddAsync(Professor aluno);
    Task UpdateAsync(Professor aluno);
    Task DeleteAsync(string matricula);

}
