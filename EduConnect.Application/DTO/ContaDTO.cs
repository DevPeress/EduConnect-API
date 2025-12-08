namespace EduConnect.Application.DTO
{
    public class ContaDTO
    {
        public string Email { get; init; } = null!;
        public string Senha { get; init; } = null!;
        public bool Lembrar { get; init; } = false;

        public ContaDTO() { }

        public ContaDTO(string email, string senha, bool lembrar)
        {
            Email = email;
            Senha = senha;
            Lembrar = lembrar;
        }
    }
}
