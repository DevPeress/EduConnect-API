using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Professores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especializacao",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "HorarioAula",
                table: "Professores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Especializacao",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HorarioAula",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
