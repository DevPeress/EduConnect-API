using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<List<Funcionario>> GetAllAsync();
    Task<(IEnumerable<Funcionario>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro);
    Task<Funcionario?> GetByIdAsync(int id);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(int id);
}
