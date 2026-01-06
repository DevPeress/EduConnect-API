namespace EduConnect.ViewModels;

public class DisciplinasResponseViewModel
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required DateOnly DataCriacao { get; set; }
}
