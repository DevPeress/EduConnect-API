using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;
public class TurmaService(ITurmaRepository repo, IMapper mapper)
{
    private readonly ITurmaRepository _turmaRepository = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<(List<TurmaDTO>, int TotalRegistro)>> GetByFilters(FiltroTurmaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroTurma
        {
            Turno = filtrodto.Turno,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Page = filtrodto.Page,
            Pesquisa = filtrodto.Pesquisa
        };

        var (turmas, total) = await _turmaRepository.GetByFilters(filtro, id, cargo);

        List<TurmaDTO> turmaDTO = _mapper.Map<List<TurmaDTO>>(turmas);

        return (turmaDTO, total);
    }

    public async Task<Result<Turma>> GetLastTurma()
    {
        var turma = await _turmaRepository.GetLastTurma();
        if (turma == null)
            return Result.Fail("Nenhuma turma encontrada.");
        
        return turma;
    }

    public async Task<Result<List<string>>> GetTurmasValidasAsync()
    {
        return await _turmaRepository.GetTurmasValidasAsync();
    }

    public async Task<Result<List<string>>> GetInformativos()
    {
        return await _turmaRepository.GetInformativos();
    }

    public async Task<Result<Turma>> GetTurmaByIdAsync(string id)
    {
        var turma = await _turmaRepository.GetTurmaByIdAsync(id);
        if (turma == null)
            return Result.Fail("Nenhuma turma encontrada.");

        return turma;
    }

    public async Task<Result<bool>> AddTurmaAsync(TurmaCadastroDTO turmaDTO)
    {
        var turmaExisting = await _turmaRepository.GetTurmaByDados(turmaDTO.Registro, turmaDTO.AnoEletivo);
        if (turmaExisting == null)
            return Result.Fail("Já existe uma turma com o mesmo registro e ano letivo.");

        var turma = _mapper.Map<Turma>(turmaDTO);

        return await _turmaRepository.AddTurmaAsync(turma, turmaDTO.Disciplinas);
    }

    public async Task<Result<bool>> UpdateTurmaAsync(TurmaUpdateDTO turmaDTO)
    {
        var turmaExisting = await _turmaRepository.GetTurmaByDados(turmaDTO.Registro, turmaDTO.AnoEletivo);
        if (turmaExisting == null)
            return Result.Fail("Não existe uma turma com esse registro!");

        var turma = _mapper.Map<Turma>(turmaDTO);

        return await _turmaRepository.UpdateTurmaAsync(turma, turmaDTO.TurmaDisciplina);
    }

    public async Task<Result<bool>> DeleteTurmaAsync(string id)
    {
        var turma = await _turmaRepository.GetTurmaByIdAsync(id);
        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return await _turmaRepository.DeleteTurmaAsync(turma);
    }
}
