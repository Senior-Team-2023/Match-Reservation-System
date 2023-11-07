using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class TeamOps : BasicOps<Team>
    {
        public TeamOps(DbContext dbcontext) : base(dbcontext) { }

        public async Task<List<Team>> GetAllExceptAsync(int id)
        {
            return await DbSet.Where(t => t.Id != id).ToListAsync();
        }
    }
}
