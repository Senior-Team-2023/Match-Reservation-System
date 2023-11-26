using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MatchReservationSystem.Models;
using MatchReservationSystem.Ops;
using MarkReservationSystem.DbContexts;
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
            return View(await MatchOps.GetAllRecursiveAsync());
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

        // GET: Matches/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagItems();
            return View();
        }
        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeTeamId,AwayTeamId,MatchVenueId,Date,MainRefereeId,LineManOneId,LineManTwoId")] Match match)
        {
            if (ModelState.IsValid)
            {
                MatchOps.Create(match);
                await MatchOps.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await SetViewBagItems();
            return View(match);
        }

        // GET: Matches/Edit/5
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,MatchVenueId,Date,MainRefereeId,LineManOneId,LineManTwoId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            await SetViewBagItems();
            return View(match);
        }

        // GET: Matches/Delete/5
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
