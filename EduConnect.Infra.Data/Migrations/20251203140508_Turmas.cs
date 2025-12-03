using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Turmas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Financeiros",
                newName: "Registro");

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Registro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alunos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisciplinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    AnoLetivo = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Registro);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.RenameColumn(
                name: "Registro",
                table: "Financeiros",
                newName: "Id");
        }
    }
}
