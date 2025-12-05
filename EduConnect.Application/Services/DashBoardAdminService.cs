using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class DashBoardAdminService(IDashboardAdminRepository repo)
{
    private readonly IDashboardAdminRepository _dashboardAdminRepository = repo;

    public async Task<int> GetTotalAlunosAsync()
    {
        var alunos = await _dashboardAdminRepository.GetAllAlunosAsync();
        return alunos.Count;
    }

    public async Task<int> GetTotalProfessoresAsync()
    {
        var professores = await _dashboardAdminRepository.GetAllProfessoresAsync();
        return professores.Count;
    }

    public async Task<int> GetTotalTurmasAsync()
    {
        var turmas = await _dashboardAdminRepository.GetAllTurmasAsync();
        return turmas.Count;
    }

    public async Task<(int, int, int)> GetAumentoAsync()
    {
        var aumentoAlunos = await _dashboardAdminRepository.GetAumentoAlunos();
        var aumentoProfessores = await _dashboardAdminRepository.GetAumentProfessores();
        var aumentoTurmas = await _dashboardAdminRepository.GetAumentTurmas();
        return (aumentoAlunos, aumentoProfessores, aumentoTurmas);
    }

    public async Task<(double, double, double)> GetPorcentagemAsync()
    {
        var aumentoAlunos = await _dashboardAdminRepository.GetPorcentagemAlunos();
        var aumentoProfessores = await _dashboardAdminRepository.GetPorcentagemProfessores();
        var aumentoTurmas = await _dashboardAdminRepository.GetPorcentagemTurmas();
        return (aumentoAlunos, aumentoProfessores, aumentoTurmas);
    }
}
