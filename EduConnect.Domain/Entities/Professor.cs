namespace EduConnect.Domain;

public class Professor : Pessoa
{
    public required List<string> Turmas { get; set; }
    public required string Disciplina { get; set; }
    public required DateOnly Contratacao { get; set; }
    public required string Formacao { get; set; }
    public required decimal Salario { get; set; } = decimal.Zero;
}
