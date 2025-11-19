using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain;

public class Professor : Pessoa
{
    [Key]
    public required string Codigo { get; set; }
    public required List<string> Turmas { get; set; }
    public required string Disciplina { get; set; }
    public required DateTime Contratacao { get; set; }
    public required string Formacao { get; set; }
    public required string Especializacao { get; set; }
    public required decimal Salario { get; set; }
    public required string HorarioAula { get; set; }
}
