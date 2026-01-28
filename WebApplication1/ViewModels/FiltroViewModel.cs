namespace EduConnect.ViewModels;

public record FiltroViewModel
{
    public string Categoria { get; set; } = "";
    public string Status { get; set; } = "";
    public int Page { get; set; } = 1;
    public string Ano { get; set; } = "";
    public string Pesquisa { get; set; } = "";
    public string Data { get; set; } = "";
    public string Selecionada { get; set; } = "";
    public string Turno { get; set; } = "";
}
