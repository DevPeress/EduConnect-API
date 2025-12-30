namespace EduConnect.Infra.CrossCutting.Utils.Validations;

public class ValidationHelpers
{
    public static bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default);
    }

    public static bool BeAValidCPF(string campo)
    {
        if (campo == null) return false;

        string? cpf = campo.Clone().ToString();
        if (cpf == null) return false;

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        tempCpf = cpf[..9];
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito = resto.ToString();
        tempCpf = $"{tempCpf}{digito}";
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito = $"{digito}{resto}";

        return cpf.EndsWith(digito);
    }
}
