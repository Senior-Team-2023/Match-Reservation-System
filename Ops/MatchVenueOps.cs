using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class MatchVenueOps : BasicOps<MatchVenue>
    {
        public MatchVenueOps(DbContext dbcontext) : base(dbcontext) { }
    }
}
