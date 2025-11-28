namespace EduConnect.Application.DTO;
public class RegistroDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Tipo { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public DateTime Horario { get; set; } = DateTime.Now;
}
