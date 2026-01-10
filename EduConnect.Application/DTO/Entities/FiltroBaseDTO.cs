namespace EduConnect.Application.DTO.Entities;

public class FiltroBaseDTO
{
    public int Page { get; set; } = 1;
    public required string Pesquisa { get; set; }
}
