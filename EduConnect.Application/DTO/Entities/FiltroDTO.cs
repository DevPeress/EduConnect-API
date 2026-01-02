using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public class FiltroDTO : FiltroBase
{
    public string? Ano { get; set; }
    public string? Categoria { get; set; }
    public string? Data { get; set; }
    public string? Status { get; set; }
    public string? Turno { get; set; }
    public string? Sala { get; set; }
}
