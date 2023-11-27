using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using MarkReservationSystem.Models;

namespace MarkReservationSystem.DbContexts
{
    public class GlobalDbContext : DbContext
    {
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchVenue> MatchVenues { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<MarkReservationSystem.Models.Reservation> Reservation { get; set; } = default!;
    }
}
