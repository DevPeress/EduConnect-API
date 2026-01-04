namespace EduConnect.Domain.Entities;

public class ProfessorDisciplina
{
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;

    public string DisciplinaRegistro { get; set; } = null!;
    public Disciplinas Disciplina { get; set; } = null!;
}
