using EduConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities;

public class Notas
{
    [Key]
    public int Id { get; set; }
    public required int Nota { get; set; }
    public required bool Deletado { get; set; }
    public required DateTime Data { get; set; }
    public required TiposAvalicaoes TipoAvalicao { get; set; }

    // Relacionamento com Aluno
    public required string AlunoRegistro { get; set; }
    public required Aluno Aluno { get; set; }

    public required int MateriaId { get; set; }
    public required int ProfessorId { get; set; }
}
