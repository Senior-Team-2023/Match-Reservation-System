using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class VenueWidthHeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShapeInMeterSquare",
                table: "MatchVenues",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "MatchVenues",
                newName: "Height");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "MatchVenues",
                newName: "ShapeInMeterSquare");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "MatchVenues",
                newName: "NumberOfSeats");
        }
    }
}
