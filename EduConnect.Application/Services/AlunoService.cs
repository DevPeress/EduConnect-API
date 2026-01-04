using EduConnect.Application.Common.Auditing;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo)
{
    private readonly IAlunoRepository _alunoRepository = repo;

    public async Task<(List<AlunoDTO>, int TotalRegistro)> GetByFilters(FiltroPessoaDTO filtrodto)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano
        };

        var (alunos, total) = await _alunoRepository.GetByFilters(filtro);

        List<AlunoDTO> alunosDTO = [];
        foreach(var aluno in alunos)
        {
            var dto = new AlunoDTO(aluno)
            {
                Registro = aluno.Registro,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Telefone = aluno.Telefone,
                Status = aluno.Status,
                Nasc = aluno.Nasc,
                Endereco = aluno.Endereco,
                Cpf = aluno.Cpf,
                ContatoEmergencia = aluno.ContatoEmergencia,
                Foto = aluno.Foto
            };
            alunosDTO.Add(dto);
        }

        return (alunosDTO, total);
    }

    public async Task<(List<string>, List<string>?)> GetInformativos()
    {
        return await _alunoRepository.GetInformativos();
    }

    public async Task<Aluno?> GetAlunoByIdAsync(string Registro)
    {
        return await _alunoRepository.GetByIdAsync(Registro);
    }

    public async Task<Aluno?> GetLastAluno()
    {
        return await _alunoRepository.GetLastPessoaAsync();
    }

    public async Task AddAlunoAsync(AlunoCadastroDTO AlunoDTO)
    {
        var aluno = new Aluno
        {
            Registro = AlunoDTO.Registro,
            Nome = AlunoDTO.Nome,
            Email = AlunoDTO.Email,
            Foto = AlunoDTO.Foto,
            Telefone = AlunoDTO.Telefone,
            Status = AlunoDTO.Status,
            Nasc = AlunoDTO.Nasc,
            Endereco = AlunoDTO.Endereco,
            Cpf = AlunoDTO.CPF,
            ContatoEmergencia = AlunoDTO.ContatoEmergencia,
            DataMatricula = DateOnly.FromDateTime(DateTime.Now),
            Media = 0,
            Deletado = false,
            TurmaRegistro = AlunoDTO.Turma
        };
        await _alunoRepository.AddAsync(aluno);
    }

    public async Task UpdateAlunoAsync(AlunoUpdateDTO AlunoDTO)
    {
        var aluno = new Aluno
        {
            Registro = AlunoDTO.Registro,
            Nome = AlunoDTO.Nome,
            Email = AlunoDTO.Email,
            Foto = AlunoDTO.Foto,
            Telefone = AlunoDTO.Telefone,
            Status = AlunoDTO.Status,
            Nasc = AlunoDTO.Nasc,
            Endereco = AlunoDTO.Endereco,
            Cpf = AlunoDTO.Cpf,
            ContatoEmergencia = AlunoDTO.ContatoEmergencia,
            Deletado = AlunoDTO.Deletado,
            Media = AlunoDTO.Media,
            TurmaRegistro = AlunoDTO.TurmaRegistro
        };
        await _alunoRepository.UpdateAsync(aluno);
    }

    public async Task DeleteAlunoAsync(string Registro)
    {
        await _alunoRepository.DeleteAsync(Registro);
    }
}
