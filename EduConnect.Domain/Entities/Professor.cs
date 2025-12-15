using EduConnect.Domain.Interfaces;

namespace EduConnect.Domain.Entities;

public class Professor : Pessoa, IPessoaComConta
{
    public int ContaId { get; set; }
    public Conta Conta { get; set; } = null!;
    public required List<string> Turmas { get; set; }
    public required string Disciplina { get; set; }
    public required DateOnly Contratacao { get; set; }
    public required string Formacao { get; set; }
    public required decimal Salario { get; set; } = decimal.Zero;
}
