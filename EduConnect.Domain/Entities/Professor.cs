using EduConnect.Domain.Interfaces;

namespace EduConnect.Domain.Entities;

public class Professor : Pessoa, IPessoaComConta
{
    public DateOnly? Contratacao { get; set; }
    public required string Formacao { get; set; }
    public required decimal Salario { get; set; } = decimal.Zero;

    // Relacionamento
    public int ContaId { get; set; }
    public Conta Conta { get; set; } = null!;
    public ICollection<ProfessorDisciplina> ProfessorDisciplinas { get; set; } = [];
    public ICollection<Turma> Turmas { get; set; } = [];
}
