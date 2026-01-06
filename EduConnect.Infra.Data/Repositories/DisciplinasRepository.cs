using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class DisciplinasRepository(EduContext context) : IDisciplinasRepository
{
    private readonly EduContext _context = context;

    public async Task<List<Disciplinas>> GetAllDisciplinas()
    {
        return await _context.Disciplinas
            .Where(d => d.Deletado == false)
            .ToListAsync();
    }

    public async Task<Disciplinas?> GetLastDisciplina()
    {
        return await _context.Disciplinas
            .OrderBy(d => d.Registro)
            .LastOrDefaultAsync();
    }

    public async Task<Disciplinas> CreateDisciplina(Disciplinas disciplina)
    {
        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();
        return disciplina;
    }
}
