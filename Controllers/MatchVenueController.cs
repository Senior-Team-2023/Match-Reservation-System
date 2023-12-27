using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatchReservationSystem.Models;
using MatchReservationSystem.Ops;
using MatchReservationSystem.DbContexts;
using Microsoft.AspNetCore.Authorization;
namespace MatchReservationSystem.Controllers
{
    [Authorize(Roles = "Manager")]
    public class MatchVenueController : Controller
    {
        private readonly MatchVenueOps MatchVenueOps;
        public MatchVenueController(GlobalDbContext context)
        {
            MatchVenueOps = new MatchVenueOps(context);
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            return View(await MatchVenueOps.GetAllAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var match = await MatchVenueOps.GetbyIdAsync((int)id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Height,Width")] MatchVenue match)
        {
            if (ModelState.IsValid)
            {
                MatchVenueOps.Create(match);
                await MatchVenueOps.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var match = await MatchVenueOps.GetbyIdAsync((int)id);
            if (match == null)
            {
                return NotFound();
            }
           
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Height,Width")] MatchVenue match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MatchVenueOps.Edit(match);
                    await MatchVenueOps.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchVenueOps.Exists(match.Id))
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
           
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var match = await MatchVenueOps.GetbyIdAsync((int)id);
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
            MatchVenueOps.Delete(id);
            await MatchVenueOps.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
