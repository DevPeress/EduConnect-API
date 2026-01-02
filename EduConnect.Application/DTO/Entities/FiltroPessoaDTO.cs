using EduConnect.Domain.Entities;

namespace EduConnect.Application.DTO.Entities;

public class FiltroPessoaDTO : FiltroBase
{
    public required int Ano { get; set; }
    public required string Categoria { get; set; }
    public required string Status { get; set; }
}
