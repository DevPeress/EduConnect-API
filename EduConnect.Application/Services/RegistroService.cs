using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class RegistroService(IRegistroRepository repo)
{
    private readonly IRegistroRepository _registroRepository = repo;
    public async Task<(List<RegistroDTO>, int TotalRegistro)> GetRegistros(FiltroDTO filtrodto)
    {
        var filtro = new Filtro
        {
            Page = filtrodto.Page,
            Categoria = filtrodto.Categoria,
            Status = filtrodto.Status
        };

        var (registros, total) = await _registroRepository.GetRegistrosAsync(filtro);

        List<RegistroDTO> registrosDTO = [];
        foreach (var registro in registros)
        {
            registrosDTO.Add(new RegistroDTO
            {
                UserName = registro.UserName,
                UserRole = registro.UserRole,
                Action = registro.Action.ToString(),
                Entity = registro.Entity,
                Description = registro.Detalhes,
                CreatedAt = registro.CreatedAt
            });
        }

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
