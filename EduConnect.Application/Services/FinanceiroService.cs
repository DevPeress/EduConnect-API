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

    public async Task<List<Financeiro>> GetByAlunoId(int id)
    {
        return await _financeiroRepository.GetByAlunoId(id);
    }

    public async Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard()
    {
        return await _financeiroRepository.GetDashBoard();
    }

    public async Task<(IEnumerable<Financeiro>, int totalRegistro)> GetByFilters(FinanceiroFiltroDTO filtrodto)
    {
        var  filtro = new FinanceiroFiltro
        {
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Data = filtrodto.Data,
        };
        return await _financeiroRepository.GetByFilters(filtro);
    }

    public async Task<Financeiro?> GetById(int id)
    {
        return await _financeiroRepository.GetById(id);
    }

    public async Task AddFinanceiroAsync(FinanceiroDTO dto)
    {
        var financeiro = new Financeiro
        {
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
            Registro = dto.Registro,
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
    public async Task DeleteFinanceiroAsync(int id)
    {
        await _financeiroRepository.Delete(id);
    }
}
