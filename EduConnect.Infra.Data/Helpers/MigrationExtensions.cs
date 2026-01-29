using Microsoft.EntityFrameworkCore.Migrations;

namespace EduConnect.Infra.Data.Helpers;

public static class MigrationExtensions
{
    public static void ExecuteStoredProcedures(
        this MigrationBuilder migrationBuilder)
    {
        var basePath = Path.Combine(AppContext.BaseDirectory, "StoredProcedures");
        Console.WriteLine(basePath);

        if (!Directory.Exists(basePath))
            return;
        
        var files = Directory.GetFiles(basePath, "*.sql", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var sql = File.ReadAllText(file);
            Console.WriteLine($"Executando SP: {Path.GetFileName(file)}");
            migrationBuilder.Sql(sql);
        }
    }
}
