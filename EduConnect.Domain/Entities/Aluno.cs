using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain;

public class Aluno : Pessoa
{
    [Key]
    public required string Turma { get; set; }
    public required int Media { get; set; }
    public required DateTime DataMatricula { get; set; }
}
