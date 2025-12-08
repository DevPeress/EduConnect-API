using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Conta
{
    [Key]
    public int Id { get; set; }
    public string Registro { get; set; } = null!;
    public string Senha { get; set; } = null!;
}
