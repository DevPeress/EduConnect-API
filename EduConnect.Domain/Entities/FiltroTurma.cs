
namespace EduConnect.Domain.Entities;
public class FiltroTurma : FiltroBase
{
    public required string Ano { get; set; }
    public required string Turno { get; set; }
    public required string Status { get; set; }
}
