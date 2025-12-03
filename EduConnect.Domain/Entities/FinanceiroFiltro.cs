namespace EduConnect.Domain.Entities;

public class FinanceiroFiltro : FiltroBase
{
    public string Categoria { get; set; } = "Todas as Categorias";
    public string Status { get; set; } = "Todos os Status";
    public string Data { get; set; } = "Todas as Datas";
}
