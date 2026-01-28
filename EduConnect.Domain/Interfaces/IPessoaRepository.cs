using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IPessoaRepository <T> where T : class
{
    Task<Result<(List<T>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro);
    Task<Result<(List<string>, List<string>)>> GetInformativos();
    Task<Result<T>> GetByIdAsync(string Registro);
    Task<Result<T>> GetLastPessoaAsync();
    Task<Result<bool>> AddAsync(T pessoa);
    Task<Result<bool>> UpdateAsync(T pessoa);
    Task<Result<bool>> DeleteAsync(string Registro);
}
