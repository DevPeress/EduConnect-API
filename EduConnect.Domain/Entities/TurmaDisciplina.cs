using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class TurmaDisciplina
{
    [Key]
    public int Id { get; set; }
    public int TurmaRegistro { get; set; }
    public Turma Turma { get; set; } = null!;

    public string DisciplinaRegistro { get; set; } = null!;
    public Disciplinas Disciplina { get; set; } = null!;
}

