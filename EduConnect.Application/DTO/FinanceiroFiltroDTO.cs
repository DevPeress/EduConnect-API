using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public class FinanceiroFiltroDTO : FiltroBase
{
    public string Categoria { get; set; } = "Todas as Categorias";
    public string Status { get; set; } = "Todos os Status";
    public string Data { get; set; } = "Todas as Datas";
}
