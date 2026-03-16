using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTO.Entities;

public class NotasDTO
{
    public required string Registro { get; set; }
    public int Nota { get; set; }
    public TiposAvalicaoes TipoAvaliacao { get; set; }
    public required string Materia { get; set; }
    public required string Professor { get; set; }
    public required DateTime Data { get; set; }
}