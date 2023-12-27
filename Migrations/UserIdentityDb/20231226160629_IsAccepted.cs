using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations.UserIdentityDb
{
    /// <inheritdoc />
    public partial class IsAccepted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                schema: "security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                schema: "security",
                table: "Users");

        }
    }
}
