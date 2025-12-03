using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public record ProfessorDTO
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Telefone { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateOnly Nasc { get; init; }
    public string Endereco { get; init; } = null!;
    public string Cpf { get; init; } = null!;
    public string ContatoEmergencia { get; init; } = null!;
    public string Registro { get; init; } = null!;
    public List<string> Turmas { get; init; } = null!;
    public string Disciplina { get; init; } = null!;
    public DateOnly Contratacao { get; init; }
    public string Formacao { get; init; } = null!;
    public decimal Salario { get; init; }

    public ProfessorDTO(Professor dados)
    {
        Id = dados.Id;
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        Endereco = dados.Endereco;
        Cpf = dados.Cpf;
        ContatoEmergencia = dados.ContatoEmergencia;
        Registro = dados.Registro;
        Turmas = dados.Turmas;
        Disciplina = dados.Disciplina;
        Contratacao = dados.Contratacao;
        Formacao = dados.Formacao;
        Salario = dados.Salario;
    }
}
