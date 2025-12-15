namespace EduConnect.ViewModels;

public class FiltroResponseViewModel <T>
{
    public required List<T> Dados { get; set; }
    public int Total { get; set; }
}
