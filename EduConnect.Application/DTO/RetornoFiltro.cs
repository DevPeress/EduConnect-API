namespace EduConnect.Application.DTO;
public record RetornoFiltro<T>
{
    public int Total { get; init; }
    public List<T> Dados { get; init; } = new();
}
