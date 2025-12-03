using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain;

public class Pessoa
{
    [Key]
    public int Id { get; set; }
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
    public required string Status { get; set; }
    public required DateOnly Nasc { get; set; }
    public required string Endereco { get; set; }
    public required string Cpf { get; set; }
    public required string ContatoEmergencia { get; set; }
}
