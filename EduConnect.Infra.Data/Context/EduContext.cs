using EduConnect.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public class EduContext (DbContextOptions<EduContext> options) : DbContext(options)
{
    public virtual DbSet<Aluno> Alunos { get; set; }
    public virtual DbSet<Professor> Professores { get; set; }
    public virtual DbSet<Funcionario> Funcionarios { get; set; }
    }
