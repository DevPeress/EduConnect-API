using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class ProfessorService(IProfessorRepository repo)
{
    private readonly IProfessorRepository _professorRepository = repo;
    public async Task<List<Professor>> GetAllProfessorAsync()
    {
        return await _professorRepository.GetAllAsync();
    }
    
    public async Task<(List<ProfessorDTO>, int TotalRegistro)> GetByFilters(FiltroPessoaDTO filtrodto)
    {
        var filtro = new FiltroPessoas
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status
        };

        var (professores, total) = await _professorRepository.GetByFilters(filtro);
        List<ProfessorDTO> professoresDTO = professores.Select(professores => new ProfessorDTO(professores)
        {
            Registro = professores.Registro,
            Nome = professores.Nome,
            Email = professores.Email,
            Telefone = professores.Telefone,
            Status = professores.Status,
            Nasc = professores.Nasc,
            Endereco = professores.Endereco,
            Cpf = professores.Cpf,
            ContatoEmergencia = professores.ContatoEmergencia
        }).ToList();
       
        return (professoresDTO, total);
    }

    public async Task<Professor?> GetProfessorByIdAsync(int id)
    {
        return await _professorRepository.GetByIdAsync(id);
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
            Id = dto.Id,
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
    public async Task DeleteProfessorAsync(int id)
    {
        await _professorRepository.DeleteAsync(id);
    }
}
