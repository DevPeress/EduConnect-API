
namespace EduConnect.Domain.Entities;
public class FiltroPessoa : FiltroBase
{
    public required string Ano { get; set; }
    public required string Categoria { get; set; }
    public required string Status { get; set; }
}
