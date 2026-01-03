using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public class FiltroRegistroDTO : FiltroBase
{
    public required string Ano { get; set; }
    public required string Categoria { get; set; }
    public required string Status { get; set; }
}
