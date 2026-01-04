namespace EduConnect.ViewModels;

public class FinanceiroResponseViewModel
{
    public int Registro { get; init; }
    public required string Aluno { get; init; }
    public required DateOnly Nasc { get; init; }
    public required string Categoria { get; init; }
    public decimal Valor { get; init; }
    public DateOnly DataVencimento { get; init; }
    public DateOnly? DataPagamento { get; init; }
    public required string Status { get; init; }
    public required string Mes { get; init; }
    public required string Foto { get; init; }
}
