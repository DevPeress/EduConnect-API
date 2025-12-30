using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Registros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Registros",
                newName: "UserRole");

            migrationBuilder.RenameColumn(
                name: "PessoaId",
                table: "Registros",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "Horario",
                table: "Registros",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Registros",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Detalhes",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Entity",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Registros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataLogin",
                table: "Contas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalhes",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "Entity",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Registros");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Registros");

            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Registros",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Registros",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Registros",
                newName: "Horario");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "Registros",
                newName: "PessoaId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataLogin",
                table: "Contas",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
