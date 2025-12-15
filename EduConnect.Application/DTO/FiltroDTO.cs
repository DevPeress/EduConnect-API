using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public class FiltroDTO : FiltroBase
{
    public string? Categoria { get; set; }
    public string? Data { get; set; }
    public string? Status { get; set; }
    public string? Turno { get; set; }
}
