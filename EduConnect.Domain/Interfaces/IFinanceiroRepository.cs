using EduConnect.Domain.Entities;
using FluentResults;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task<List<Financeiro>> GetByAlunoId(string Registro);
    Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard();
    Task<(List<Financeiro>, int TotalRegistro)> GetByFilters(FiltroFinanceiro filtro);
    Task<Financeiro?> GetById(string Registro);
    Task<bool> Add(Financeiro financeiro);
    Task<bool> Update(Financeiro financeiro);
    Task<bool> Delete(Financeiro financeiro);
}