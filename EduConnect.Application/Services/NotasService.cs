using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class NotasService(INotasRepository repo, IMapper mapper)
{
    private readonly INotasRepository _notasRepository = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<(List<NotasDTO>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var result = await _notasRepository.GetByFilters(filtro, id, cargo);
        if (result.IsFailed)
            return Result.Fail("Não foi possível realizar a filtragem");

        var (notas, total) = result.Value;

        List<NotasDTO> notasDTO = _mapper.Map<List<NotasDTO>>(notas);

        return (notasDTO, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _notasRepository.GetInformativos();
    }

    public async Task<Result<Notas>> GetNotasByIdAsync(int Registro)
    {
        var aluno = await _notasRepository.GetByIdAsync(Registro);
        if (aluno == null)
            return Result.Fail("Nota não encontrado.");

        return aluno;
    }

    public async Task<Result<bool>> AddNotaAsync(NotasCadastroDTO notaDTO)
    {
        var nota = _mapper.Map<Notas>(notaDTO);

        return await _notasRepository.AddAsync(nota);
    }

    public async Task<Result<bool>> UpdateNotaAsync(NotasUpdateDTO notaDTO)
    {
        var notaExting = await _notasRepository.GetByIdAsync(notaDTO.Registro);
        if (notaExting == null)
            return Result.Fail("Não foi possível localizar o Aluno para a edição.");

        var nota = _mapper.Map<Notas>(notaDTO);

        return await _notasRepository.UpdateAsync(nota);
    }

    public async Task<Result<bool>> DeleteNotaAsync(int Registro)
    {
        var notaExting = await _notasRepository.GetByIdAsync(Registro);
        if (notaExting == null)
            return Result.Fail("Não foi possível localizar a nota para a exclusão.");

        return await _notasRepository.DeleteAsync(notaExting.Value);
    }
}
