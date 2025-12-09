using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ProfessorRepository(EduContext context) : IProfessorRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Professor> QueryFiltroProfessor(FiltroPessoas filtro)
    {
        var query = _context.Professores.AsNoTracking();

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        return query;
    }

    public async Task<(IEnumerable<Professor>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro)
    {
        var query = QueryFiltroProfessor(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Professor?> GetByIdAsync(int id)
    {
        return await _context.Professores.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Professor?> GetLastProfessorAsync()
    {
        return await _context.Professores
        .OrderBy(a => a.Registro)
        .LastAsync();
    }

    public async Task AddAsync(Professor professor)
    {
        await _context.Professores.AddAsync(professor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Professor professor)
    {
        _context.Professores.Update(professor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var professor = await GetByIdAsync(id);
        if (professor != null)
        {
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
