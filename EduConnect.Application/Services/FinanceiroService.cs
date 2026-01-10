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
            Pesquisa = filtrodto.Pesquisa
        };
        return await _financeiroRepository.GetByFilters(filtro);
    }

    public async Task<Financeiro?> GetById(string Registro)
    {
        return await _financeiroRepository.GetById(Registro);
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

    public async Task UpdateFinanceiroAsync(FinanceiroUpdateDTO FinanceiroDTO)
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
            Cancelado = FinanceiroDTO.Cancelado,
            Deletado = FinanceiroDTO.Deletado,
            DataPagamento = FinanceiroDTO.DataPagamento,
            Observacoes = FinanceiroDTO.Observacoes,
            AlunoRegistro = FinanceiroDTO.AlunoRegistro
        };
        await _financeiroRepository.Update(financeiro);
    }
    public async Task DeleteFinanceiroAsync(string Registro)
    {
        await _financeiroRepository.Delete(Registro);
    }
}
