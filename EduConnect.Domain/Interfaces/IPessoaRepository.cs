using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IPessoaRepository <T> where T : class
{
    Task<(List<T>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro, string id, string cargo);
    Task<(List<string>, List<string>)> GetInformativos();
    Task<T?> GetByIdAsync(string Registro);
    Task<T?> GetLastPessoaAsync();
    Task<bool> AddAsync(T pessoa, Conta conta);
    Task<bool> UpdateAsync(T pessoa);
    Task<bool> DeleteAsync(T pessoa);
}
