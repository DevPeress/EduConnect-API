using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IPessoaComConta
{
    int ContaId { get; set; }
    Conta Conta { get; set; }
}
