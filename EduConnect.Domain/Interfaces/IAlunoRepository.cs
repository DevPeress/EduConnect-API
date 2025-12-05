using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<List<Aluno>> GetAllAsync();
    Task<(IEnumerable<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro);
    Task<Aluno?> GetByIdAsync(int id);
    Task<Aluno?> GetLastAlunoAsync();
    Task AddAsync(Aluno aluno);
    Task UpdateAsync(Aluno aluno);
    Task DeleteAsync(int id);

}
