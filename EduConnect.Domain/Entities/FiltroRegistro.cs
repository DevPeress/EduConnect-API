
namespace EduConnect.Domain.Entities;
public class FiltroRegistro : FiltroBase
{
    public required string Ano { get; set; }
    public required string Categoria { get; set; }
    public required string Status { get; set; }
}
