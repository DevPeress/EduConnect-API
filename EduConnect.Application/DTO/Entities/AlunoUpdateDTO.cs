namespace EduConnect.Application.DTO.Entities;

public record AlunoUpdateDTO
{
    public required string Registro { get; init; }
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required string Foto { get; init; }
    public required string Telefone { get; init; }
    public required string Status { get; init; }
    public required DateOnly Nasc { get; init; }
    public required string Endereco { get; init; }
    public required string Cpf { get; init; }
    public required string NomeEmergencia { get; init; }
    public required string ContatoEmergencia { get; init; }
    public string? TurmaRegistro { get; init; }
}
