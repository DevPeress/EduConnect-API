using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public class FuncionarioDTO : PessoaDTO
{
    public string Cargo { get; init; } = null!;
    public DateOnly DataAdmissao { get; init; }
    public decimal Salario { get; init; }
    public string Departamento { get; init; } = null!;
    public string Supervisor { get; init; } = null!;
    public string Turno { get; init; } = null!;

    public FuncionarioDTO() { }
    public FuncionarioDTO(Funcionario dados)
    {
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
