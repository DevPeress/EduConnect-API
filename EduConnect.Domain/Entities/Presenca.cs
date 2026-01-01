namespace EduConnect.Domain.Entities;

public class Presenca
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    public bool Presente { get; set; }
    public bool Justificada { get; set; }
    public string? Observacao { get; set; }
    public int ProfessorId { get; set; }
    public DateTime CriadoEm { get; set; }

    // Relacionamento
    public int AlunoId { get; set; }
    public required Aluno Aluno { get; set; }
    public int TurmaId { get; set; }
    public required Turma Turma { get; set; }
}
