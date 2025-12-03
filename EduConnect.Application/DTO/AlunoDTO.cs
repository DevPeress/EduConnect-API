using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;
public record AlunoDTO
{
    public Guid Id { get; init; }
    public string Registro { get; init; } = null!;
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Telefone { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateOnly Nasc { get; init; }
    public string Endereco { get; init; } = null!;
    public string Cpf { get; init; } = null!;
    public string ContatoEmergencia { get; init; } = null!;
    public string Turma { get; init; } = null!;
    public int Media { get; init; }
    public DateOnly DataMatricula { get; init; }

    public AlunoDTO(Aluno dados)
    {
        Id = dados.Id;
        Registro = dados.Registro;
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        Endereco = dados.Endereco;
        Cpf = dados.Cpf;
        ContatoEmergencia = dados.ContatoEmergencia;
        Turma = dados.Turma;
        Media = dados.Media;
        DataMatricula = dados.DataMatricula;
    }
}
