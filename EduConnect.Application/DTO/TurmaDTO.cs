namespace EduConnect.Application.DTO;
public record TurmaDTO
{
    public Guid Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public Guid ProfessorID { get; set; }
    public List<Guid> Alunos { get; set; } = [];
    public Guid SalaID { get; set; }
    public Guid DisciplinaID { get; set; }
    public required string Horario { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
}
