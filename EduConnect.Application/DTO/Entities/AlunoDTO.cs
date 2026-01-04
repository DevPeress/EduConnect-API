using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;
public class AlunoDTO 
{
    public required string Registro { get; init; }
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required string Foto { get; init; }
    public required string Telefone { get; init; }
    public required string Status { get; init; }
    public required DateOnly Nasc { get; init; }
    public string? TurmaRegistro { get; init; } 
    public AlunoDTO() { }
    public AlunoDTO(Aluno dados)
    {
        Registro = dados.Registro;
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        TurmaRegistro = dados.TurmaRegistro;
        Foto = dados.Foto;
    }
}
