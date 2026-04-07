namespace EduConnect.Domain.Entities;

public class Boletim
{
    public required string NomeAluno { get; set; }
    public required string Turma { get; set; }
    public required List<DisciplinaNota> Disciplinas { get; set; }
}
