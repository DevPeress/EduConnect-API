using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(FiltroPessoa filtro)
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
        return query;
    }

    public async Task<Result<(List<Aluno>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro)
    {
        var query = QueryFiltroAluno(filtro);
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

    public async Task<Result<Aluno>> GetByIdAsync(string Registro)
    {
        var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Registro == Registro);
        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        return aluno;
    }

    public async Task<Result<Aluno>> GetLastPessoaAsync()
    {
        var lastAluno = await _context.Alunos
            .OrderBy(a => a.Id)
            .LastOrDefaultAsync();
        if (lastAluno == null)
            return Result.Fail("Nenhum Aluno encontrado.");

        return lastAluno;
    }

    public async Task<Result<bool>> AddAsync(Aluno alunoAdd)
    {
        var aluno = await GetByIdAsync(alunoAdd.Registro);
        if (aluno != null)
            return Result.Fail("Já existe um Aluno com esse Registro!.");

        var conta = new Conta
        {
            Registro = alunoAdd.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };

        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        alunoAdd.ContaId = conta.Id;
        alunoAdd.Conta = conta;

        await _context.Alunos.AddAsync(alunoAdd);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateAsync(Aluno alunoUpdate)
    {
        var aluno = await GetByIdAsync(alunoUpdate.Registro);
        if (aluno == null)
            return Result.Fail("Não foi possível localizar o Aluno para a edição.");

        _context.Alunos.Update(alunoUpdate);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteAsync(string Registro)
    {
        var aluno = await GetByIdAsync(Registro);
        if (aluno.IsFailed) 
            return Result.Fail("Não foi possível localizar o Aluno para a exclusão.");

        aluno.Value.Deletado = true;
        _context.Alunos.Update(aluno.Value);
        await _context.SaveChangesAsync();

        return Result.Ok(true);
    }
}
