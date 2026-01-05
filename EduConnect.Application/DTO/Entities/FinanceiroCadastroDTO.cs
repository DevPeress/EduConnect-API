namespace EduConnect.Application.DTO.Entities;

public record FinanceiroCadastroDTO
{
    public required string Registro { get; init; }
    public required string Categoria { get; init; }
    public string Metodo { get; init; } = "Cartão de Crédito";
    public string Descricao { get; init; } = default!;
    public required decimal Valor { get; init; }
    public required DateOnly DataVencimento { get; init; }
    public required bool Pago { get; init; }
    public DateOnly? DataPagamento { get; init; }
    public string? Observacoes { get; init; }
    public required string AlunoRegistro { get; init; }
}
