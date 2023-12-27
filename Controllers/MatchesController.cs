using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MatchReservationSystem.Models;
using MatchReservationSystem.Ops;
using MatchReservationSystem.DbContexts;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MatchReservationSystem.Controllers
{
    public class MatchesController : Controller
    {
        private readonly MatchOps MatchOps;
        private readonly TeamOps TeamOps;
        private readonly MatchVenueOps MatchVenueOps;
        private readonly RefereeOps RefereeOps;
        public MatchesController(GlobalDbContext context)
        {
            MatchOps = new MatchOps(context);
            TeamOps = new TeamOps(context);
            MatchVenueOps = new MatchVenueOps(context);
            RefereeOps = new RefereeOps(context);
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            return View(await MatchOps.GetAllRecursiveSortedByDateAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var match = await MatchOps.GetRecursiveAsync((int)id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        [Authorize(Roles = "Manager")]
        // GET: Matches/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagItems();
            return View();
        }
        // POST: Matches/Create
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeTeamId,AwayTeamId,MatchVenueId,Date,MainRefereeId,LineManOneId,LineManTwoId")] Match match)
        {
            if (ModelState.IsValid)
            {
                string validMatchMsg = await ValidateMatch(match);
                if (validMatchMsg == "")
                {
                    MatchOps.Create(match);
                    await MatchOps.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ErrMsg = validMatchMsg;
            }
            await SetViewBagItems();
            return View(match);
        }

        // GET: Matches/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            var match = await MatchOps.GetRecursiveAsync((int)id);
            if (match == null)
            {
                return NotFound();
            }
            await SetViewBagItems();
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,MatchVenueId,Date,MainRefereeId,LineManOneId,LineManTwoId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string validMatchMsg = await ValidateMatch(match);
                if (validMatchMsg == "")
                {
                    try
                    {
                        MatchOps.Edit(match);
                        await MatchOps.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MatchOps.Exists(match.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ErrMsg = validMatchMsg;
            }
            await SetViewBagItems();
            return View(match);
        }

        // GET: Matches/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            var match = await MatchOps.GetRecursiveAsync((int)id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MatchOps.Delete(id);
            await MatchOps.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllTeamsExcept(int? teamId)
        {
            if (teamId == null)
            {
                return Json("");
            }
            // Load the options for the second dropdown list based on the selected value of the first dropdown list
            var teams = await TeamOps.GetAllExceptAsync((int)teamId);
            // Return the options as a JSON result
            return Json(new SelectList(teams, "Id", "Name"));
        }

        public async Task<IActionResult> GetAllLineMenExcept(int? refereeId)
        {
            if (refereeId == null)
            {
                return Json("");
            }
            // Load the options for the second dropdown list based on the selected value of the first dropdown list
            var referees = await RefereeOps.GetAllLineMenExceptAsync((int)refereeId);
            // Return the options as a JSON result
            return Json(new SelectList(referees, "Id", "Name"));
        }
        private async Task<string> ValidateMatch(Match newMatch)
        {
            if (DateTime.Now.AddDays(3) < newMatch.Date)
                return "Match creating must be before the match day with at least 3 days";

            var allMatches = await MatchOps.GetAllExceptMe(newMatch.Id);
            foreach (Match match in allMatches)
            {

                if (newMatch.Date.Year == match.Date.Year && newMatch.Date.Month == match.Date.Month && newMatch.Date.Day == match.Date.Day)
                {
                    if (newMatch.MatchVenueId == match.MatchVenueId)
                        return "Stadium already in use in this day";


                    if (newMatch.HomeTeamId == match.AwayTeamId || newMatch.AwayTeamId == match.HomeTeamId || newMatch.HomeTeamId == match.HomeTeamId || newMatch.AwayTeamId == match.AwayTeamId)
                        return "One of the teams already has a match in this day";

                    if (newMatch.MainRefereeId == match.MainRefereeId)
                        return "Main Referee aready has a match this day";

                    if (newMatch.LineManOneId == match.LineManOneId || newMatch.LineManTwoId == match.LineManTwoId || newMatch.LineManOneId == match.LineManTwoId || newMatch.LineManTwoId == match.LineManOneId)
                        return "One of the 2 linemen already has a match in this day";
                }
            }
            return "";
        }
        private async Task SetViewBagItems()
        {
            ViewBag.HomeTeams = new SelectList(await TeamOps.GetAllAsync(), "Id", "Name");
            ViewBag.AwayTeams = new SelectList(await TeamOps.GetAllAsync(), "Id", "Name");
            ViewBag.MatchVenues = new SelectList(await MatchVenueOps.GetAllAsync(), "Id", "Name");
            ViewBag.MainReferees = new SelectList(await RefereeOps.GetAllMainRefereeAsync(), "Id", "Name");
            ViewBag.LineMen = new SelectList(await RefereeOps.GetAllLineMenAsync(), "Id", "Name");
        }
    }
}
