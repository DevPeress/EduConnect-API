namespace EduConnect.Application.DTO;
public record CardsAdminDto
{
    public required string Dado { get; init; }
    public double Total { get; init; }
    public double Aumento { get; init; }
    public double Porcentagem { get; init; }
}
