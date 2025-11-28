namespace EduConnect.Application.DTO;

public class ProfessorDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateOnly Nasc { get; set; }
    public string Endereco { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string ContatoEmergencia { get; set; } = null!;
    public string Registro { get; set; } = null!;
    public List<string> Turmas { get; set; } = null!;
    public string Disciplina { get; set; } = null!;
    public DateOnly Contratacao { get; set; }
    public string Formacao { get; set; } = null!;
    public decimal Salario { get; set; }
}
