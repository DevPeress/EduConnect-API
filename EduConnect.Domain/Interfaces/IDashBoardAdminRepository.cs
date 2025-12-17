using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDashboardAdminRepository
{
    Task<(int, int, int)> GetTotalsAsync();
    Task<(int, int, int)> GetAumentoAsync();
    Task<(double, double, double)> GetPorcentagemAsync();
}
