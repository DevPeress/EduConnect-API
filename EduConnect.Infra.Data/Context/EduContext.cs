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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var tipos = new List<Type>
        {
            typeof(Aluno),
            typeof(Professor),
            typeof(Funcionario)
        };

        // 🔒 Impede que existam 2 tipos com a mesma matrícula
        foreach (var tipo in tipos)
        {
            modelBuilder.Entity(tipo)
                .HasIndex("Registro")
            .IsUnique();
        }
    }
}