using EduConnect.Domain.Entities;

namespace EduConnect.Domain.Interfaces;

public interface IFuncionarioRepository : IPessoaRepository<Funcionario>
{
    Task<(List<string>, List<string>)> GetInformativos()
}