using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class ProfessorRepository(EduContext context) : IProfessorRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Professor> QueryFiltroProfessor(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Professores.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos" && filtro.Pesquisa.Length > 0)
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Email, pesquisa) ||
                EF.Functions.Like(dados.Telefone, pesquisa) ||
                EF.Functions.Like(dados.Formacao, pesquisa) 
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.Contratacao.Year == anoLetivo);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Salas")
        {
            query = query.Where(dados => dados.Turmas.Any(turma => turma.Nome == filtro.Categoria));
        }

        if (cargo != "Administrador" && cargo != "Funcionário")
        {
            return Enumerable.Empty<Professor>().AsQueryable();
        }

        return query;
    }

    public async Task<Result<(List<Professor>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroProfessor(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        var anos = await _context.Professores
            .Where(a => a.Deletado == false)
            .Select(a => a.Contratacao.Year.ToString())
            .Distinct()
            .ToListAsync();

        var salas = await _context.Professores
            .Where(a => a.Deletado == false)
            .SelectMany(a => a.Turmas.Select(t => t.Nome))
            .Distinct()
            .ToListAsync();

        return (anos, salas);
    }

    public async Task<Result<List<ProfessorDisciplina>>> GetDisciplinasByProfessorAsync(string Registro)
    {
        return await _context.ProfessorDisciplinas.Where(p => p.Professor.Registro == Registro).ToListAsync();
    }

    public async Task<Result<List<Turma>>> GetTurmasByProfessorAsync(string Registro)
    {
        return await _context.Turmas.Where(t => t.ProfessorResponsavel == Registro).ToListAsync();
    }

    public async Task<Result<Professor>> GetByIdAsync(string Registro)
    {
        var professor = await _context.Professores.Where(p => p.Deletado == false).FirstOrDefaultAsync(a => a.Registro == Registro);
        if (professor == null)
            return Result.Fail<Professor>("Professor não encontrado.");

        return professor;
    }

    public async Task<Result<Professor>> GetLastPessoaAsync()
    {
        var professor = await _context.Professores.Where(p => p.Deletado == false)
        .OrderBy(a => a.Registro)
        .LastOrDefaultAsync();

        if (professor == null)
            return Result.Fail<Professor>("Nenhum professor encontrado.");

        return professor; 
    }

    public async Task<Result<bool>> AddAsync(Professor professor, Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        professor.ContaId = conta.Id;
        professor.Conta = conta;

        await _context.Professores.AddAsync(professor);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateAsync(Professor professor)
    {
        var existingProfessor = await GetByIdAsync(professor.Registro);
        if (existingProfessor == null)
            return Result.Fail<bool>("Professor não encontrado.");

        _context.Professores.Update(professor);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteAsync(string Registro)
    {
        var professor = await GetByIdAsync(Registro);
        if (professor.IsFailed)
            return Result.Fail<bool>("Professor não encontrado.");

       
        professor.Value.Deletado = false;
        _context.Professores.Update(professor.Value);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
