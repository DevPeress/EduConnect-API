using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public class FiltroFinanceiroDTO : FiltroBase
{
    public required string Ano { get; set; }
    public required string Turno { get; set; }
    public required string Status { get; set; }
}
