using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class FuncionarioRepository(EduContext context) : IFuncionarioRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Funcionario> QueryFiltroProfessor(FiltroPessoas filtro)
    {
        var query = _context.Funcionarios.AsNoTracking().Where(p => p.Deletado == false);

        if (filtro.Status != null && filtro.Status != "Todos os Status")
        {
            query = query.Where(dados => dados.Status == filtro.Status);
        }

        return query;
    }

    public async Task<(IEnumerable<Funcionario>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro)
    {
        var query = QueryFiltroProfessor(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Funcionario?> GetByIdAsync(int id)
    {
        return await _context.Funcionarios.Where(p => p.Deletado == false).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Funcionario?> GetLastFuncionarioAsync()
    {
        return await _context.Funcionarios.Where(p => p.Deletado == false)
        .OrderBy(a => a.Registro)
        .LastAsync();
    }

    public async Task AddAsync(Funcionario funcionario)
    {
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
