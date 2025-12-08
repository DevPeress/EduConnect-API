namespace EduConnect.Application.DTO
{
    public class ContaDTO
    {
        public string Registro { get; init; } = null!;
        public string Senha { get; init; } = null!;
        public bool? Lembrar { get; init; } = false;

        public ContaDTO() { }

        public ContaDTO(string registro, string senha, bool lembrar)
        {
            Registro = registro;
            Senha = senha;
            Lembrar = lembrar;
        }
    }
}
