using EduConnect.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public class EduContext(DbContextOptions<EduContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public virtual DbSet<Funcionario> Funcionarios { get; set; }
    private readonly string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EduConnect;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
