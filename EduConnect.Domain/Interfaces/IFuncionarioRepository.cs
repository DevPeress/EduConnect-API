using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<(IEnumerable<Funcionario>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro);
    Task<Funcionario?> GetByIdAsync(int id);
    Task<Funcionario?> GetLastFuncionarioAsync();
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(int id);
}
