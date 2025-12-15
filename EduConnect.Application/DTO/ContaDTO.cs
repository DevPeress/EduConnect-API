namespace EduConnect.Application.DTO
{
    public class ContaDTO
    {
        public string Registro { get; init; } = null!;
        public string Senha { get; init; } = null!;
        public string? Cargo { get; init; }
        public bool? Lembrar { get; init; } = false;

        public ContaDTO() { }

        public ContaDTO(string registro, string senha, bool lembrar, string cargo)
        {
            Registro = registro;
            Senha = senha;
            Lembrar = lembrar;
            Cargo = cargo;
        }
    }
}
