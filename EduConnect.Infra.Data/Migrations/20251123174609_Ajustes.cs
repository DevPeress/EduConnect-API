using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Professores",
                newName: "Registro");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Funcionarios",
                newName: "Registro");

            migrationBuilder.RenameColumn(
                name: "Matricula",
                table: "Alunos",
                newName: "Registro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Registro",
                table: "Professores",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "Registro",
                table: "Funcionarios",
                newName: "Codigo");

            migrationBuilder.RenameColumn(
                name: "Registro",
                table: "Alunos",
                newName: "Matricula");
        }
    }
}
