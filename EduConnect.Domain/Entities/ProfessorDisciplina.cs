using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class ProfessorDisciplina
{
    [Key]
    public int Id { get; set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;

    public string DisciplinaRegistro { get; set; } = null!;
    public Disciplinas Disciplina { get; set; } = null!;
}
