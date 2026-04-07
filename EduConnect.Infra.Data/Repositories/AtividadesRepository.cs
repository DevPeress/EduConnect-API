
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AtividadesRepository(EduContext context) : IAtividadesRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Atividades> QueryFiltroAtividades(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Atividades.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos")
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Id.ToString(), pesquisa) 
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.Data.Year == anoLetivo);
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
            var turmaAluno = _context.Alunos.AsNoTracking().Where(a => a.Id.ToString() == id).FirstOrDefault();
            query = query.Where(dados => dados.TurmaId == turmaAluno!.Registro);
            return query;
        }
    }

    public async Task<Result<(List<Atividades>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroAtividades(filtro, id, cargo);
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

    public async Task<Result<Atividades>> GetByIdAsync(int Registro)
    {
        var atividades = await _context.Atividades.FirstOrDefaultAsync(a => a.Id == Registro);
        if (atividades == null)
            return Result.Fail("Atividade não encontrado.");

        return atividades;
    }

    public async Task<Result<bool>> AddAsync(Atividades atividadeAdd)
    {
        await _context.Atividades.AddAsync(atividadeAdd);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateAsync(Atividades atividadeUpdate)
    {
        _context.Atividades.Update(atividadeUpdate);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteAsync(Atividades atividadeRemove)
    {
        atividadeRemove.Deletado = true;
        _context.Atividades.Update(atividadeRemove);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
