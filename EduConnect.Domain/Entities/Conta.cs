using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Conta
{
    [Key]
    public int Id { get; set; }
    public string Registro { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Cargo { get; set; } = null!;
    public bool Deletado { get; set; } = false;
    public int LimiteLogin { get; set; } = 0;
    public DateTime? DataLogin { get; set; }

    // Relacionamento
    public Pessoa Pessoa { get; set; } = null!;
}   
