using EduConnect.Application.DTO;
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
    public async Task<Professor?> GetLastProfessorAsync()
    {
        return await _professorRepository.GetLastProfessorAsync();
    }
    public async Task AddProfessorAsync(ProfessorDTO dto)
    {
        var professor = new Professor
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Turmas = dto.Turmas,
            Disciplina = dto.Disciplina,
            Formacao = dto.Formacao,
            Contratacao = dto.Contratacao,
            Salario = dto.Salario,
        };
        await _professorRepository.AddAsync(professor);
    }
    public async Task UpdateProfessorAsync(ProfessorDTO dto)
    {
        var professor = new Professor
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Status = dto.Status,
            Nasc = dto.Nasc,
            Endereco = dto.Endereco,
            Cpf = dto.Cpf,
            ContatoEmergencia = dto.ContatoEmergencia,
            Registro = dto.Registro,
            Turmas = dto.Turmas,
            Disciplina = dto.Disciplina,
            Formacao = dto.Formacao,
            Contratacao = dto.Contratacao,
            Salario = dto.Salario,
        };
        await _professorRepository.UpdateAsync(professor);
    }
    public async Task DeleteProfessorAsync(string matricula)
    {
        await _professorRepository.DeleteAsync(matricula);
    }
}
