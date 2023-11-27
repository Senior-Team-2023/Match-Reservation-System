using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarkReservationSystem.DbContexts;
using MarkReservationSystem.Models;
using MatchReservationSystem.Ops;
using MatchReservationSystem.Models;

namespace MarkReservationSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly MatchOps MatchOps;
        private readonly MatchVenueOps MatchVenueOps;
        private readonly ReservationOps ReservationOps;

        public ReservationsController(GlobalDbContext context)
        {
            MatchOps = new MatchOps(context);
            MatchVenueOps = new MatchVenueOps(context);
            ReservationOps = new ReservationOps(context);
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await ReservationOps.GetAllRecursiveAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await ReservationOps.GetbyIdAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create/Id
        public async Task<IActionResult> Create(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var match = await MatchOps.GetRecursiveAsync((int)Id);
            if (match == null)
            {
                return NotFound();
            }
            int matchId = match.Id;
            int matchVenueId = match.MatchVenue.Id;
            int width = match.MatchVenue.Width;
            int height = match.MatchVenue.Height;
            await SetViewBagItems(matchId, matchVenueId,width, height);    
            return View();
        }
      
        private async Task SetViewBagItems(int matchId, int matchVenueId, int width, int height)
        {

            List<Reservation> reservedSeats = await ReservationOps.GetMatchReservations(matchId, matchVenueId);
            List<int> availablePositions = new List<int>();
            List<int> reservedPositions = new List<int>();
            List<bool> reservedPositionsBool = new List<bool>();
            List<string> seatColors = new List<string>();

            for (int i = 0; i < width * height; i++)
            {
                if (!reservedSeats.Any(r => r.SeatPosition == i))
                {
                    // Add seat to available seats
                    availablePositions.Add(i);
                    // Set seat to not reserved
                    reservedPositionsBool.Add(false);
                    // Set seat color to gray
                    seatColors.Add("gray");

                }
                else
                {
                    // Add seat to reserved seats
                    reservedPositions.Add(i);
                    // Set seat to reserved
                    reservedPositionsBool.Add(true);
                    // Set seat color to red
                    seatColors.Add("red");
                }
            }
            // Create 2D list of seats
            List<List<int>> seats2D = new List<List<int>>();
            for (int i = 0; i < height; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < width; j++)
                {
                    row.Add(i * width + j);
                }
                seats2D.Add(row);
            }
            ViewBag.SeatPositions = new SelectList(availablePositions);
            ViewBag.SeatColors = seatColors;
            ViewBag.ReservedPositions = reservedPositions;
            ViewBag.ReservedPositionsBool = reservedPositionsBool;
            ViewBag.Seats2D = seats2D;
            ViewBag.venueWidth = width;
            ViewBag.venueHeight = height;
            ViewBag.matchId = matchId;
            ViewBag.matchVenueId = matchVenueId;
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchVenueId,SeatPosition,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationOps.Create(reservation);
                await ReservationOps.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var match = await MatchOps.GetRecursiveAsync((int)reservation.MatchId);
         
            int matchVenueId = match.MatchVenue.Id;
            int width = match.MatchVenue.Width;
            int height = match.MatchVenue.Height;
            await SetViewBagItems((int)reservation.MatchId, matchVenueId, width, height);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var reservation = await _context.Reservation.FindAsync(id);
            //if (reservation == null)
            //{
            //    return NotFound();
            //}
            //ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Id", reservation.MatchId);
            //ViewData["MatchVenueId"] = new SelectList(_context.MatchVenues, "Id", "Id", reservation.MatchVenueId);
            //ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", reservation.ApplicationUserId);
            //return View(reservation);
            return View();
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,MatchId,MatchVenueId,SeatPosition,ReservationDate")] Reservation reservation)
        {
            //if (id != reservation.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(reservation);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ReservationExists(reservation.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["MatchId"] = new SelectList(_context.Matches, "Id", "Id", reservation.MatchId);
            //ViewData["MatchVenueId"] = new SelectList(_context.MatchVenues, "Id", "Id", reservation.MatchVenueId);
            //ViewData["ApplicationUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", reservation.ApplicationUserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await ReservationOps.GetbyIdAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
            //return View();
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ReservationOps.Delete(id);
            await ReservationOps.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool ReservationExists(int id)
        //{
        //  return (_context.Reservation?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
