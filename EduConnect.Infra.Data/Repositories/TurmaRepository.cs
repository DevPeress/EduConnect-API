using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class TurmaRepository(EduContext context) : ITurmaRepository
{
    private readonly EduContext _context = context;
    public async Task<List<Turma>> GetTurmasAsync()
    {
        return await _context.Turmas.ToListAsync();
    }
    public async Task<Turma?> GetTurmaByIdAsync(int id)
    {
        return await _context.Turmas.FindAsync(id);
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
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
        }
    }
}
