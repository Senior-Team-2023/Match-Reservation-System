using MatchReservationSystem.Models;

namespace MarkReservationSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        //public string? ApplicationUserId { get; set; }
        //public virtual ApplicationUser? User { get; set; }
        public int? MatchId { get; set; }
        public virtual Match? Match { get; set; }
        public int? MatchVenueId { get; set; }
        public virtual MatchVenue? MatchVenue { get; set; }
        public int SeatPosition { get; set; }
        public DateTime ReservationDate { get; set; }

    }
}
