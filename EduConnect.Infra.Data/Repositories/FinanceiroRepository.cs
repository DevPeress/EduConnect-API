using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EduConnect.Infra.Data.Repositories;

public class FinanceiroRepository(EduContext context) : IFinanceiroRepository
{
    private readonly EduContext _context = context;
    public async Task<List<Financeiro>> GetAll()
    {
        return await _context.Financeiros.ToListAsync();
    }
    public async Task<List<Financeiro>> GetByAlunoId(Guid alunoId)
    {
        return await _context.Financeiros.Where(dados => dados.AlunoId == alunoId).ToListAsync();
    }
    public async Task<List<Financeiro>> GetByFilters(string categoria, string status, string data)
    {
        List<Financeiro> resultados = await GetAll();
        if (status == "Todos os Status" && categoria == "Todas as Categorias" && data == "Todas as Datas")
        {
            return resultados;
        }

        if (categoria != "Todas as Categorias")
        {
            foreach (var dados in resultados.ToList())
            {
                if (dados.Categoria != categoria)
                {
                    resultados.Remove(dados);
                }
            }
        }

        if (status != "Todos os Status")
        {
            foreach (var dados in resultados.ToList())
            {
                if (status == "Pago" && dados.Pago == false)
                {
                    resultados.Remove(dados);
                }
                else if (status == "Pendente" && dados.Pago == true)
                {
                    resultados.Remove(dados);
                }
                else if (status == "Atrasado")
                {
                    var today = DateOnly.FromDateTime(DateTime.Now);
                    if (!(dados.Pago == false && dados.DataVencimento < today))
                    {
                        resultados.Remove(dados);
                    }
                }
                else if (status == "Cancelado" && dados.Cancelado == false)
                {
                    resultados.Remove(dados);
                }
            }
        }

        if (data != "Todas as Datas")
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            foreach (var dados in resultados.ToList())
            {
                int mesSelecionado = DateTime.ParseExact(
                    data,
                    "MMMM",
                    new CultureInfo("pt-BR"),
                    DateTimeStyles.None
                ).Month;
                if (mesSelecionado != dados.DataVencimento.Month)
                {
                    resultados.Remove(dados);
                }
            }
        }

        return resultados;
    }
    public async Task<Financeiro?> GetById(Guid id)
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

    public async Task Delete(Guid id)
    {
        var financeiro = await GetById(id);
        if (financeiro != null)
        {
            _context.Financeiros.Remove(financeiro);
            await _context.SaveChangesAsync();
        }
    }
}
