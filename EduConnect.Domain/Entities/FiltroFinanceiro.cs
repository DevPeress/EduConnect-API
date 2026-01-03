
namespace EduConnect.Domain.Entities;
public class FiltroFinanceiro : FiltroBase
{
    public required string Status { get; set; }
    public required string Pagamento { get; set; }
    public required string Categoria { get; set; }
    public required string Meses { get; set; }
}
