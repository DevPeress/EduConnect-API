using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public class ProfessorDTO : PessoaDTO
{
    public ICollection<Turma> Turmas { get; init; } = null!;
    public string Disciplina { get; init; } = null!;
    public DateOnly Contratacao { get; init; }
    public string Formacao { get; init; } = null!;
    public decimal Salario { get; init; }

    public ProfessorDTO() { }
    public ProfessorDTO(Professor dados)
    {
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        Endereco = dados.Endereco;
        Cpf = dados.Cpf;
        ContatoEmergencia = dados.ContatoEmergencia;
        Registro = dados.Registro;
        Turmas = dados.Turmas;
        Disciplina = dados.Disciplina;
        Contratacao = dados.Contratacao;
        Formacao = dados.Formacao;
        Salario = dados.Salario;
    }
}
