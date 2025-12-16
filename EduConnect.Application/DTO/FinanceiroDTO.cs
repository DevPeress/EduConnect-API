using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public record FinanceiroDTO
{
    public int Registro { get; init; }
    public int AlunoId { get; init; }
    public string Categoria { get; init; } = default!;
    public string Metodo { get; init; } = default!;
    public string Descricao { get; init; } = default!;
    public decimal Valor { get; init; }
    public DateOnly DataVencimento { get; init; }
    public bool Pago { get; init; }
    public DateOnly? DataPagamento { get; init; }
    public bool Cancelado { get; init; }
    public string? Observacoes { get; init; }

    public FinanceiroDTO() { }

    public FinanceiroDTO(Financeiro u)
    {
        Registro = u.Registro;
        AlunoId = u.AlunoId;
        Categoria = u.Categoria;
        Valor = u.Valor;
        DataVencimento = u.DataVencimento;
        Pago = u.Pago;
        DataPagamento = u.DataPagamento;
        Cancelado = u.Cancelado;
    }
}
