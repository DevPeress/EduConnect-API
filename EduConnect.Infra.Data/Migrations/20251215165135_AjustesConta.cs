using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustesConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professores",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Contas");

            migrationBuilder.RenameTable(
                name: "Professores",
                newName: "Pessoa");

            migrationBuilder.RenameIndex(
                name: "IX_Professores_Registro",
                table: "Pessoa",
                newName: "IX_Pessoa_Registro");

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

            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Pessoa",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Pessoa",
                type: "nvarchar(max)",
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
                name: "IX_Pessoa_ContaId",
                table: "Pessoa",
                column: "ContaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_ContaId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "ContaId",
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
                name: "Foto",
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
                name: "IX_Pessoa_Registro",
                table: "Professores",
                newName: "IX_Professores_Registro");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Contas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Contas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Turmas",
                table: "Professores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
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
                    ContatoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataMatricula = table.Column<DateOnly>(type: "date", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media = table.Column<int>(type: "int", nullable: false),
                    Nasc = table.Column<DateOnly>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turma = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContatoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdmissao = table.Column<DateOnly>(type: "date", nullable: false),
                    Deletado = table.Column<bool>(type: "bit", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nasc = table.Column<DateOnly>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Registro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supervisor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Registro",
                table: "Alunos",
                column: "Registro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_Registro",
                table: "Funcionarios",
                column: "Registro",
                unique: true);
        }
    }
}
