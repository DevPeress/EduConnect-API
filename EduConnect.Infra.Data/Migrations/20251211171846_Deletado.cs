using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Deletado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Turmas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Registros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Professores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Funcionarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Financeiros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Contas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Alunos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Financeiros");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Alunos");
        }
    }
}
