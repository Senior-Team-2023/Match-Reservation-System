﻿using MatchReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public class MatchOps : BasicOps<Match>
    {
        public MatchOps(DbContext dbcontext) : base(dbcontext) { }

        public async Task<List<Match>> GetAllRecursiveAsync()
        {
            return await DbSet
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.MatchVenue)
                .Include(m => m.MainReferee)
                .Include(m => m.LineManOne)
                .Include(m => m.LineManTwo)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task<List<Match>> GetAllRecursiveSortedByDateAsync()
        {
            return await DbSet
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.MatchVenue)
                .Include(m => m.MainReferee)
                .Include(m => m.LineManOne)
                .Include(m => m.LineManTwo)
                .AsSplitQuery()
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
        public async Task<Match> GetRecursiveAsync(int id)
        {
            return await DbSet
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.MatchVenue)
                .Include(m => m.MainReferee)
                .Include(m => m.LineManOne)
                .Include(m => m.LineManTwo)
                .AsSplitQuery()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Match>> GetAllExceptMe(int id)
        {
            return await DbSet
                .Where(x => x.Id != id)
                .ToListAsync();
        }
    }
}
