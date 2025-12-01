using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

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
    public async Task<List<Financeiro>> GetByCategoria(string categoria)
    {
        return await _context.Financeiros.Where(dados => dados.Categoria == categoria).ToListAsync();
    }
    public async Task<List<Financeiro>> GetByStatus(string status)
    {
        if (status == "Pago")
        {
            return await _context.Financeiros.Where(dados => dados.Pago == true).ToListAsync();
        } 
        else if (status == "Pendente")
        {
            return await _context.Financeiros.Where(dados => dados.Pago == false).ToListAsync();
        }
        else if (status == "Atrasado")
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _context.Financeiros
                .Where(dados => dados.Pago == false && dados.DataVencimento < today)
                .ToListAsync();
        }
        else if (status == "Cancelado")
        {
            return await _context.Financeiros.Where(dados => dados.Cancelado == true).ToListAsync();
        }
        else
        {
            return await GetAll();
        }
    }
    public async Task<List<Financeiro>> GetByDateRange(DateOnly startDate, DateOnly endDate)
    {
        return await _context.Financeiros
            .Where(dados => dados.DataVencimento >= startDate && dados.DataVencimento <= endDate)
            .ToListAsync();
    }
    public async Task<Financeiro?> GetById(Guid id)
    {
        return await _context.Financeiros.FirstOrDefaultAsync(dados => dados.Id == id);
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
