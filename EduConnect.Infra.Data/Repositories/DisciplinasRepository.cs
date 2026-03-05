using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class DisciplinasRepository(EduContext context) : IDisciplinasRepository
{
    private readonly EduContext _context = context;
    private IQueryable<Disciplinas> QueryFiltroAluno(FiltroDisciplinas filtro)
    {
        var query = _context.Disciplinas.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos" && filtro.Pesquisa.Length > 0)
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Descricao, pesquisa)
            );
        }

        return query;
    }

    public async Task<(IEnumerable<Disciplinas>, int TotalRegistro)> GetDisciplinas(FiltroDisciplinas filtro)
    {
        var query = QueryFiltroAluno(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(9);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Disciplinas?> GetDisciplinaById(string Registro)
    {
        return await _context.Disciplinas.FirstOrDefaultAsync(d => d.Registro == Registro) ?? null;
    }

    public async Task<List<Disciplinas>> GetAllDisciplinas()
    {
        return await _context.Disciplinas
            .AsNoTracking()
            .Where(d => d.Deletado == false)
            .ToListAsync();
    }

    public async Task<Disciplinas?> GetLastDisciplina()
    {
        return await _context.Disciplinas
            .OrderBy(d => d.Registro)
            .LastOrDefaultAsync() ?? null;
    }

    public async Task<bool> CreateDisciplina(Disciplinas disciplina)
    {
        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteDisciplina(Disciplinas disciplina)
    {
        disciplina.Deletado = true;
        await _context.SaveChangesAsync();
        
        return true;
    }
}
