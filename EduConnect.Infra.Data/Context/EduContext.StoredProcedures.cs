using EduConnect.Application.DTO.StoredProcedures;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public partial class EduContext
{
    private List<Type> Lista = new List<Type> { 
        typeof(GetTotalDashBoard),
        typeof(GetAumentoDashBoard)
    };
    public DbSet<GetTotalDashBoard> GetTotalDashBoard { get; set; }
    public DbSet<GetAumentoDashBoard> DashboardTotais { get; set; }

    partial void OnModelCreatingStoredProcedures(ModelBuilder modelBuilder)
    {
        foreach (var type in Lista)
        {
            modelBuilder.Entity(type).HasNoKey().ToView(null);
        }
    }
}
