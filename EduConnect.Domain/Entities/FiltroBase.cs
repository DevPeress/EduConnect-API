namespace EduConnect.Domain.Entities;
public class FiltroBase
{
    public int Limit { get; set; } = 6;
    public int Offset => CalcularOffset();
    public int Page { get; set; } = 1;

    int CalcularOffset()
    {
        return (Page == 1) ? 0 : (Page - 1) * Limit;
    }

    public void AlterarLimit(int novoLimit)
    {
        Limit = novoLimit;
    }
}
