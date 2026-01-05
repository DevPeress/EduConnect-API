using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Financeiro
{
    [Key]
    public int Id { get; set; }
    public required string Registro { get; set; }
    public required string Categoria { get; set; }
    public string Metodo { get; init; } = "Cartão de Crédito";
    public string Descricao { get; init; } = default!;
    public required decimal Valor { get; set; }
    public required DateOnly DataVencimento { get; set; }
    public bool Pago { get; set; }
    public bool Cancelado { get; set; }
    public bool Deletado { get; set; } = false;
    public DateOnly? DataPagamento { get; set; }
    public string? Observacoes { get; set; }

    // Relacionamento
    public required string AlunoRegistro { get; set; }
    public Aluno Aluno { get; set; } = null!;
}
