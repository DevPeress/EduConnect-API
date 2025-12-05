using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(FiltroPessoas filtro)
    {
        var query = _context.Alunos.AsNoTracking();
        
        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        return query;
    }

    public async Task<(IEnumerable<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro)
    {
        var query = QueryFiltroAluno(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync(); 

        return (result, total);
    }

    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Aluno?> GetLastAlunoAsync()
    {
        return await _context.Alunos
        .OrderBy(a => a.Id)
        .LastOrDefaultAsync();
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

    public async Task DeleteAsync(int id)
    {
        var aluno = await GetByIdAsync(id);
        if (aluno != null)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
