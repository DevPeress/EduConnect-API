using EduConnect.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

public abstract class MigrationWithSP : Migration
{
    public void RunSP(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.ExecuteStoredProcedures();
    }
}
