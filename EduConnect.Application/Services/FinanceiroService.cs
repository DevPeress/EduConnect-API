using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;
using FluentResults;

namespace EduConnect.Application.Services;

public class FinanceiroService(IFinanceiroRepository repo, IMapper mapper)
{
    private readonly IFinanceiroRepository _financeiroRepository = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<Financeiro>>> GetByAlunoId(string Registro)
    {
        return await _financeiroRepository.GetByAlunoId(Registro);
    }

    public async Task<Result<(decimal TotalRecebido, decimal TotalPendente, decimal TotalAtrasado)>> GetDashBoard()
    {
        return await _financeiroRepository.GetDashBoard();
    }

    public async Task<Result<(List<Financeiro>, int totalRegistro)>> GetByFilters(FiltroFinanceiroDTO filtrodto)
    {
        var  filtro = new FiltroFinanceiro
        {
            Status = filtrodto.Status,
            Categoria = filtrodto.Categoria,
            Meses = filtrodto.Meses,
            Pesquisa = filtrodto.Pesquisa
        };

        return await _financeiroRepository.GetByFilters(filtro);
    }

    public async Task<Result<Financeiro>> GetById(string Registro)
    {
        var financeiro = await _financeiroRepository.GetById(Registro);
        if (financeiro == null)
            return Result.Fail("Registro financeiro não encontrado.");

        return financeiro;
    }

    public async Task<Result<bool>> AddFinanceiroAsync(FinanceiroCadastroDTO FinanceiroDTO)
    {
        var financeiroExistente = await _financeiroRepository.GetById(FinanceiroDTO.Registro);
        if (financeiroExistente != null)
            return Result.Fail("Já existe um registro financeiro com esse identificador.");

        var financeiro = _mapper.Map<Financeiro>(FinanceiroDTO);

        return await _financeiroRepository.Add(financeiro);
    }

    public async Task<Result<bool>> UpdateFinanceiroAsync(FinanceiroUpdateDTO FinanceiroDTO)
    {
        var financeiroExistente = await _financeiroRepository.GetById(FinanceiroDTO.Registro);
        if (financeiroExistente == null)
            return Result.Fail("Não existe um registro financeiro com esse identificador.");

        var financeiro = _mapper.Map<Financeiro>(FinanceiroDTO);

        return await _financeiroRepository.Update(financeiro);
    }

    public async Task<Result<bool>> DeleteFinanceiroAsync(string Registro)
    {
        var financeiroExistente = await _financeiroRepository.GetById(Registro);
        if (financeiroExistente == null)
            return Result.Fail("Não existe um registro financeiro com esse identificador.");

        return await _financeiroRepository.Delete(financeiroExistente);
    }
}
