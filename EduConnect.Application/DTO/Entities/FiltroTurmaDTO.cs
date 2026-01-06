namespace EduConnect.Application.DTO.Entities;

public class FiltroTurmaDTO : FiltroBaseDTO
{
    public required string Ano { get; set; }
    public required string Turno { get; set; }
    public required string Status { get; set; }
}
