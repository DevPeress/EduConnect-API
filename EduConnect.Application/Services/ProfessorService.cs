using EduConnect.Domain;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class ProfessorService(IProfessorRepository repo)
{
    private readonly IProfessorRepository _professorRepository = repo;
    public async Task<List<Professor>> GetAllProfessorAsync()
    {
        return await _professorRepository.GetAllAsync();
    }
    public async Task<Professor?> GetProfessorByIdAsync(string matricula)
    {
        return await _professorRepository.GetByIdAsync(matricula);
    }
    public async Task AddProfessorAsync(Professor professor)
    {
        await _professorRepository.AddAsync(professor);
    }
    public async Task UpdateProfessorAsync(Professor professor)
    {
        await _professorRepository.UpdateAsync(professor);
    }
    public async Task DeleteProfessorAsync(string matricula)
    {
        await _professorRepository.DeleteAsync(matricula);
    }
}
