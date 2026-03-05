using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class TurmaRepository(EduContext context) : ITurmaRepository
{
    private readonly EduContext _context = context;
    private readonly string Year = DateTime.Now.Year.ToString();

    private IQueryable<Turma> QueryFiltroTurma(FiltroTurma filtro, string id, string cargo)
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

        if (cargo == "Administrador" || cargo == "Funcionário")
        {
            return query;
        }
        else
        {
            query = query.Where(dados => dados.ProfessorResponsavel == id);
            return query;
        }
    }

    public async Task<(List<Turma>, int TotalRegistro)> GetByFilters(FiltroTurma filtro, string id, string cargo)
    {
        var query = QueryFiltroTurma(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Turma?> GetLastTurma()
    {
        return await _context.Turmas.OrderBy(a => a.Registro).LastOrDefaultAsync();
    }

    public async Task<List<string>> GetTurmasValidasAsync()
    {
        return await _context.Turmas
            .Where(a => a.Deletado == false && a.Status == "Ativa" && a.AnoLetivo == Year)
            .Select(a => a.Nome)
            .ToListAsync();
    }

    public async Task<List<string>> GetInformativos()
    {
        return await _context.Turmas
            .Where(a => a.Deletado == false)
            .Select(a => a.DataCriacao.Year.ToString())
            .Distinct()
            .ToListAsync();
    }

    public async Task<Turma?> GetTurmaByIdAsync(string id)
    {
        var turma = await _context.Turmas.FirstOrDefaultAsync(dados => dados.Registro == id && dados.Deletado == false);
        if (turma == null)
            return null;

        return turma;
    }

    public async Task<Turma?> GetTurmaByDados(string Registro, string AnoLetivo)
    {
        var turma = await _context.Turmas.FirstOrDefaultAsync(dados => dados.Registro == Registro && dados.AnoLetivo == AnoLetivo && dados.Deletado == false);
        if (turma == null)
            return null;

        return turma;
    }

    public async Task<bool> AddTurmaAsync(Turma turma, List<string> disciplinas)
    {
        var disciplinasRegistradas = await _context.TurmaDisciplinas
           .Where(td => td.TurmaRegistro == turma.Registro && turma.AnoLetivo == td.AnoLetivo)
           .ToListAsync();
        if (disciplinasRegistradas.Count > 0)
            return false;

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

        return true;
    }

    public async Task<bool> UpdateTurmaAsync(Turma turma, List<string> disciplinas)
    {
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

        return true;
    }

    public async Task<bool> DeleteTurmaAsync(Turma turma)
    {
        turma.Deletado = true;
        _context.Turmas.Update(turma);
        await _context.SaveChangesAsync();

        return true;
    }
}
