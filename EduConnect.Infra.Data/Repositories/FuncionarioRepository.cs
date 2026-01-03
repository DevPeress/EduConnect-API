using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class FuncionarioRepository(EduContext context) : IFuncionarioRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Funcionario> QueryFiltroProfessor(FiltroPessoa filtro)
    {
        var query = _context.Funcionarios.AsNoTracking().Where(p => p.Deletado == false);

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

        return query;
    }

    public async Task<(IEnumerable<Funcionario>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro)
    {
        var query = QueryFiltroProfessor(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<(List<string>, List<string>)> GetInformativos()
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

    public async Task<Funcionario?> GetByIdAsync(int id)
    {
        return await _context.Funcionarios.Where(p => p.Deletado == false).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Funcionario?> GetLastPessoaAsync()
    {
        return await _context.Funcionarios.Where(p => p.Deletado == false)
        .OrderBy(a => a.Registro)
        .LastAsync();
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        var conta = new Conta
        {
            Registro = funcionario.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        funcionario.ContaId = conta.Id;
        funcionario.Conta = conta;

        await _context.Funcionarios.AddAsync(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var funcionario = await GetByIdAsync(id);
        if (funcionario != null)
        {
            await Task.Run(() =>
            {
                funcionario.Deletado = true;
                _context.Funcionarios.Update(funcionario);
                _context.SaveChanges();
            });
        }
    }
}
