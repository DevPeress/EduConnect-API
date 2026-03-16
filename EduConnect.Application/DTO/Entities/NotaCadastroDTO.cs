namespace EduConnect.Application.DTO.Entities;

public class NotasCadastroDTO
{
    public int Nota { get; set; }
    public required string Materia { get; set; }
    public required string Aluno { get; set; }
}