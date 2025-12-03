using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task<List<Financeiro>> GetAll();
    Task <List<Financeiro>> GetByAlunoId(Guid alunoId);
    Task <(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FinanceiroFiltro filtro);
    Task <Financeiro?> GetById(Guid id);
    Task Add(Financeiro financeiro);
    Task Update(Financeiro financeiro);
    Task Delete(Guid id);
}