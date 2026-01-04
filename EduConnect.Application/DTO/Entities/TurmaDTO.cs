using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public record TurmaDTO
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required DateTime Inicio { get; set; }
    public required DateTime Fim { get; set; }
    public required string Sala { get; set; }
    public required string Status { get; set; }
    public TurmaDTO() { }
    public TurmaDTO(Turma turma)
    {
        Registro = turma.Registro;
        Nome = turma.Nome;
        Turno = turma.Turno;
        Inicio = turma.Inicio;
        Fim = turma.Fim;
        Sala = turma.Sala;
        Status = turma.Status;
    }
}
