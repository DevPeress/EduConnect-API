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

    private IQueryable<Financeiro> QueryFiltroFinanceiro(FiltroFinanceiro filtro)
    {
        var query = _context.Financeiros.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "" && filtro.Pesquisa.Length > 2)
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.AlunoRegistro, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Descricao, pesquisa) ||
                EF.Functions.Like(dados.Metodo, pesquisa) ||
                EF.Functions.Like(dados.Observacoes!, pesquisa) ||
                EF.Functions.Like(dados.Valor.ToString(), pesquisa) ||
                EF.Functions.Like(dados.Aluno.Nome, pesquisa) ||
                EF.Functions.Like(dados.Aluno.Email, pesquisa)
            );
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Categorias")
        {
            query = query.Where(dados => dados.Categoria == filtro.Categoria);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
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

        if (filtro.Meses != null && filtro.Meses != "Todos os Meses")
        {
            int mesSelecionado = DateTime.ParseExact(
                filtro.Meses,
                "MMMM",
                new CultureInfo("pt-BR"),
                DateTimeStyles.None
            ).Month;
            query = query.Where(dados => dados.DataVencimento.Month == mesSelecionado);
        }

        return query;
    }

    public async Task<List<Financeiro>> GetByAlunoId(string Registro)
    {
        return await _context.Financeiros.Where(dados => dados.Aluno.Registro == Registro && dados.Deletado == false).ToListAsync();
    }

    public async Task<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)> GetDashBoard()
    {
        var query = _context.Financeiros.AsNoTracking().Where(p => p.Deletado == false);
        decimal totalRecebido = query.Where(p => p.Pago == true).Sum(p => p.Valor);

        query = query.Where(p => p.Pago == false);

        decimal totalPendente = query.Where(p => p.DataVencimento >= today).AsEnumerable().Sum(p => p.Valor);
        decimal totalAtrasado = query.Where(p => p.DataVencimento < today).AsEnumerable().Sum(p => p.Valor);

        return (totalRecebido, totalPendente, totalAtrasado);
    }

    public async Task<(IEnumerable<Financeiro>, int TotalRegistro)> GetByFilters(FiltroFinanceiro filtro)
    {
        var query = QueryFiltroFinanceiro(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(5);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Financeiro?> GetById(string Registro)
    {
        return await _context.Financeiros.FirstOrDefaultAsync(dados => dados.Registro == Registro && dados.Deletado == false);
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

    public async Task Delete(string Registro)
    {
        var financeiro = await GetById(Registro);
        if (financeiro != null)
        {
            await Task.Run(() =>
            {
                financeiro.Deletado = true;
                _context.Financeiros.Update(financeiro);
                _context.SaveChanges();
            });
        }
    }
}
