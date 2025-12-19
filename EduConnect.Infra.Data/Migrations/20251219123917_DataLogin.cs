using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DataLogin",
                table: "Contas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LimiteLogin",
                table: "Contas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLogin",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "LimiteLogin",
                table: "Contas");
        }
    }
}
