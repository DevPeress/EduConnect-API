using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesEntidades : MigrationWithSP
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            RunSP(migrationBuilder);
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_TurmaRegistro",
                table: "Alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_Turmas_TurmaRegistro",
                table: "Disciplinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_TurmaRegistro",
                table: "Disciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_TurmaRegistro",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DisciplinaID",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Disciplina",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "DisciplinasRegistro",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "TurmaRegistro",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "TurmaRegistro",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurmaRegistro1",
                table: "Alunos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfessorDisciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaRegistro = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorDisciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorDisciplinas_Disciplinas_DisciplinaRegistro",
                        column: x => x.DisciplinaRegistro,
                        principalTable: "Disciplinas",
                        principalColumn: "Registro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorDisciplinas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaDisciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaRegistro = table.Column<int>(type: "int", nullable: false),
                    DisciplinaRegistro = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaDisciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinas_Disciplinas_DisciplinaRegistro",
                        column: x => x.DisciplinaRegistro,
                        principalTable: "Disciplinas",
                        principalColumn: "Registro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinas_Turmas_TurmaRegistro",
                        column: x => x.TurmaRegistro,
                        principalTable: "Turmas",
                        principalColumn: "Registro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaRegistro1",
                table: "Alunos",
                column: "TurmaRegistro1");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDisciplinas_DisciplinaRegistro",
                table: "ProfessorDisciplinas",
                column: "DisciplinaRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDisciplinas_ProfessorId",
                table: "ProfessorDisciplinas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplinas_DisciplinaRegistro",
                table: "TurmaDisciplinas",
                column: "DisciplinaRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplinas_TurmaRegistro",
                table: "TurmaDisciplinas",
                column: "TurmaRegistro");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Turmas_TurmaRegistro1",
                table: "Alunos",
                column: "TurmaRegistro1",
                principalTable: "Turmas",
                principalColumn: "Registro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Turmas_TurmaRegistro1",
                table: "Alunos");

            migrationBuilder.DropTable(
                name: "ProfessorDisciplinas");

            migrationBuilder.DropTable(
                name: "TurmaDisciplinas");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_TurmaRegistro1",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "TurmaRegistro1",
                table: "Alunos");

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaID",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Disciplina",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisciplinasRegistro",
                table: "Professores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurmaRegistro",
                table: "Disciplinas",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TurmaRegistro",
                table: "Alunos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TurmaId",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_DisciplinasRegistro",
                table: "Professores",
                column: "DisciplinasRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_TurmaRegistro",
                table: "Disciplinas",
                column: "TurmaRegistro");

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
                name: "FK_Disciplinas_Turmas_TurmaRegistro",
                table: "Disciplinas",
                column: "TurmaRegistro",
                principalTable: "Turmas",
                principalColumn: "Registro");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Disciplinas_DisciplinasRegistro",
                table: "Professores",
                column: "DisciplinasRegistro",
                principalTable: "Disciplinas",
                principalColumn: "Registro");
        }
    }
}
