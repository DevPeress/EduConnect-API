using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;

namespace EduConnect.Infra.Data.Repositories;

public class ContaRepository(EduContext context) : IContaRepository
{
    private readonly EduContext _context = context;
    public async Task<Conta> GetConta(string email, string senha)
    {
        return await _context.Contas
            .FirstOrDefaultAsync(c => c.Email == email && c.Senha == senha);
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Contas.AnyAsync(c => c.Email == email);
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
            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
        }
    }
}
