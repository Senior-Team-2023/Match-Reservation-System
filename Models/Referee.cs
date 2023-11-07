using MatchReservationSystem.Common;

namespace MatchReservationSystem.Models
{
    public class Referee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RefereeType RefereeType { get; set; }
    }
}
