using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task <List<Financeiro>> GetByAlunoId(string Registro);
    Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard();
    Task <(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FiltroFinanceiro filtro);
    Task <Financeiro?> GetById(string Registro);
    Task Add(Financeiro financeiro);
    Task Update(Financeiro financeiro);
    Task Delete(string Registro);
}