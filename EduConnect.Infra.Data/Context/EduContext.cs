using EduConnect.Domain;
using EduConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public class EduContext(DbContextOptions<EduContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Financeiro> Financeiros { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Conta> Contas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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