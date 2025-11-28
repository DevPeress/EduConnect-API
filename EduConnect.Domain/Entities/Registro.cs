namespace EduConnect.Domain.Entities;

public class Registro
{
    public required Guid Id { get; set; }
    public required string Tipo { get; set; }
    public required string Descricao { get; set; }
    public required DateTime Horario { get; set; }
    public required Guid PessoaId { get; set; }
}
