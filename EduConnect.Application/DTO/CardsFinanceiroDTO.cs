namespace EduConnect.Application.DTO;

public record CardsFinanceiroDTO
{
    public required string Dado { get; init; }
    public int Number { get; init; }
    public CardsFinanceiroDTO(string dado, int number)
    {
        Dado = dado;
        Number = number;
    }
}
