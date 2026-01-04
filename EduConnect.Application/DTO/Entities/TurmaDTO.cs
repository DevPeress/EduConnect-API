using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTO.Entities;
public record TurmaDTO
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required string Inicio { get; set; }
    public required string Fim { get; set; }
    public required string Sala { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
    public DateOnly DataCriacao { get; set; }
    public required string Status { get; set; }
    public bool Deletado { get; set; } 
    public int ProfessorID { get; set; }
    public Professor Professor { get; set; } = null!;
    public ICollection<Aluno> Alunos { get; set; } = [];
    public ICollection<TurmaDisciplina> Disciplinas { get; set; } = [];

    public TurmaDTO() { }

    public TurmaDTO(Turma dados)
    {
        Registro = dados.Registro;
        Nome = dados.Nome;
        Turno = dados.Turno;
        Inicio = dados.Inicio;
        Fim = dados.Fim;
        Sala = dados.Sala;
        Capacidade = dados.Capacidade;
        AnoLetivo = dados.AnoLetivo;
        DataCriacao = dados.DataCriacao;
        Status = dados.Status;
        Deletado = dados.Deletado;
        ProfessorID = dados.ProfessorId;
        Alunos = dados.Alunos;
        Disciplinas = dados.TurmaDisciplinas;
    }
}
