using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalStadiumInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "City",
                table: "MatchVenues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "MatchVenues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShapeInMeterSquare",
                table: "MatchVenues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "MatchVenues");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "MatchVenues");

            migrationBuilder.DropColumn(
                name: "ShapeInMeterSquare",
                table: "MatchVenues");
        }
    }
}
