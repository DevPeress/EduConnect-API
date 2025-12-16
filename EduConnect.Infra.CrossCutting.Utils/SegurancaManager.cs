using System.Security.Cryptography;
using System.Text;

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

    public static string GerarSenha(int tamanho = 12)
    {
        const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&*";
        var bytes = new byte[tamanho];
        var senha = new StringBuilder(tamanho);

        RandomNumberGenerator.Fill(bytes);

        foreach (var b in bytes)
            senha.Append(caracteres[b % caracteres.Length]);

        return GerarHash(senha.ToString());
    }
}
