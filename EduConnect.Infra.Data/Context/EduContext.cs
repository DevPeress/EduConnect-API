using EduConnect.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public class EduContext(DbContextOptions<EduContext> options) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
}
