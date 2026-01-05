using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class FinanceiroService(IFinanceiroRepository repo)
{
    private readonly IFinanceiroRepository _financeiroRepository = repo;

    public async Task<List<Financeiro>> GetByAlunoId(string Registro)
    {
        return await _financeiroRepository.GetByAlunoId(Registro);
    }

    public async Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard()
    {
        return await _financeiroRepository.GetDashBoard();
    }

    public async Task<(IEnumerable<Financeiro>, int totalRegistro)> GetByFilters(FiltroFinanceiroDTO filtrodto)
    {
        var  filtro = new FiltroFinanceiro
        {
            Status = filtrodto.Status,
            Categoria = filtrodto.Categoria,
            Meses = filtrodto.Meses,
        };
        return await _financeiroRepository.GetByFilters(filtro);
    }

    public async Task<Financeiro?> GetById(int id)
    {
        return await _financeiroRepository.GetById(id);
    }

    public async Task AddFinanceiroAsync(FinanceiroCadastroDTO FinanceiroDTO)
    {
        var financeiro = new Financeiro
        {
            Registro = FinanceiroDTO.Registro,
            Categoria = FinanceiroDTO.Categoria,
            Metodo = FinanceiroDTO.Metodo,
            Descricao = FinanceiroDTO.Descricao,
            Valor = FinanceiroDTO.Valor,
            DataVencimento = FinanceiroDTO.DataVencimento,
            Pago = FinanceiroDTO.Pago,
            DataPagamento = FinanceiroDTO.DataPagamento,
            Observacoes = FinanceiroDTO.Observacoes,
            AlunoRegistro = FinanceiroDTO.AlunoRegistro
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
