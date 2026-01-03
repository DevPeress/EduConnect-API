using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class TurmaRepository(EduContext context) : ITurmaRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Turma> QueryFiltroTurma(FiltroTurma filtro)
    {
        var query = _context.Turmas.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.AnoLetivo.Year == anoLetivo);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Turno != null && filtro.Turno != "Todos os Turnos")
        {
            query = query.Where(dados => dados.Turno == filtro.Turno);
        }

        return query;
    }

    public async Task<(IEnumerable<Turma>, int TotalRegistro)> GetByFilters(FiltroTurma filtro)
    {
        var query = QueryFiltroTurma(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<List<string>> GetInformativos()
    {
        return await _context.Turmas
            .Where(a => a.Deletado == false)
            .Select(a => a.DataCriacao.Year.ToString())
            .Distinct()
            .ToListAsync();
    }

    public async Task<Turma?> GetTurmaByIdAsync(int id)
    {
        return await _context.Turmas.FirstOrDefaultAsync(dados => dados.Registro == id && dados.Deletado == false);
    }

    public async Task AddTurmaAsync(Turma turma)
    {
        await _context.Turmas.AddAsync(turma);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTurmaAsync(Turma turma)
    {
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTurmaAsync(int id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma != null)
        {
            await Task.Run(() =>
            {
                turma.Deletado = true;
                _context.Turmas.Update(turma);
                _context.SaveChanges();
            });
        }
    }
}
