using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IDashboardAdminRepository
{
    Task<List<Aluno>> GetAllAlunosAsync();
    Task<List<Professor>> GetAllProfessoresAsync();
    Task<List<Turma>> GetAllTurmasAsync();
    Task<int> GetAumentoAlunos();
    Task<int> GetAumentProfessores();
    Task<int> GetAumentTurmas();
    Task<double> GetPorcentagemAlunos();
    Task<double> GetPorcentagemProfessores();
    Task<double> GetPorcentagemTurmas();
}
