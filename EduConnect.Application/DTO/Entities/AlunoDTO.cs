using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;
public class AlunoDTO : PessoaDTO
{
    public string? TurmaRegistro { get; init; } 
    public int Media { get; init; }
    public DateOnly DataMatricula { get; init; }
    public AlunoDTO() { }
    public AlunoDTO(Aluno dados)
    {
        Registro = dados.Registro;
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        Endereco = dados.Endereco;
        Cpf = dados.Cpf;
        ContatoEmergencia = dados.ContatoEmergencia;
        TurmaRegistro = dados.TurmaRegistro;
        Media = dados.Media;
        DataMatricula = dados.DataMatricula;
        Foto = dados.Foto;
    }
}
