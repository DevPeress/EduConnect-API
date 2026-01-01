using EduConnect.Domain.Entities;
using EduConnect.Infra.Data.ModelCreating;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public partial class EduContext(DbContextOptions<EduContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Financeiro> Financeiros { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Presenca> Presencas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureModels();

        OnModelCreatingStoredProcedures(modelBuilder);
    }

    partial void OnModelCreatingStoredProcedures(ModelBuilder modelBuilder);
}
