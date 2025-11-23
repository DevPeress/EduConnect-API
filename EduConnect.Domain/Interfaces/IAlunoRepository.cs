namespace EduConnect.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<List<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(string matricula);
    Task AddAsync(Aluno aluno);
    Task UpdateAsync(Aluno aluno);
    Task DeleteAsync(string matricula);

}
