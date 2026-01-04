using EduConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Turma
{
    [Key]
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required DateTime Inicio { get; set; }
    public required DateTime Fim { get; set; }
    public required string Sala { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
    public DateOnly DataCriacao { get; set; } 
    public required string Status { get; set; }
    public required bool Deletado { get; set; }

    // Relacionamento
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
    public required ICollection<Aluno> Alunos { get; set; } 
    public required ICollection<TurmaDisciplina> TurmaDisciplinas { get; set; } 
}
