using EduConnect.Domain;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo)
{
    private readonly IAlunoRepository _alunoRepository = repo;
    public async Task<List<Aluno>> GetAllAlunosAsync()
    {
        return await _alunoRepository.GetAllAsync();
    }
    public async Task<Aluno?> GetAlunoByIdAsync(string matricula)
    {
        return await _alunoRepository.GetByIdAsync(matricula);
    }
    public async Task AddAlunoAsync(Aluno aluno)
    {
        await _alunoRepository.AddAsync(aluno);
    }
    public async Task UpdateAlunoAsync(Aluno aluno)
    {
        await _alunoRepository.UpdateAsync(aluno);
    }
    public async Task DeleteAlunoAsync(string matricula)
    {
        await _alunoRepository.DeleteAsync(matricula);
    }
}
