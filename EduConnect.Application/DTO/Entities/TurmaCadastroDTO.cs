using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;
public record TurmaCadastroDTO
{
    public required string Registro { get; set; }
    public required string Nome { get; set; }
    public required string Turno { get; set; }
    public required string Status { get; set; }
    public required DateOnly AnoEletivo { get; set; }
    public required int Capacidade { get; set; }
    public required int ProfessorResponsavel { get; set; }
    public required DateTime Inicio { get; set; }
    public required DateTime Fim { get; set; }
    public required string Sala { get; set; }
    public required string Dias { get; set; }
}
