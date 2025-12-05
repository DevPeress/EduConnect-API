using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EduConnect.Infra.Data.Repositories;

public class FinanceiroRepository(EduContext context) : IFinanceiroRepository
{
    private readonly EduContext _context = context;
    private readonly DateOnly today = DateOnly.FromDateTime(DateTime.Today);

    private IQueryable<Financeiro> QueryFiltroFinanceiro(FinanceiroFiltro filtro)
    {
        var query = _context.Financeiros.AsNoTracking();

        if (filtro.Categoria != "Todas as Categorias")
        {
            query = query.Where(dados => dados.Categoria == filtro.Categoria);
        }

        if (filtro.Status != "Todos os Status")
        {
            if (filtro.Status == "Pago")
            {
                query = query.Where(dados => dados.Pago == true);
            }
            else if (filtro.Status == "Pendente")
            {
                query = query.Where(dados => dados.Pago == false && dados.DataVencimento >= today);
            }
            else if (filtro.Status == "Atrasado")
            {
                query = query.Where(dados => dados.Pago == false && dados.DataVencimento < today);
            }
            else if (filtro.Status == "Cancelado")
            {
                query = query.Where(dados => dados.Cancelado == true);
            }
        }

        if (filtro.Data != "Todos os Meses")
        {
            int mesSelecionado = DateTime.ParseExact(
                filtro.Data,
                "MMMM",
                new CultureInfo("pt-BR"),
                DateTimeStyles.None
            ).Month;
            query = query.Where(dados => dados.DataVencimento.Month == mesSelecionado);
        }

        return query;
    }

    public async Task<List<Financeiro>> GetAll()
    {
        return await _context.Financeiros.ToListAsync();
    }

    public async Task<List<Financeiro>> GetByAlunoId(int alunoId)
    {
        return await _context.Financeiros.Where(dados => dados.AlunoId == alunoId).ToListAsync();
    }

    public async Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard()
    {
        var query = _context.Financeiros.AsNoTracking();
        decimal totalRecebido = query.Where(p => p.Pago == true).Sum(p => p.Valor);

        query = query.Where(p => p.Pago == false);

        decimal totalPendente = query.Where(p => p.DataVencimento >= today).AsEnumerable().Sum(p => p.Valor);
        decimal totalAtrasado = query.Where(p => p.DataVencimento < today).AsEnumerable().Sum(p => p.Valor);

        return (totalRecebido, totalPendente, totalAtrasado);
    }

    public async Task<(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FinanceiroFiltro filtro)
    {
        var query = QueryFiltroFinanceiro(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Financeiro?> GetById(int id)
    {
        return await _context.Financeiros.FirstOrDefaultAsync(dados => dados.Registro == id);
    }

    public async Task Add(Financeiro financeiro)
    {
        await _context.Financeiros.AddAsync(financeiro);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Financeiro financeiro)
    {
        _context.Financeiros.Update(financeiro);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var financeiro = await GetById(id);
        if (financeiro != null)
        {
            _context.Financeiros.Remove(financeiro);
            await _context.SaveChangesAsync();
        }
    }
}
