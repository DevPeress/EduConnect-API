using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContaPessoa_OneToOne_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Contas_ContaId",
                table: "Pessoa",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
