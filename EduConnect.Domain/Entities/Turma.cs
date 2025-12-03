using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Turma
{
    [Key]
    public int Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public int ProfessorID { get; set; }
    public List<int> Alunos { get; set; } = [];
    public int SalaID { get; set; }
    public int DisciplinaID { get; set; }
    public required string Horario { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
}
