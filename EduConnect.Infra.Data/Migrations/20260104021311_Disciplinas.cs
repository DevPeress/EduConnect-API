using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Disciplinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turmas",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Turma",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Horario",
                table: "Turmas",
                newName: "Sala");

            migrationBuilder.AddColumn<string>(
                name: "Fim",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Inicio",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisciplinasRegistro",
                table: "Professores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TurmaId",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Registro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false),
                    TurmaRegistro = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Registro);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Turmas_TurmaRegistro",
                        column: x => x.TurmaRegistro,
                        principalTable: "Turmas",
                        principalColumn: "Registro");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professores_DisciplinasRegistro",
                table: "Professores",
                column: "DisciplinasRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_TurmaRegistro",
                table: "Disciplinas",
                column: "TurmaRegistro");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinasRegistro",
                table: "Professores",
                column: "DisciplinasRegistro",
                principalTable: "Disciplinas",
                principalColumn: "Registro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Professores_DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Sala",
                table: "Turmas",
                newName: "Horario");

            migrationBuilder.AddColumn<string>(
                name: "Turmas",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Turma",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
