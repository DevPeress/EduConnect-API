using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<List<Funcionario>> GetAllAsync();
    Task<Funcionario?> GetByIdAsync(Guid id);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(Guid id);
}
