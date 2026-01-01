using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTO.Entities;
public record TurmaDTO
{
    public int Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required string Horario { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
    public DateOnly DataCriacao { get; set; }
    public required string Status { get; set; }
    public bool Deletado { get; set; } 
    public int ProfessorID { get; set; }
    public ICollection<Aluno> Alunos { get; set; } = [];
    public int DisciplinaID { get; set; }

    public TurmaDTO() { }

    public TurmaDTO(Turma dados)
    {
        Registro = dados.Registro;
        Nome = dados.Nome;
        Turno = dados.Turno;
        Horario = dados.Horario;
        Capacidade = dados.Capacidade;
        AnoLetivo = dados.AnoLetivo;
        DataCriacao = dados.DataCriacao;
        Status = dados.Status;
        Deletado = dados.Deletado;
        ProfessorID = dados.ProfessorId;
        Alunos = dados.Alunos;
        DisciplinaID = dados.DisciplinaID;
    }
}
