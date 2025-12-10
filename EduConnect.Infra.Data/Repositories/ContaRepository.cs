using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace EduConnect.Infra.Data.Repositories;

public class ContaRepository(EduContext context) : IContaRepository
{
    private readonly EduContext _context = context;
    public async Task<Conta> GetConta(string registro, string senha)
    {
        return await _context.Contas
            .FirstAsync(c => c.Registro == registro && c.Senha == senha);
    }

    public async Task<bool> EmailExistsAsync(string registro)
    {
        return await _context.Contas.AnyAsync(c => c.Registro == registro);
    }

    public async Task AddContaAsync(Conta conta)
    {
        await _context.Contas.AddAsync(conta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContaAsync(int id)
    {
        var conta = await _context.Contas.FindAsync(id);
        if (conta != null)
        {
            await Task.Run(() =>
            {
                conta.Deletado = true;
                _context.Contas.Update(conta);
                _context.SaveChanges();
            });
        }
    }
}
