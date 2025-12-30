using Bogus;
using Bogus.Extensions.Brazil;
using EduConnect.Infra.CrossCutting.Utils.Validations;
using FluentAssertions;
using Xunit;

namespace EduConnect.Tests.CrossCutting.Utils;

public class ValidationHelpersTest
{
    private readonly Faker faker = new();

    [Fact(DisplayName = "Data Válida")]
    public void DataValida()
    {
        ValidationHelpers.BeAValidDate(faker.Date.Recent(1)).Should().BeTrue();
    }

    [Fact(DisplayName = "Data Invalida")]
    public void DataInvalida()
    {
        ValidationHelpers.BeAValidDate(DateTime.MinValue).Should().BeFalse();
    }

    [Fact(DisplayName = "CPF Válido")]
    public void CpfValido()
    {
        ValidationHelpers.BeAValidCPF(faker.Person.Cpf()).Should().BeTrue();
    }

    [Fact(DisplayName = "CPF Invalido")]
    public void CpfInvalido()
    {
        var cpfInvalido = "012.345.678-99";
        ValidationHelpers.BeAValidCPF(cpfInvalido).Should().BeFalse();
    }

}
