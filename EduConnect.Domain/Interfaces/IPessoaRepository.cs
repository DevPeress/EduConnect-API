using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IPessoaRepository <T> where T : class
{
    Task<(IEnumerable<T>, int TotalRegistro)> GetByFilters(Filtro filtro);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetLastPessoaAsync();
    Task AddAsync(T pessoa);
    Task UpdateAsync(T pessoa);
    Task DeleteAsync(int id);
}
