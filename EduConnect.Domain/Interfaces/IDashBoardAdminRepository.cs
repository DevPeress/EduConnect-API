using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDashboardAdminRepository
{
    Task<List<Aluno>> GetAllAlunosAsync();
    Task<List<Professor>> GetAllProfessoresAsync();
    Task<int> GetAumentoAlunos();
    Task<int> GetAumentProfessores();
    Task<double> GetPorcentagemAlunos();
    Task<double> GetPorcentagemProfessores();
}
