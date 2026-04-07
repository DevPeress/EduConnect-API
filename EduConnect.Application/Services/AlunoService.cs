using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using EduConnect.Infra.CrossCutting.Utils;
using FluentResults;

namespace EduConnect.Application.Services;

public class AlunoService(IAlunoRepository repo, IMapper mapper)
{
    private readonly IAlunoRepository _alunoRepository = repo;
    private readonly IMapper _mapper = mapper;

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

        var (alunos, total) = await _alunoRepository.GetByFilters(filtro, id, cargo);

        List<AlunoDTO> alunosDTO = _mapper.Map<List<AlunoDTO>>(alunos);

        return (alunosDTO, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _alunoRepository.GetInformativos();
    }

    public async Task<Result<Aluno>> GetAlunoByIdAsync(string Registro)
    {
        var aluno = await _alunoRepository.GetByIdAsync(Registro);
        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        return aluno;
    }

    public async Task<Result<Aluno>> GetLastAluno()
    {
        var aluno = await _alunoRepository.GetLastPessoaAsync();
        if (aluno == null)
            return Result.Fail("Registro não encontrado.");

        return aluno;
    }

    public async Task<Result<bool>> AddAlunoAsync(AlunoCadastroDTO AlunoDTO)
    {
        var alunoExiting = await _alunoRepository.GetByIdAsync(AlunoDTO.Registro);
        if (alunoExiting != null)
            return Result.Fail("Já existe um Aluno com esse Registro!.");

        var conta = new Conta
        {
            Registro = AlunoDTO.Registro,
            Senha = SegurancaManager.GerarSenha(),
            Cargo = "Aluno"
        };

        var aluno = _mapper.Map<Aluno>(AlunoDTO);

        return await _alunoRepository.AddAsync(aluno, conta);
    }

    public async Task<Result<bool>> UpdateAlunoAsync(AlunoUpdateDTO AlunoDTO, DateOnly matricula, int media)
    {
        var alunoExting = await _alunoRepository.GetByIdAsync(AlunoDTO.Registro);
        if (alunoExting == null)
            return Result.Fail("Não foi possível localizar o Aluno para a edição.");

        var aluno = _mapper.Map<Aluno>(AlunoDTO);

        return await _alunoRepository.UpdateAsync(aluno);
    }

    public async Task<Result<bool>> DeleteAlunoAsync(string Registro)
    {
        var alunoExting = await _alunoRepository.GetByIdAsync(Registro);
        if (alunoExting == null)
            return Result.Fail("Não foi possível localizar o Aluno para a exclusão.");

        return await _alunoRepository.DeleteAsync(alunoExting);
    }

    public async Task<Result<byte[]>> GetBoletimAsync(string Registro)
    {
        var alunoExting = await _alunoRepository.GetByIdAsync(Registro);
        if (alunoExting == null)
            return Result.Fail("Não foi possível localizar o Aluno para pegar o boletim.");

        return await _alunoRepository.GetBoletimAsync(Registro);
    }
}
