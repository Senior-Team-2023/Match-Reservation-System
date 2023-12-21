using MatchReservationSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.DbContexts
{
    public class UserIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users","security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles","security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles","security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims","security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins","security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims","security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens","security");

        }
    }
}
