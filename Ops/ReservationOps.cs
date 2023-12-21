using MatchReservationSystem.Models;
using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class ReservationOps : BasicOps<Reservation>
    {
        public ReservationOps(DbContext dbcontext) : base(dbcontext) { }
        public async Task<List<Reservation>> GetAllRecursiveAsync(string? userId)
        {
            return await DbSet
                .Include(m => m.MatchVenue)
                .Include(m => m.Match)
                .ThenInclude(m => m.AwayTeam)
                .Include(m => m.Match)
                .ThenInclude(m => m.HomeTeam)
                .Where(x=>x.UserId == userId)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task<Reservation> GetRecursiveByIdAsync(int? id)
        {
            return await DbSet
                .Where(x => x.Id == id)
                .Include(m => m.MatchVenue)
                .Include(m => m.Match)
                .ThenInclude(m => m.AwayTeam)
                .Include(m => m.Match)
                .ThenInclude(m => m.HomeTeam)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Reservation>> GetMatchReservations(int matchId, int matchVenueId)
        {
            return await DbSet
                .Where(m => m.MatchId == matchId && m.MatchVenueId == matchVenueId)
                .ToListAsync();
        }
    }
}
