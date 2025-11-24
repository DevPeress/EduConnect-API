namespace EduConnect.Domain;

public class Aluno : Pessoa
{
    public required string Turma { get; set; }
    public required int Media { get; set; }
    public required DateOnly DataMatricula { get; set; }
}
