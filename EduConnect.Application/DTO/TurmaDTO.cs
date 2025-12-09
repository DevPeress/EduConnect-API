using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO;
public record TurmaDTO
{
    public int Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public int ProfessorID { get; set; }
    public List<int> Alunos { get; set; } = [];
    public int SalaID { get; set; }
    public int DisciplinaID { get; set; }
    public required string Horario { get; set; }
    public int Capacidade { get; set; }
    public DateOnly AnoLetivo { get; set; }
    public DateOnly DataCriacao { get; set; }
    public string Status { get; set; } = "Ativa";

    public TurmaDTO() { }

    public TurmaDTO(Turma dados)
    {
        Nome = dados.Nome;
        Turno = dados.Turno;
        ProfessorID = dados.ProfessorID;
        Alunos = dados.Alunos;
        SalaID = dados.SalaID;
        DisciplinaID = dados.DisciplinaID;
        Horario = dados.Horario;
        Capacidade = dados.Capacidade;
        AnoLetivo = dados.AnoLetivo;
        DataCriacao = dados.DataCriacao;
        Status = dados.Status;
    }
}
