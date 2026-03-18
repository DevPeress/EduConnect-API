using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class AtividadesService(IAtividadesRepository repo, IMapper mapper)
{
    private readonly IAtividadesRepository _atividadesRepository = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<(List<AtividadesDTO>, int TotalRegistro)>> GetByFilters(FiltroPessoaDTO filtrodto, string id, string cargo)
    {
        var filtro = new FiltroPessoa
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
            Pesquisa = filtrodto.Pesquisa
        };

        var result = await _atividadesRepository.GetByFilters(filtro, id, cargo);
        if (result.IsFailed)
            return Result.Fail("Não foi possível realizar a filtragem");

        var (atividades, total) = result.Value;

        List<AtividadesDTO> atividadesDTO = _mapper.Map<List<AtividadesDTO>>(atividades);

        return (atividadesDTO, total);
    }

    public async Task<Result<(List<string>, List<string>)>> GetInformativos()
    {
        return await _atividadesRepository.GetInformativos();
    }

    public async Task<Result<Atividades>> GetatividadesByIdAsync(int Registro)
    {
        var aluno = await _atividadesRepository.GetByIdAsync(Registro);
        if (aluno == null)
            return Result.Fail("Atividade não encontrado.");

        return aluno;
    }

    public async Task<Result<bool>> AddatividadesAsync(AtividadesCadastroDTO atividadesDTO)
    {
        var atividade = _mapper.Map<Atividades>(atividadesDTO);

        return await _atividadesRepository.AddAsync(atividade);
    }

    public async Task<Result<bool>> UpdateatividadesAsync(AtividadesUpdateDTO atividadesDTO)
    {
        var atividadeExting = await _atividadesRepository.GetByIdAsync(atividadesDTO.Id);
        if (atividadeExting == null)
            return Result.Fail("Não foi possível localizar a atividade para a edição.");

        var atividade = _mapper.Map<Atividades>(atividadesDTO);

        return await _atividadesRepository.UpdateAsync(atividade);
    }

    public async Task<Result<bool>> DeleteatividadesAsync(int Registro)
    {
        var atividadeExting = await _atividadesRepository.GetByIdAsync(Registro);
        if (atividadeExting == null)
            return Result.Fail("Não foi possível localizar a atividade para a exclusão.");

        return await _atividadesRepository.DeleteAsync(atividadeExting.Value);
    }
}
