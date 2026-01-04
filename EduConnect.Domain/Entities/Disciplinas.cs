using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Disciplinas
{
    [Key]
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public DateOnly DataCriacao { get; set; }
    public bool Deletado { get; set; } = false;

    // Relacionamento
    public ICollection<Professor> Professores { get; set; } = [];
}
