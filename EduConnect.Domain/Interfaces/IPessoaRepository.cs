using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IPessoaRepository <T> where T : class
{
    Task<(IEnumerable<T>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro);
    Task<(List<string>, List<string>?)> GetInformativos();
    Task<T?> GetByIdAsync(string Registro);
    Task<T?> GetLastPessoaAsync();
    Task AddAsync(T pessoa);
    Task UpdateAsync(T pessoa);
    Task DeleteAsync(string Registro);
}
