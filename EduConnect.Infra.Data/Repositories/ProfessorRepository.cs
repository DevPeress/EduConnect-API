using EduConnect.Domain;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ProfessorRepository(EduContext context) : IProfessorRepository
{
    private readonly EduContext _context = context;

    public async Task<List<Professor>> GetAllAsync()
    {
        return await _context.Professores.ToListAsync();
    }
    public async Task<Professor?> GetByIdAsync(string matricula)
    {
        return await _context.Professores.FirstOrDefaultAsync(a => a.Registro == matricula);
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
    public async Task DeleteAsync(string matricula)
    {
        var professor = await GetByIdAsync(matricula);
        if (professor != null)
        {
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
