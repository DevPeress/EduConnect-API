using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain;

public class Funcionario : Pessoa
{
    [Key]
    public required string Codigo { get; set; }
    public required string Cargo { get; set; }
    public required DateTime DataAdmissao { get; set; }
    public required decimal Salario { get; set; }
    public required string Departamento { get; set; }
    public required string Supervisor { get; set; }
    public required string Turno { get; set; }
}
