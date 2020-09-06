using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoV.Migrations
{
    public partial class segundamigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_jogador",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "posicao_jogador",
                table: "Times");

            migrationBuilder.AddColumn<string>(
                name: "NumeroJogador",
                table: "Times",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosicaoJogador",
                table: "Times",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroJogador",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "PosicaoJogador",
                table: "Times");

            migrationBuilder.AddColumn<string>(
                name: "numero_jogador",
                table: "Times",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "posicao_jogador",
                table: "Times",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
