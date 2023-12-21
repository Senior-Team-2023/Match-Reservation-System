using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using MatchReservationSystem.Models;

namespace MatchReservationSystem.DbContexts
{
    public class GlobalDbContext : DbContext
    {
        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MatchVenue> MatchVenues { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<MatchReservationSystem.Models.Reservation> Reservation { get; set; } = default!;
    }
}
