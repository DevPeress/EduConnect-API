namespace EduConnect.Application.DTO;

public class ProfessorDTO
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime Nasc { get; set; }
    public string Endereco { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string ContatoEmergencia { get; set; } = null!;
    public string Registro { get; set; } = null!;
    public List<string> Turmas { get; set; } = null!;
    public string Disciplina { get; set; } = null!;
    public DateTime Contratacao { get; set; }
    public string Formacao { get; set; } = null!;
    public string Especializacao { get; set; } = null!;
    public decimal Salario { get; set; }
    public string HorarioAula { get; set; } = null!;
}
