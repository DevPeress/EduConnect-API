namespace EduConnect.Application.DTO;

public class FuncionarioDTO
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
    public string Cargo { get; set; } = null!;
    public DateTime DataAdmissao { get; set; }
    public decimal Salario { get; set; }
    public string Departamento { get; set; } = null!;
    public string Supervisor { get; set; } = null!;
    public string Turno { get; set; } = null!;
}
