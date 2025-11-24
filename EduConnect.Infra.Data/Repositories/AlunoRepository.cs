using EduConnect.Domain;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    public async Task<List<Aluno>> GetAllAsync()
    {
        return await _context.Alunos.ToListAsync();
    }
    public async Task<Aluno?> GetByIdAsync(string matricula)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Registro == matricula);
    }
    public async Task<Aluno?> GetLastAlunoAsync()
    {
        return await _context.Alunos
        .OrderBy(a => a.Registro)
        .LastAsync();
    }
    public async Task AddAsync(Aluno aluno)
    {
        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(string matricula)
    {
        var aluno = await GetByIdAsync(matricula);
        if (aluno != null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
