namespace EduConnect.Domain.Entities;

public class Funcionario : Pessoa
{
    public required string Cargo { get; set; }
    public required DateOnly DataAdmissao { get; set; }
    public required decimal Salario { get; set; } = decimal.Zero;
    public required string Departamento { get; set; }
    public required string Supervisor { get; set; }
    public required string Turno { get; set; }
}
