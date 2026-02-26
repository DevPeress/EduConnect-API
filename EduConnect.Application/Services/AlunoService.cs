using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo)
{
    private readonly IAlunoRepository _alunoRepository = repo;

    public async Task<Result<(List<AlunoDTO>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var result = await _alunoRepository.GetByFilters(filtro, id, cargo);
        if (result.IsFailed)
            return Result.Fail("Não foi possível realizar a filtragem");

        var (alunos, total) = result.Value;

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

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _alunoRepository.GetInformativos();
    }

    public async Task<Result<Aluno>> GetAlunoByIdAsync(string Registro)
    {
        return await _alunoRepository.GetByIdAsync(Registro);
    }

    public async Task<Result<Aluno>> GetLastAluno()
    {
        return await _alunoRepository.GetLastPessoaAsync();
    }

    public async Task<Result<bool>> AddAlunoAsync(AlunoCadastroDTO AlunoDTO)
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
            NomeEmergencia = AlunoDTO.NomeEmergencia,
            DataMatricula = DateOnly.FromDateTime(DateTime.Now),
            Media = 0,
            Deletado = false,
            TurmaRegistro = AlunoDTO.Turma
        };

        return await _alunoRepository.AddAsync(aluno);
    }

    public async Task<Result<bool>> UpdateAlunoAsync(AlunoUpdateDTO AlunoDTO, DateOnly matricula, int media)
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
            NomeEmergencia = AlunoDTO.NomeEmergencia,
            DataMatricula = matricula,
            Deletado = false,
            Media = media,
            TurmaRegistro = AlunoDTO.TurmaRegistro
        };

        return await _alunoRepository.UpdateAsync(aluno);
    }

    public async Task<Result<bool>> DeleteAlunoAsync(string Registro)
    {
        return await _alunoRepository.DeleteAsync(Registro);
    }
}
