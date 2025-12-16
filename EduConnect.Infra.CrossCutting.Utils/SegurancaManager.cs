namespace EduConnect.Infra.CrossCutting.Utils;

public class SegurancaManager
{
    public static string GerarHash(string valor)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
        return BCrypt.Net.BCrypt.HashPassword(valor, salt);
    }

    public static bool VerificarHash(string valor, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(valor, hash);
    }
}
