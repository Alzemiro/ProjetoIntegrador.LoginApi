using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoV.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gols",
                columns: table => new
                {
                    GolId = table.Column<Guid>(nullable: false),
                    MinutoGol = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gols", x => x.GolId);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeId = table.Column<Guid>(nullable: false),
                    numero_jogador = table.Column<string>(nullable: true),
                    posicao_jogador = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    PartidaId = table.Column<Guid>(nullable: false),
                    mandantePartida = table.Column<string>(nullable: true),
                    LocalPartida = table.Column<string>(nullable: true),
                    DataHoraPartida = table.Column<DateTime>(nullable: false),
                    placarFinal = table.Column<int>(nullable: false),
                    GolId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.PartidaId);
                    table.ForeignKey(
                        name: "FK_Partidas_Gols_GolId",
                        column: x => x.GolId,
                        principalTable: "Gols",
                        principalColumn: "GolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    JogadorId = table.Column<Guid>(nullable: false),
                    NomeJogador = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    GolId = table.Column<Guid>(nullable: false),
                    TimeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.JogadorId);
                    table.ForeignKey(
                        name: "FK_Jogadores_Gols_GolId",
                        column: x => x.GolId,
                        principalTable: "Gols",
                        principalColumn: "GolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogadores_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clubes",
                columns: table => new
                {
                    ClubeId = table.Column<Guid>(nullable: false),
                    ClubeNome = table.Column<string>(nullable: true),
                    ClubeCidade = table.Column<string>(nullable: true),
                    ClubeEstadio = table.Column<string>(nullable: true),
                    TimeId = table.Column<Guid>(nullable: false),
                    PartidaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubes", x => x.ClubeId);
                    table.ForeignKey(
                        name: "FK_Clubes_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "PartidaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clubes_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rodadas",
                columns: table => new
                {
                    RodadaId = table.Column<Guid>(nullable: false),
                    ClubeNome = table.Column<string>(nullable: true),
                    DataInicioRodada = table.Column<DateTime>(nullable: false),
                    DataFimRodada = table.Column<DateTime>(nullable: false),
                    PartidaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rodadas", x => x.RodadaId);
                    table.ForeignKey(
                        name: "FK_Rodadas_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "PartidaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temporadas",
                columns: table => new
                {
                    TemporadaId = table.Column<Guid>(nullable: false),
                    Etapa = table.Column<int>(nullable: false),
                    PartidaId = table.Column<Guid>(nullable: false),
                    ClubeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporadas", x => x.TemporadaId);
                    table.ForeignKey(
                        name: "FK_Temporadas_Clubes_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "Clubes",
                        principalColumn: "ClubeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Temporadas_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "PartidaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campeonato",
                columns: table => new
                {
                    CampeonatoId = table.Column<Guid>(nullable: false),
                    NomeCampeonato = table.Column<string>(nullable: true),
                    TemporadaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campeonato", x => x.CampeonatoId);
                    table.ForeignKey(
                        name: "FK_Campeonato_Temporadas_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporadas",
                        principalColumn: "TemporadaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campeonato_TemporadaId",
                table: "Campeonato",
                column: "TemporadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_PartidaId",
                table: "Clubes",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubes_TimeId",
                table: "Clubes",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_GolId",
                table: "Jogadores",
                column: "GolId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_TimeId",
                table: "Jogadores",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_GolId",
                table: "Partidas",
                column: "GolId");

            migrationBuilder.CreateIndex(
                name: "IX_Rodadas_PartidaId",
                table: "Rodadas",
                column: "PartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporadas_ClubeId",
                table: "Temporadas",
                column: "ClubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Temporadas_PartidaId",
                table: "Temporadas",
                column: "PartidaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campeonato");

            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Rodadas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Temporadas");

            migrationBuilder.DropTable(
                name: "Clubes");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Gols");
        }
    }
}
