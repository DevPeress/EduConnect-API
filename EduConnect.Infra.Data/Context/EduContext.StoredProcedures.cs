using EduConnect.Application.DTO.StoredProcedures;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infra.Data.Context;

public partial class EduContext
{
    public DbSet<DashboardTotaisDto> DashboardTotais { get; set; }

    partial void OnModelCreatingStoredProcedures(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DashboardTotaisDto>(entity =>
        {
            entity.HasNoKey();
            entity.ToView(null);
        });
    }
}
