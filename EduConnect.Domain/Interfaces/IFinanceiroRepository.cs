using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task <List<Financeiro>> GetByAlunoId(int alunoId);
    Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard();
    Task <(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FinanceiroFiltro filtro);
    Task <Financeiro?> GetById(int id);
    Task Add(Financeiro financeiro);
    Task Update(Financeiro financeiro);
    Task Delete(int id);
}