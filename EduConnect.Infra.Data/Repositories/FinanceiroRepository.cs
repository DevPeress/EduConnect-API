using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EduConnect.Infra.Data.Repositories;

public class FinanceiroRepository(EduContext context) : IFinanceiroRepository
{
    private readonly EduContext _context = context;
    private readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
    private IQueryable<Financeiro> QueryFiltroFuncionario(FinanceiroFiltro filtro)
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
                query = query.Where(dados => dados.Pago == false);
            }
            else if (filtro.Status == "Atrasado")
            {
                query = query.Where(dados => dados.Pago == false && dados.DataVencimento < Today);
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
    public async Task<(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FinanceiroFiltro filtro)
    {
        var query = QueryFiltroFuncionario(filtro);
        var total = await query.CountAsync();

        filtro.AlterarLimit(4);
        query = query.Skip(filtro.Offset).Take(4);
        var result = await query.ToListAsync();

        return (result, total);
    }
    public async Task<decimal> GetRecebidos()
    {
        var query = _context.Financeiros.AsNoTracking()
            .Where(dados => dados.Pago == true);
        var dados = await query.ToListAsync();
        decimal total = dados.Sum(p => p.Valor);
        return total;
    }
    public async Task<decimal> GetPendentes()
    {
        var query = _context.Financeiros.AsNoTracking()
            .Where(dados => dados.Pago == false && dados.DataVencimento < Today);
        var dados = await query.ToListAsync();
        decimal total = dados.Sum(p => p.Valor);
        return total;
    }
    public async Task<decimal> GetAtrasados()
    {
        var query = _context.Financeiros.AsNoTracking()
            .Where(dados => dados.Pago == false && dados.DataVencimento > Today);
        var dados = await query.ToListAsync();
        decimal total = dados.Sum(p => p.Valor);
        return total;
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
