namespace EduConnect.Application.DTO;

public record PessoaDTO
{
    public required string Registro { get; init; }
    public required string Nome { get; init; }
    public required string Email { get; init; }
    public required string Telefone { get; init; }
    public required string Status { get; init; }
    public required DateOnly Nasc { get; init; }
    public required string Endereco { get; init; }
    public required string Cpf { get; init; }
    public required string ContatoEmergencia { get; init; }
}
