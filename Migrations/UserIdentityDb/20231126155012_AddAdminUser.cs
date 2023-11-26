using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchReservationSystem.Migrations.UserIdentityDb
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [FirstName], [LastName], [Birthdate], [Address], [Gender], [City], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1a5f2c83-9e2e-45e8-88d6-66a014d07a54', N'admin', N'mark', N'yasser', N'2000-06-25 00:00:00', N'Maadi', 0, 0, N'ADMIN', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAIAAYagAAAAEGkamYBM1s74G5tEDECn1TzODRE0UX8p3VOFmfdMtkolivHNMpuULUUmvfQ5Xj0HyA==', N'YLUHMGQFJ2MQTHAPE6RVKPU4U6SZO3SZ', N'a97ac162-a7da-44e7-851a-f30640e8858b', NULL, 0, 0, NULL, 1, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE [Id] = '1a5f2c83-9e2e-45e8-88d6-66a014d07a54'");    
        }
    }
}
