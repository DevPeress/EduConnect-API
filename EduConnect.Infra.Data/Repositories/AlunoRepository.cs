using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(Filtro filtro)
    {
        var query = _context.Alunos.AsNoTracking().Where(p => p.Deletado == false);
        
        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Sala != null && filtro.Sala != "Todas as Salas")
        {
            query = query.Where(dados => dados.Turma == filtro.Sala);
        }

        return query;
    }

    public async Task<(IEnumerable<Aluno>, int TotalRegistro)> GetByFilters(Filtro filtro)
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
            await Task.Run(() =>
            {
                aluno.Deletado = true;
                _context.Alunos.Update(aluno);
                _context.SaveChanges();
            });
        }
    }
}
