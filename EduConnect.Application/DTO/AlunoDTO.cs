namespace EduConnect.Application.DTO;

public class AlunoDTO
{
    public string Registro { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateOnly Nasc { get; set; }
    public string Endereco { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string ContatoEmergencia { get; set; } = null!;
    public string Turma { get; set; } = null!;
    public int Media { get; set; }
    public DateOnly DataMatricula { get; set; }
}
