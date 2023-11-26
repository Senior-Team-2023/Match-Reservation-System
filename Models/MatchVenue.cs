using MatchReservationSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace MatchReservationSystem.Models
{
    public class MatchVenue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }

        [Display(Name = "Width (Seats in each row)")]
        public int Width { get; set; }


        [Display(Name = "Height (Rows)")]
        public int Height { get; set; }
    }
}
