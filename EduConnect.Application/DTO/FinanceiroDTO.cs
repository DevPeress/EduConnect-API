namespace EduConnect.Application.DTO;

public class FinanceiroDTO
{
    public Guid Registro { get; set; }
    public Guid AlunoId { get; set; }
    public required string Categoria { get; set; }
    public decimal Valor { get; set; }
    public DateOnly DataVencimento { get; set; }
    public bool Pago { get; set; }
    public DateOnly? DataPagamento { get; set; }
    public string? Aluno { get; set; }
    public DateOnly? Nasc { get; set; }
    public bool Cancelado { get; set; }
}
