using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_Registro",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "DataAdmissao",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "DataMatricula",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Funcionario_Salario",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Media",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Turma",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Turno",
                table: "Pessoa");

            migrationBuilder.RenameTable(
                name: "Pessoa",
                newName: "Professores");

            migrationBuilder.RenameIndex(
                name: "IX_Pessoa_ContaId",
                table: "Professores",
                newName: "IX_Professores_ContaId");

            migrationBuilder.AlterColumn<string>(
                name: "Turmas",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "Professores",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Registro",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Formacao",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disciplina",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contratacao",
                table: "Professores",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professores",
                table: "Professores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Turma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media = table.Column<int>(type: "int", nullable: false),
                    DataMatricula = table.Column<DateOnly>(type: "date", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nasc = table.Column<DateOnly>(type: "date", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContatoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContaId = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdmissao = table.Column<DateOnly>(type: "date", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supervisor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nasc = table.Column<DateOnly>(type: "date", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContatoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_ContaId",
                table: "Alunos",
                column: "ContaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ContaId",
                table: "Funcionarios",
                column: "ContaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Contas_ContaId",
                table: "Professores",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Contas_ContaId",
                table: "Professores");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professores",
                table: "Professores");

            migrationBuilder.RenameTable(
                name: "Professores",
                newName: "Pessoa");

            migrationBuilder.RenameIndex(
                name: "IX_Professores_ContaId",
                table: "Pessoa",
                newName: "IX_Pessoa_ContaId");

            migrationBuilder.AlterColumn<string>(
                name: "Turmas",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "Pessoa",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Registro",
                table: "Pessoa",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Formacao",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Disciplina",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Contratacao",
                table: "Pessoa",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataAdmissao",
                table: "Pessoa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataMatricula",
                table: "Pessoa",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departamento",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pessoa",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Funcionario_Salario",
                table: "Pessoa",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Media",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Turma",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Turno",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Registro",
                table: "Pessoa",
                column: "Registro",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
