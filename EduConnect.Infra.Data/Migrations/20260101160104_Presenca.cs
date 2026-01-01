using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Presenca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alunos",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "SalaID",
                table: "Turmas");

            migrationBuilder.RenameColumn(
                name: "ProfessorID",
                table: "Turmas",
                newName: "ProfessorId");

            migrationBuilder.AddColumn<int>(
                name: "TurmaRegistro",
                table: "Alunos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Presencas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false),
                    Justificada = table.Column<bool>(type: "bit", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presencas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaRegistro",
                table: "Alunos",
                column: "TurmaRegistro");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_TurmaRegistro",
                table: "Alunos",
                column: "TurmaRegistro",
                principalTable: "Turmas",
                principalColumn: "Registro");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_TurmaRegistro",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropTable(
                name: "Presencas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_TurmaRegistro",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "TurmaRegistro",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "Turmas",
                newName: "ProfessorID");

            migrationBuilder.AddColumn<string>(
                name: "Alunos",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalaID",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
