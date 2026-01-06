namespace EduConnect.Application.DTO.Entities;

public class FiltroRegistroDTO : FiltroBaseDTO
{
    public required string Ano { get; set; }
    public required string Categoria { get; set; }
    public required string Status { get; set; }
}
