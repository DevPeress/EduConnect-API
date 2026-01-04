namespace EduConnect.Domain.Entities;

public class Disciplinas
{
    public int DisciplinaID { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public DateOnly DataCriacao { get; set; }
    public bool Deletado { get; set; } = false;

    // Relacionamento
    public ICollection<Professor> Professores { get; set; } = [];
}
