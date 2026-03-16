namespace EduConnect.Application.DTO.Entities;

public class NotasUpdateDTO
{
    public required int Registro { get; set; }
    public int Nota { get; set; }
    public required string Materia { get; set; }
    public required string Professor { get; set; }
}