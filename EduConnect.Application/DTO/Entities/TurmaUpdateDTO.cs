using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public record TurmaUpdateDTO
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required string Status { get; set; }
    public required string AnoEletivo { get; set; }
    public required int Capacidade { get; set; }
    public required string ProfessorResponsavel { get; set; }
    public required TimeOnly Inicio { get; set; }
    public required TimeOnly Fim { get; set; }
    public required string Sala { get; set; }
    public required List<string> Dias { get; set; }
    public required bool Deletado { get; set; } 
    public required List<string> TurmaDisciplina { get; set; }
    public required ICollection<Aluno> Alunos { get; set; }
}
