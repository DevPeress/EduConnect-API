using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;
public record RegistroDTO
{
    public int Id { get; init; } 
    public string Tipo { get; init; } = null!;
    public string Descricao { get; init; } = null!;
    public DateTime Horario { get; init; } = DateTime.Now;
    public int PessoaId { get; init; } 

    public RegistroDTO (Registro dados)
    {
        Id = dados.Id;
        Tipo = dados.Tipo;
        Descricao = dados.Descricao;
        Horario = dados.Horario;
        PessoaId = dados.PessoaId;
    }
}
