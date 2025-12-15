using EduConnect.Domain.Interfaces;

namespace EduConnect.Domain.Entities;

public class Funcionario : Pessoa, IPessoaComConta
{
    public int ContaId { get; set; }
    public Conta Conta { get; set; } = null!;
    public required string Cargo { get; set; }
    public required DateOnly DataAdmissao { get; set; }
    public required decimal Salario { get; set; } = decimal.Zero;
    public required string Departamento { get; set; }
    public required string Supervisor { get; set; }
    public required string Turno { get; set; }
}
