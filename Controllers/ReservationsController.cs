﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MatchReservationSystem.DbContexts;
using MatchReservationSystem.Models;
using MatchReservationSystem.Ops;
using Microsoft.AspNetCore.Authorization;
using MatchReservationSystem.Utilities;

namespace MatchReservationSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly MatchOps MatchOps;
        private readonly MatchVenueOps MatchVenueOps;
        private readonly ReservationOps ReservationOps;
        private readonly HttpContextUserManager HttpContextUserManager;

        public ReservationsController(GlobalDbContext context, HttpContextUserManager httpContextUserManager)
        {
            MatchOps = new MatchOps(context);
            MatchVenueOps = new MatchVenueOps(context);
            ReservationOps = new ReservationOps(context);
            HttpContextUserManager = httpContextUserManager;
        }
        [Authorize(Roles = "Fan")]
        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var userId = HttpContextUserManager.GetUserId();
            return View(await ReservationOps.GetAllRecursiveAsync(userId));
        }
        [Authorize(Roles = "Fan,Manager")]
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await ReservationOps.GetRecursiveByIdAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [Authorize(Roles = "Fan,Manager")]
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
            await SetViewBagItems(matchId, matchVenueId, width, height);
            return View();
        }



        // POST: Reservations/Create
        [Authorize(Roles = "Fan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchVenueId,SeatPosition")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                var reservedMatch = await MatchOps.GetRecursiveAsync((int)reservation.MatchId);
                DateTime matchDate = reservedMatch.Date;
                DateTime nowPlus3Days = now.AddDays(3);
                //Console.WriteLine("nowPlus3Days: " + nowPlus3Days.ToString("yyyy-MM-dd"));
                //Console.WriteLine("matchDate: " + matchDate.ToString("yyyy-MM-dd"));

                if (nowPlus3Days > matchDate)
                {
                    return BadRequest("Reservation cannot be made because the match date is within the next 3 days.");
                }
                reservation.ReservationDate = now;
                reservation.UserId = HttpContextUserManager.GetUserId();
                ReservationOps.Create(reservation);
                await ReservationOps.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { Id = reservation.Id });
            }

            var match = await MatchOps.GetRecursiveAsync((int)reservation.MatchId);

            int matchVenueId = match.MatchVenue.Id;
            int width = match.MatchVenue.Width;
            int height = match.MatchVenue.Height;
            await SetViewBagItems((int)reservation.MatchId, matchVenueId, width, height);
            return View(reservation);
        }


        [Authorize(Roles = "Fan")]
        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await ReservationOps.GetRecursiveByIdAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
            //return View();
        }

        // POST: Reservations/Delete/5
        [Authorize(Roles = "Fan")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ReservationOps.Delete(id);
            await ReservationOps.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
