using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using FluentResults;
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

    public async Task<Result<(IEnumerable<Disciplinas>, int TotalRegistro)>> GetDisciplinas(FiltroDisciplinas filtro)
    {
        var query = QueryFiltroAluno(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(9);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Result<List<Disciplinas>>> GetAllDisciplinas()
    {
        return await _context.Disciplinas
            .AsNoTracking()
            .Where(d => d.Deletado == false)
            .ToListAsync();
    }

    public async Task<Result<Disciplinas>> GetLastDisciplina()
    {
        var lastDisciplina = await _context.Disciplinas
            .OrderBy(d => d.Registro)
            .LastOrDefaultAsync();
        if (lastDisciplina == null)
            return Result.Fail("Nenhuma disciplina encontrada.");

        return lastDisciplina;
    }

    public async Task<Result<Disciplinas>> CreateDisciplina(Disciplinas disciplina)
    {
        var discplinas = await _context.Disciplinas.AnyAsync(d => d.Registro == disciplina.Registro);
        if (discplinas)
            return Result.Fail("Já existe uma disciplina com esse registro.");

        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();
        return disciplina;
    }

    public async Task<Result<bool>> DeleteDisciplina(string Registro)
    {
        var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Registro == Registro);
        if (disciplina == null)
            return Result.Fail("Disciplina não encontrada.");
        
        disciplina.Deletado = true;
        await _context.SaveChangesAsync();
        
        return Result.Ok(true);
    }
}
