using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class RegistroRepository(EduContext context) : IRegistroRepository
{
    private readonly EduContext _context = context;

    public async Task<List<Registro>> GetRegistrosAsync()
    {
        return await _context.Registros.Where(p => p.Deletado == false).ToListAsync();
    }

    public async Task<List<Registro>> GetLastRegistrosAync()
    {
        return await _context.Registros.Where(p => p.Deletado == false)
            .OrderBy(r => r.Horario)
            .Take(10)
            .ToListAsync();
    }

    public async Task<Registro?> GetRegistroByIdAsync(int registro)
    {
        return await _context.Registros.Where(p => p.Deletado == false).FirstOrDefaultAsync(r => r.Id == registro);
    }

    public async Task AddRegistroAsync(Registro registro)
    {
        await _context.Registros.AddAsync(registro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRegistroAsync(Registro registro)
    {
        _context.Registros.Update(registro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRegistroAsync(int id)
    {
        var registro = await GetRegistroByIdAsync(id);
        if (registro != null)
        {
            await Task.Run(() =>
            {
                registro.Deletado = false;
                _context.Registros.Update(registro);
                _context.SaveChanges();
            });
        }
    }
}
