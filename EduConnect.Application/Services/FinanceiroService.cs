using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class FinanceiroService(IFinanceiroRepository repo)
{
    private readonly IFinanceiroRepository _financeiroRepository = repo;
    public async Task<List<Financeiro>> GetAllFinanceirosAsync()
    {
        return await _financeiroRepository.GetAll();
    }
    public async Task<List<Financeiro>> GetByAlunoId(Guid id)
    {
        return await _financeiroRepository.GetByAlunoId(id);
    }
    public async Task<List<Financeiro>> GetByCategoria(string categoria)
    {
        return await _financeiroRepository.GetByCategoria(categoria);
    }
    public async Task<List<Financeiro>> GetByStatus(string status)
    {
        return await _financeiroRepository.GetByStatus(status);
    }
    public async Task<List<Financeiro>> GetByDateRange(DateOnly startDate, DateOnly endDate)
    {
        return await _financeiroRepository.GetByDateRange(startDate, endDate);
    }
    public async Task<Financeiro?> GetById(Guid id)
    {
        return await _financeiroRepository.GetById(id);
    }
    public async Task AddFinanceiroAsync(FinanceiroDTO dto)
    {
        var financeiro = new Financeiro
        {
            Id = Guid.NewGuid(),
            AlunoId = dto.AlunoId,
            Categoria = dto.Categoria,
            Valor = dto.Valor,
            DataVencimento = dto.DataVencimento,
            Pago = dto.Pago,
            DataPagamento = dto.DataPagamento,
            Cancelado = dto.Cancelado
        };
        await _financeiroRepository.Add(financeiro);
    }
    public async Task UpdateFinanceiroAsync(FinanceiroDTO dto)
    {
        var financeiro = new Financeiro
        {
            Id = dto.Id,
            AlunoId = dto.AlunoId,
            Categoria = dto.Categoria,
            Valor = dto.Valor,
            DataVencimento = dto.DataVencimento,
            Pago = dto.Pago,
            DataPagamento = dto.DataPagamento,
            Cancelado = dto.Cancelado
        };
        await _financeiroRepository.Update(financeiro);
    }
    public async Task DeleteFinanceiroAsync(Guid id)
    {
        await _financeiroRepository.Delete(id);
    }
}
