using EduConnect.Domain.Interfaces;

namespace EduConnect.Domain.Entities;

public class Aluno : Pessoa, IPessoaComConta
{
    public required int Media { get; set; }
    public required DateOnly DataMatricula { get; set; }

    // Relacionamento
    public int ContaId { get; set; }
    public Conta Conta { get; set; } = null!;
    public string? TurmaId { get; set; }
    public Turma? Turma { get; set; } = null!;
}
