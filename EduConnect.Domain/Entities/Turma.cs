using EduConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Turma
{
    [Key]
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required TimeOnly Inicio { get; set; }
    public required TimeOnly Fim { get; set; }
    public required string Sala { get; set; }
    public required int Capacidade { get; set; }
    public required string AnoLetivo { get; set; }
    public DateOnly DataCriacao { get; set; } 
    public required string Status { get; set; }
    public required string ProfessorResponsavel { get; set; }
    public required List<string> Dias { get; set; }
    public required bool Deletado { get; set; }

    // Relacionamento
    public required ICollection<Aluno> Alunos { get; set; } 
    public required ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; } 
}
