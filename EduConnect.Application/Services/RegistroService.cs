using EduConnect.Application.DTO;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Interfaces;

namespace EduConnect.Application.Services;

public class RegistroService(IRegistroRepository repo)
{
    private readonly IRegistroRepository _registroRepository = repo;
    public async Task<List<Registro>> GetAllRegistrosAsync()
    {
        return await _registroRepository.GetRegistrosAsync();
    }
    public async Task<List<Registro>> GetLastRegistrosAsync()
    {
        return await _registroRepository.GetLastRegistrosAync();
    }
    public async Task<Registro?> GetRegistroByIdAsync(Guid id)
    {
        return await _registroRepository.GetRegistroByIdAsync(id);
    }
    public async Task AddRegistroAsync(RegistroDTO dto)
    {
        var registro = new Registro
        {
            Id = Guid.NewGuid(),
            Tipo = dto.Tipo,
            Descricao = dto.Descricao,
            Horario = DateTime.Now,
            PessoaId = dto.Id
        };
        await _registroRepository.AddRegistroAsync(registro);
    }
    public async Task UpdateRegistroAsync(RegistroDTO dto)
    {
        var registro = new Registro
        {
            Id = dto.Id,
            Tipo = dto.Tipo,
            Descricao = dto.Descricao,
            Horario = dto.Horario,
            PessoaId = dto.Id
        };
        await _registroRepository.UpdateRegistroAsync(registro);
    }
    public async Task DeleteRegistroAsync(Guid id)
    {
        await _registroRepository.DeleteRegistroAsync(id);
    }
}
