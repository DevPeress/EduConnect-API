using AutoMapper;
using EduConnect.Application.DTO.Entities;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class RegistroService(IRegistroRepository repo, IMapper mapper)
{
    private readonly IRegistroRepository _registroRepository = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<(List<RegistroDTO>, int TotalRegistro)> GetRegistros(FiltroRegistroDTO filtrodto)
    {
        var filtro = new FiltroRegistro
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status,
            Ano = filtrodto.Ano,
        };

        var (registros, total) = await _registroRepository.GetRegistrosAsync(filtro);

        List<RegistroDTO> registrosDTO = _mapper.Map<List<RegistroDTO>>(registros);

        return (registrosDTO, total);
    }

    public async Task<Registro?> GetRegistroByIdAsync(int id)
    {
        return await _registroRepository.GetRegistroByIdAsync(id);
    }

    public async Task DeleteRegistroAsync(int id)
    {
        await _registroRepository.DeleteRegistroAsync(id);
    }
}
