using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public class FuncionarioDTO
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
    public string Cargo { get; init; } = null!;
    public DateOnly DataAdmissao { get; init; }
    public decimal Salario { get; init; }
    public string Departamento { get; init; } = null!;
    public string Supervisor { get; init; } = null!;
    public string Turno { get; init; } = null!;

    public FuncionarioDTO(Funcionario dados)
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
        Cargo = dados.Cargo;
        DataAdmissao = dados.DataAdmissao;
        Salario = dados.Salario;
        Departamento = dados.Departamento;
        Supervisor = dados.Supervisor;
        Turno = dados.Turno;
    }
}
