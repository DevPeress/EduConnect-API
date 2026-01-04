using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;
public class AlunoDTO : PessoaDTO
{
    public string? TurmaRegistro { get; init; } 
    public AlunoDTO() { }
    public AlunoDTO(Aluno dados)
    {
        Registro = dados.Registro;
        Nome = dados.Nome;
        Email = dados.Email;
        Telefone = dados.Telefone;
        Status = dados.Status;
        Nasc = dados.Nasc;
        TurmaRegistro = dados.TurmaRegistro;
        Foto = dados.Foto;
    }
}
