using MarkReservationSystem.Models;
using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class ReservationOps : BasicOps<Reservation>
    {
        public ReservationOps(DbContext dbcontext) : base(dbcontext) { }
        public async Task<List<Reservation>> GetAllRecursiveAsync()
        {
            return await DbSet
                .Include(m => m.MatchVenue)
                //.Include(m => m.User)
                .Include(m => m.Match)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task<List<Reservation>> GetMatchReservations(int matchId, int matchVenueId)
        {
            return await DbSet
                .Where(m => m.MatchId == matchId && m.MatchVenueId == matchVenueId)
                .ToListAsync();
        }
    }
}
