using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface INotasRepository
{
    Task<Result<(List<Notas>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo);
    Task<Result<(List<string>, List<string>)>> GetInformativos();
    Task<Result<Notas>> GetByIdAsync(int Registro);
    Task<Result<bool>> AddAsync(Notas nota);
    Task<Result<bool>> UpdateAsync(Notas nota);
    Task<Result<bool>> DeleteAsync(Notas nota);
}