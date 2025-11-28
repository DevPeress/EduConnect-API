using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;
public interface IAlunoRepository
{
    Task<List<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(Guid id);
    Task<Aluno?> GetLastAlunoAsync();
    Task AddAsync(Aluno aluno);
    Task UpdateAsync(Aluno aluno);
    Task DeleteAsync(Guid id);

}
