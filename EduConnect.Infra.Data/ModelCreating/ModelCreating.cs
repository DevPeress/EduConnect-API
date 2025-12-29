using EduConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.ModelCreating
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureModels(this ModelBuilder modelBuilder)
        {
            var tipos = new List<Type>
            {
                typeof(Aluno),
                typeof(Professor),
                typeof(Funcionario)
            };

            // Impede que existam 2 tipos com a mesma matrícula
            foreach (var tipo in tipos)
            {
                modelBuilder.Entity(tipo)
                    .HasOne(typeof(Conta), "Conta")
                    .WithOne()
                    .HasForeignKey(tipo, "ContaId")
                    .OnDelete(DeleteBehavior.Restrict);
            }

            // CONFIGURAÇÃO GLOBAL PARA TODOS OS DECIMAIS
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var decimalProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

                foreach (var property in decimalProperties)
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }
        }
    }
}