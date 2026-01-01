using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class DashBoardAdminService(IDashboardAdminRepository repo)
{
    private readonly IDashboardAdminRepository _dashboardAdminRepository = repo;

    public async Task<(int, int, int, int)> GetTotalsAsync()
    {
        return await _dashboardAdminRepository.GetTotalsAsync();
    }

    public async Task<(int, int, int, int)> GetAumentoAsync()
    {
        return await _dashboardAdminRepository.GetAumentoAsync();
    }

    public async Task<(double, double, double, double)> GetPorcentagemAsync()
    {
        return await _dashboardAdminRepository.GetPorcentagemAsync();
    }
}
