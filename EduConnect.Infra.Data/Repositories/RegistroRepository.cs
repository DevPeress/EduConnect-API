using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Repositories;
public class RegistroRepository(EduContext context) : IRegistroRepository
{
    private readonly EduContext _context = context;

    private IQueryable<Registro> QueryFiltroRegistro(FiltroRegistro filtro)
    {
        var query = _context.Registros.AsNoTracking().Where(p => p.Deletado == false);
        return query;
    }

    public async Task<(IEnumerable<Registro>, int TotalRegistro)> GetRegistrosAsync(FiltroRegistro filtro)
    {
        var query = QueryFiltroRegistro(filtro);
        var total = await query.CountAsync();

        query = query.Skip(filtro.Offset).Take(6);
        var result = await query.ToListAsync();

        return (result, total);
    }

    public async Task<Registro?> GetRegistroByIdAsync(int registro)
    {
        return await _context.Registros.Where(p => p.Deletado == false).FirstOrDefaultAsync(r => r.Id == registro);
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
