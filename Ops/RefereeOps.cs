using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class RefereeOps : BasicOps<Referee>
    {
        public RefereeOps(DbContext dbcontext) : base(dbcontext) { }
        public async Task<List<Referee>> GetAllMainRefereeAsync()
        {
            return await DbSet.Where(r => r.RefereeType == Common.RefereeType.MainReferee).ToListAsync();
        }
        public async Task<List<Referee>> GetAllLineMenAsync()
        {
            return await DbSet.Where(r => r.RefereeType == Common.RefereeType.LineMan).ToListAsync();
        }
        public async Task<List<Referee>> GetAllLineMenExceptAsync(int id)
        {
            return await DbSet.Where(r => r.RefereeType == Common.RefereeType.LineMan && r.Id != id).ToListAsync();
        }

    }
}
