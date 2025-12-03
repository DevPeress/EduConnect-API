using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;

public record FinanceiroDTO
{
    public int Registro { get; init; }
    public int AlunoId { get; init; }
    public string Categoria { get; init; } = default!;
    public decimal Valor { get; init; }
    public DateOnly DataVencimento { get; init; }
    public bool Pago { get; init; }
    public DateOnly? DataPagamento { get; init; }
    public bool Cancelado { get; init; }

    // Dados opcionais relacionados ao aluno (preenchidos no serviço, não na entidade)
    public string? Aluno { get; init; }
    public DateOnly? Nasc { get; init; }
    public string? Status { get; init; }

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
