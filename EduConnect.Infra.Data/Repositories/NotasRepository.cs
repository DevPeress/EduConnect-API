using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class NotasRepository(EduContext context) : INotasRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Notas> QueryFiltroNotas(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Notas.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos")
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Aluno.Nome, pesquisa) ||
                EF.Functions.Like(dados.Id.ToString(), pesquisa) 
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.Data.Year == anoLetivo);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Salas")
        {
            query = query.Where(dados => dados.Aluno.Turma!.Nome == filtro.Categoria);
        }

        if (cargo == "Administrador" || cargo == "Funcionário")
        {
            return query;
        }
        else if (cargo == "Professor")
        {
            query = query.Where(dados => dados.ProfessorId.ToString() == id);
            return query;
        }
        else
        {
            query = query.Where(dados => dados.AlunoId.ToString() == id);
            return query;
        }
    }

    public async Task<Result<(List<Notas>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroNotas(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync(); 

        return (result, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        var anos = await _context.Alunos
            .Where(a => a.Deletado == false)
            .Select(a => a.DataMatricula.Year.ToString())
            .Distinct()
            .ToListAsync();
        var salas = await _context.Alunos
            .Where(a => a.Deletado == false)
            .Select(a => a.Turma!.Nome)
            .Distinct()
            .ToListAsync();

        return (anos, salas);
    }

    public async Task<Result<Notas>> GetByIdAsync(int Registro)
    {
        var nota = await _context.Notas.FirstOrDefaultAsync(a => a.Id == Registro);
        if (nota == null)
            return Result.Fail("Nota não encontrado.");

        return nota;
    }

    public async Task<Result<bool>> AddAsync(Notas notaAdd)
    {
        await _context.Notas.AddAsync(notaAdd);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateAsync(Notas notaUpdate)
    {
        _context.Notas.Update(notaUpdate);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteAsync(Notas nota)
    {
        nota.Deletado = true;
        _context.Notas.Update(nota);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
