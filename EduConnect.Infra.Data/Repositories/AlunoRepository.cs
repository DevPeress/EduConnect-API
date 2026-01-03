using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class AlunoRepository(EduContext context) : IAlunoRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Aluno> QueryFiltroAluno(FiltroPessoa filtro)
    {
        var query = _context.Alunos.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Ano != null && filtro.Ano != "Todos os Anos")
        {
            int anoLetivo = int.Parse(filtro.Ano);
            query = query.Where(dados => dados.DataMatricula.Year == anoLetivo);
        }
        
        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        return query;
    }

    public async Task<(IEnumerable<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoa filtro)
    {
        var query = QueryFiltroAluno(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync(); 

        return (result, total);
    }

    public async Task<(List<string>, List<string>?)> GetInformativos()
    {
        var anos = await _context.Alunos
            .Where(a => a.Deletado == false)
            .Select(a => a.DataMatricula.Year.ToString())
            .Distinct()
            .ToListAsync();
        return (anos, []);
    }

    public async Task<Aluno?> GetByIdAsync(int id)
    {
        return await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Aluno?> GetLastPessoaAsync()
    {
        return await _context.Alunos
        .OrderBy(a => a.Id)
        .LastOrDefaultAsync();
    }

    public async Task AddAsync(Aluno aluno)
    {
        var conta = new Conta
        {
            Registro = aluno.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();

        aluno.ContaId = conta.Id;
        aluno.Conta = conta;

        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aluno aluno)
    {
        _context.Alunos.Update(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var aluno = await GetByIdAsync(id);
        if (aluno != null)
        {
            await Task.Run(() =>
            {
                aluno.Deletado = true;
                _context.Alunos.Update(aluno);
                _context.SaveChanges();
            });
        }
    }
}
