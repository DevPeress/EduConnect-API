using EduConnect.Domain.Entities;
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
    public async Task<Professor?> GetByIdAsync(Guid id)
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
    public async Task DeleteAsync(Guid id)
    {
        var professor = await GetByIdAsync(id);
        if (professor != null)
        {
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
