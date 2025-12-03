using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Registro
{
    [Key]
    public int Id { get; set; }
    public required string Tipo { get; set; }
    public required string Descricao { get; set; }
    public required DateTime Horario { get; set; }
    public required int PessoaId { get; set; }
}
