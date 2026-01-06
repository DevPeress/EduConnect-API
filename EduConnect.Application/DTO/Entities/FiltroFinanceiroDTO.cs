namespace EduConnect.Application.DTO.Entities;

public class FiltroFinanceiroDTO : FiltroBaseDTO
{
    public required string Status { get; set; }
    public required string Categoria { get; set; }
    public required string Meses { get; set; }
}
