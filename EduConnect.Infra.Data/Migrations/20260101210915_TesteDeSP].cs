using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TesteDeSP : MigrationWithSP
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisciplinaId",
                table: "Presencas");

            migrationBuilder.CreateIndex(
                name: "IX_Presencas_AlunoId",
                table: "Presencas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Presencas_TurmaId",
                table: "Presencas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presencas_Alunos_AlunoId",
                table: "Presencas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Presencas_Turmas_TurmaId",
                table: "Presencas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Registro",
                onDelete: ReferentialAction.Cascade);

            RunSP(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presencas_Alunos_AlunoId",
                table: "Presencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Presencas_Turmas_TurmaId",
                table: "Presencas");

            migrationBuilder.DropIndex(
                name: "IX_Presencas_AlunoId",
                table: "Presencas");

            migrationBuilder.DropIndex(
                name: "IX_Presencas_TurmaId",
                table: "Presencas");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaId",
                table: "Presencas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
