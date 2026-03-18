using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Atividades
{
    [Key]
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required DateTime Data { get; set; }
    public required DateTime DataEntrega { get; set; }
    public required int ProfessorId { get; set; }
    public required string TurmaId { get; set; }   
    public bool Deletado { get; set; } = false;
}
