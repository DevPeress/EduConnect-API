using EduConnect.Application.DTO.StoredProcedures;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public partial class EduContext
{
    private readonly List<Type> Lista = [ 
        typeof(GetTotalDashBoard),
        typeof(GetAumentoDashBoard),
        typeof(GetPorcentagemDashBoard)
    ];
    public DbSet<GetTotalDashBoard> GetTotalDashBoard { get; set; }
    public DbSet<GetAumentoDashBoard> GetAumentoDashBoard { get; set; }
    public DbSet<GetPorcentagemDashBoard> GetPorcentagemDashBoard { get; set; }

    partial void OnModelCreatingStoredProcedures(ModelBuilder modelBuilder)
    {
        foreach (var type in Lista)
        {
            modelBuilder.Entity(type).HasNoKey().ToView(null);
        }
    }
}
