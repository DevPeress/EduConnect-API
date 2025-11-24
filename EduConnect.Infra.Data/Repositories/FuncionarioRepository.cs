using EduConnect.Domain;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;

public class FuncionarioRepository(EduContext context) : IFuncionarioRepository
{
    private readonly EduContext _context = context;

    public async Task<List<Funcionario>> GetAllAsync()
    {
        return await _context.Funcionarios.ToListAsync();
    }
    public async Task<Funcionario?> GetByIdAsync(string matricula)
    {
        return await _context.Funcionarios.FirstOrDefaultAsync(a => a.Registro == matricula);
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
    public async Task DeleteAsync(string matricula)
    {
        var funcionario = await GetByIdAsync(matricula);
        if (funcionario != null)
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
        }
    }
}
