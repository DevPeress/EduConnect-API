using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;
public class AlunoDTO : PessoaDTO
{
    public string Turma { get; init; } = null!;
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
        Turma = dados.Turma;
        Media = dados.Media;
        DataMatricula = dados.DataMatricula;
    }
}
