using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class TurmaRepository(EduContext context) : ITurmaRepository
{
    private readonly EduContext _context = context;
    private readonly string Year = DateTime.Now.Year.ToString();

    private IQueryable<Turma> QueryFiltroTurma(FiltroTurma filtro)
    {
        var query = _context.Turmas.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos" && filtro.Pesquisa.Length > 0)
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Sala, pesquisa) ||
                EF.Functions.Like(dados.Capacidade.ToString(), pesquisa) ||
                EF.Functions.Like(dados.ProfessorResponsavel, pesquisa)
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            query = query.Where(dados => dados.AnoLetivo == filtro.Ano);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Turno != null && filtro.Turno != "Todos os Turnos")
        {
            query = query.Where(dados => dados.Turno == filtro.Turno);
        }

        return query;
    }

    public async Task<Result<(List<Turma>, int TotalRegistro)>> GetByFilters(FiltroTurma filtro)
    {
        var query = QueryFiltroTurma(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Result<Turma>> GetLastTurma()
    {
        var turma = await _context.Turmas.OrderBy(a => a.Registro).LastOrDefaultAsync();
        if (turma == null)
            return Result.Fail("Nenhuma turma encontrada.");
      
        return turma;
    }

    public async Task<Result<List<string>>> GetTurmasValidasAsync()
    {
        return await _context.Turmas
            .Where(a => a.Deletado == false && a.Status == "Ativa" && a.AnoLetivo == Year)
            .Select(a => a.Nome)
            .ToListAsync();
    }

    public async Task<Result<List<string>>> GetInformativos()
    {
        return await _context.Turmas
            .Where(a => a.Deletado == false)
            .Select(a => a.DataCriacao.Year.ToString())
            .Distinct()
            .ToListAsync();
    }

    public async Task<Result<Turma>> GetTurmaByIdAsync(string id)
    {
        var turma = await _context.Turmas.FirstOrDefaultAsync(dados => dados.Registro == id && dados.Deletado == false);
        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return turma;
    }

    public async Task<Result<bool>> AddTurmaAsync(Turma turma, List<string> disciplinas)
    {
        var existTurma = await _context.Turmas
            .AnyAsync(t => t.Registro == turma.Registro && t.AnoLetivo == turma.AnoLetivo && t.Deletado == false);
        if (existTurma)
            return Result.Fail("Já existe uma turma cadastrada com este registro para o ano letivo.");

        var disciplinasRegistradas = await _context.TurmaDisciplinas
           .Where(td => td.TurmaRegistro == turma.Registro && turma.AnoLetivo == td.AnoLetivo)
           .ToListAsync();
        if (disciplinasRegistradas.Count > 0)
            return Result.Fail("Disciplinas já cadastradas para esta turma.");

        foreach (var disciplina in disciplinas)
        {
            await _context.TurmaDisciplinas.AddAsync(new TurmaDisciplina
            {
                AnoLetivo = turma.AnoLetivo,
                TurmaRegistro = turma.Registro,
                DisciplinaRegistro = disciplina
            });
        }

        await _context.SaveChangesAsync();

        turma.TurmaDisciplinas = disciplinasRegistradas;

        await _context.Turmas.AddAsync(turma);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateTurmaAsync(Turma turma, List<string> disciplinas)
    {
        var existTurma = await _context.Turmas
            .AnyAsync(t => t.Registro == turma.Registro && t.AnoLetivo == turma.AnoLetivo && t.Deletado == false);
        if (!existTurma)
            return Result.Fail("Não existe uma turma com esse registro!");

        foreach (var disciplina in disciplinas)
        {
            var existingEntry = await _context.TurmaDisciplinas
                .FirstOrDefaultAsync(td => td.TurmaRegistro == turma.Registro &&
                                           td.DisciplinaRegistro == disciplina &&
                                           td.AnoLetivo == turma.AnoLetivo);
            if (existingEntry == null)
            {
                await _context.TurmaDisciplinas.AddAsync(new TurmaDisciplina
                {
                    AnoLetivo = turma.AnoLetivo,
                    TurmaRegistro = turma.Registro,
                    DisciplinaRegistro = disciplina
                });
            }
        }

        var disciplinasRegistradas = await _context.TurmaDisciplinas
           .Where(td => td.TurmaRegistro == turma.Registro && turma.AnoLetivo == td.AnoLetivo)
           .ToListAsync();

        turma.TurmaDisciplinas = disciplinasRegistradas;

        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteTurmaAsync(string id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        turma.Deletado = true;
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
