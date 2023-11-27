using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class Reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    MatchVenueId = table.Column<int>(type: "int", nullable: true),
                    SeatPosition = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_MatchVenues_MatchVenueId",
                        column: x => x.MatchVenueId,
                        principalTable: "MatchVenues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MatchId",
                table: "Reservation",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MatchVenueId",
                table: "Reservation",
                column: "MatchVenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
