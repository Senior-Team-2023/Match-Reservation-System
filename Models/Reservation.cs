using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace MatchReservationSystem.Models
{
    public class Reservation
    {
        [Display(Name ="Ticket Number")]
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Display(Name = "Match")]
        public int? MatchId { get; set; }
        public virtual Match? Match { get; set; }

        [Display(Name = "Stadium")]
        public int? MatchVenueId { get; set; }
        public virtual MatchVenue? MatchVenue { get; set; }
        [Display(Name = "Seat Position")]
        public int SeatPosition { get; set; }
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

    }
}
