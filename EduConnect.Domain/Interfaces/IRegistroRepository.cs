using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IRegistroRepository
{
    Task<(IEnumerable<Registro>, int TotalRegistro)> GetRegistrosAsync(FiltroPessoa filtro);
    Task<Registro?> GetRegistroByIdAsync(int registro);
    Task DeleteRegistroAsync(int registro);
}
