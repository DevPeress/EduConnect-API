using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Alunos.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos")
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Email, pesquisa) ||
                EF.Functions.Like(dados.Cpf, pesquisa)
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.DataMatricula.Year == anoLetivo);
        }
        
        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todas as Salas")
        {
            query = query.Where(dados => dados.Turma!.Nome == filtro.Categoria);
        }

        if (cargo == "Administrador" || cargo == "Funcionário")
        {
            return query;
        }
        else
        {
            query = query.Where(dados => dados.Turma!.ProfessorResponsavel == id);
            return query;
        }
    }

    public async Task<(List<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroAluno(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync(); 

        return (result, total);
    }

    public async Task<(List<string>, List<string>)> GetInformativos()
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

    public async Task<Aluno?> GetByIdAsync(string Registro)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Registro == Registro);
        if (aluno == null)
            return null;

        return aluno;
    }

    public async Task<Aluno?> GetLastPessoaAsync()
    {
        return await _context.Alunos
            .OrderBy(a => a.Id)
            .LastOrDefaultAsync();
    }

    public async Task<bool> AddAsync(Aluno alunoAdd, Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        alunoAdd.ContaId = conta.Id;
        alunoAdd.Conta = conta;

        await _context.Alunos.AddAsync(alunoAdd);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Aluno alunoUpdate)
    {
        _context.Alunos.Update(alunoUpdate);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Aluno aluno)
    {
        aluno.Deletado = true;
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();

        return true;
    }
}
