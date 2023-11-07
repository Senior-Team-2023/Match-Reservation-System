using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchVenues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchVenues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefereeType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamId = table.Column<int>(type: "int", nullable: true),
                    AwayTeamId = table.Column<int>(type: "int", nullable: true),
                    MatchVenueId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MainRefereeId = table.Column<int>(type: "int", nullable: true),
                    LineManOneId = table.Column<int>(type: "int", nullable: true),
                    LineManTwoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_MatchVenues_MatchVenueId",
                        column: x => x.MatchVenueId,
                        principalTable: "MatchVenues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Referees_LineManOneId",
                        column: x => x.LineManOneId,
                        principalTable: "Referees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Referees_LineManTwoId",
                        column: x => x.LineManTwoId,
                        principalTable: "Referees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Referees_MainRefereeId",
                        column: x => x.MainRefereeId,
                        principalTable: "Referees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LineManOneId",
                table: "Matches",
                column: "LineManOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LineManTwoId",
                table: "Matches",
                column: "LineManTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MainRefereeId",
                table: "Matches",
                column: "MainRefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchVenueId",
                table: "Matches",
                column: "MatchVenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "MatchVenues");

            migrationBuilder.DropTable(
                name: "Referees");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
