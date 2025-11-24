namespace EduConnect.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<List<Funcionario>> GetAllAsync();
    Task<Funcionario?> GetByIdAsync(string matricula);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(string matricula);
}
