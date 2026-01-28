using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task<Result<List<Financeiro>>> GetByAlunoId(string Registro);
    Task<Result<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)>> GetDashBoard();
    Task<Result<(List<Financeiro>, int TotalRegistro)>> GetByFilters(FiltroFinanceiro filtro);
    Task<Result<Financeiro>> GetById(string Registro);
    Task<Result<bool>> Add(Financeiro financeiro);
    Task<Result<bool>> Update(Financeiro financeiro);
    Task<Result<bool>> Delete(string Registro);
}