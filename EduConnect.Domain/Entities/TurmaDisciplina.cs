using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class TurmaDisciplina
{
    [Key]
    public int Id { get; set; }

    // Relacionamentos
    public string TurmaRegistro { get; set; } = null!;
    public Turma Turma { get; set; } = null!;

    public string DisciplinaRegistro { get; set; } = null!;
    public Disciplinas Disciplina { get; set; } = null!;
}

