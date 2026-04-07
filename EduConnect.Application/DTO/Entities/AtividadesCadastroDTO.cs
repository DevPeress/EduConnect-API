namespace EduConnect.Application.DTO.Entities;

public class AtividadesCadastroDTO
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required DateTime DataEntrega { get; set; }
    public required string TurmaId { get; set; }
}