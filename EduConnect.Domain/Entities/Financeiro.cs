using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Financeiro
{
    [Key]
    public int Id { get; set; }
    public string Registro { get; set; }
    public int AlunoId { get; set; }
    public required string Categoria { get; set; }
    public decimal Valor { get; set; }
    public DateOnly DataVencimento { get; set; }
    public bool Pago { get; set; }
    public bool Cancelado { get; set; }
    public DateOnly? DataPagamento { get; set; }
    public string? Observacoes { get; set; }
}
