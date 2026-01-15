using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public record TurmaDTO
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required string Horario { get; set; }
    public required string Professor{ get; set; }
    public required string Sala { get; set; }
    public required int Capacidade { get; set; }
    public TurmaDTO() { }
    public TurmaDTO(Turma turma)
    {
        Registro = turma.Registro;
        Nome = turma.Nome;
        Turno = turma.Turno;
        Professor = turma.ProfessorResponsavel;
        Sala = turma.Sala;
        Capacidade = turma.Capacidade;
    }
}
