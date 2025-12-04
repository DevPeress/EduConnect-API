using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo)
{
    private readonly IAlunoRepository _alunoRepository = repo;
    public async Task<List<Aluno>> GetAllAlunosAsync()
    {
        return await _alunoRepository.GetAllAsync();
    }
    public async Task<(IEnumerable<Aluno>, int TotalRegistro)> GetByFilters(FiltroPessoas filtro)
    {
        return await _alunoRepository.GetByFilters(filtro);
    }
    public async Task<Aluno?> GetAlunoByIdAsync(int id)
    {
        return await _alunoRepository.GetByIdAsync(id);
    }
    public async Task<Aluno?> GetLastAluno()
    {
        return await _alunoRepository.GetLastAlunoAsync();
    }
    public async Task AddAlunoAsync(AlunoDTO dto)
    {
        var aluno = new Aluno
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
            Turma = dto.Turma,
            Media = dto.Media,
            DataMatricula = dto.DataMatricula
        };
        await _alunoRepository.AddAsync(aluno);
    }
    public async Task UpdateAlunoAsync(AlunoDTO dto)
    {
        var aluno = new Aluno
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
            Turma = dto.Turma,
            Media = dto.Media,
            DataMatricula = dto.DataMatricula
        };
        await _alunoRepository.UpdateAsync(aluno);
    }
    public async Task DeleteAlunoAsync(int id)
    {
        await _alunoRepository.DeleteAsync(id);
    }
}
