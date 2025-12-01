using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFinanceiroRepository
{
    Task<List<Financeiro>> GetAll();
    Task <List<Financeiro>> GetByAlunoId(Guid alunoId);
    Task <List<Financeiro>> GetByCategoria(string categoria);
    Task <List<Financeiro>> GetByStatus(string status);
    Task <List<Financeiro>> GetByDateRange(DateOnly startDate, DateOnly endDate);
    Task <Financeiro?> GetById(Guid id);
    Task Add(Financeiro financeiro);
    Task Update(Financeiro financeiro);
    Task Delete(Guid id);
}
