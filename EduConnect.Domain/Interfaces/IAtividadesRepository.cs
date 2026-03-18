using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IAtividadesRepository
{
    Task<Result<(List<Atividades>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo);
    Task<Result<(List<string>, List<string>)>> GetInformativos();
    Task<Result<Atividades>> GetByIdAsync(int Registro);
    Task<Result<bool>> AddAsync(Atividades nota);
    Task<Result<bool>> UpdateAsync(Atividades nota);
    Task<Result<bool>> DeleteAsync(Atividades nota);
}