namespace EduConnect.ViewModels;

public class FuncionarioResponseViewModel
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required DateOnly Nasc { get; set; }
    public required string Cargo { get; set; }
    public required string Departamento { get; set; }
    public DateOnly DataAdmissao { get; set; }
    public required string Status { get; set; }
    public required string Foto { get; set; }
    public required string Telefone { get; set; }
}
