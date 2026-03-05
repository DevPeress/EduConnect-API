using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class FuncionarioRepository(EduContext context) : IFuncionarioRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Funcionario> QueryFiltroProfessor(FiltroPessoa filtro, string id, string cargo)
    {
        var query = _context.Funcionarios.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Pesquisa != "Todos" && filtro.Pesquisa.Length > 0)
        {
            var pesquisa = $"%{filtro.Pesquisa}%";

            query = query.Where(dados =>
                EF.Functions.Like(dados.Nome, pesquisa) ||
                EF.Functions.Like(dados.Registro, pesquisa) ||
                EF.Functions.Like(dados.Email, pesquisa) ||
                EF.Functions.Like(dados.Cargo, pesquisa) ||
                EF.Functions.Like(dados.Supervisor, pesquisa) 
            );
        }

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.Nasc.Year == anoLetivo);
        }

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        if (filtro.Categoria != null && filtro.Categoria != "Todos os Departamentos")
        {
            query = query.Where(dados => dados.Departamento == filtro.Categoria);
        }

        if (cargo != "Administrador" && cargo != "Funcionário")
        {
            return Enumerable.Empty<Funcionario>().AsQueryable();
        }

        return query;
    }

    public async Task<Result<(List<Funcionario>, int TotalRegistro)>> GetByFilters(FiltroPessoa filtro, string id, string cargo)
    {
        var query = QueryFiltroProfessor(filtro, id, cargo);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        var departamentos = await _context.Funcionarios
            .Where(p => p.Deletado == false)
            .Select(f => f.Departamento)
            .Distinct()
            .ToListAsync();
        var statusList = await _context.Funcionarios
            .Where(p => p.Deletado == false)
            .Select(f => f.DataAdmissao.Year.ToString())
            .Distinct()
            .ToListAsync();
        return (departamentos, statusList);
    }

    public async Task<Result<Funcionario>> GetByIdAsync(string Registro)
    {
        var funcionario = await _context.Funcionarios
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Registro == Registro && f.Deletado == false);

        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        return funcionario; 
    }

    public async Task<Result<Funcionario>> GetLastPessoaAsync()
    {
        return await _context.Funcionarios.Where(p => p.Deletado == false)
        .OrderBy(a => a.Registro)
        .LastAsync();
    }

    public async Task<Result<bool>> AddAsync(Funcionario funcionario, Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        funcionario.ContaId = conta.Id;
        funcionario.Conta = conta;

        await _context.Funcionarios.AddAsync(funcionario);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<bool>> UpdateAsync(Funcionario funcionario)
    {
        var funcionarioExistente = await GetByIdAsync(funcionario.Registro);
        if (funcionarioExistente.IsFailed)
            return Result.Fail("Funcionário não encontrado.");

        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result<bool>> DeleteAsync(string Registro)
    {
        var funcionario = await GetByIdAsync(Registro);
        if (funcionario.IsFailed)
            return Result.Fail("Funcionário não encontrado.");

        funcionario.Value.Deletado = true;
        _context.Funcionarios.Update(funcionario.Value);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
}
