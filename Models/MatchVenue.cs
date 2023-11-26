using MatchReservationSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace MatchReservationSystem.Models
{
    public class MatchVenue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }

        [Display(Name = "Capacity")]
        public int NumberOfSeats { get; set; }


        [Display(Name = "Shape (In Meter Square)")]
        public int ShapeInMeterSquare { get; set; }
    }
}
