using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Conta
{
    [Key]
    public int Id { get; set; }
    public string Registro { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Foto { get; set; } = "https://images.pexels.com/photos/774909/pexels-photo-774909.jpeg?auto=compress&cs=tinysrgb&w=100";
    public string Cargo { get; set; } = null!;
    public bool Deletado { get; set; } = false;
}   
