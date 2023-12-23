
using System.ComponentModel.DataAnnotations;

namespace MatchReservationSystem.Models
{
    public class Match
    {
        public int Id { get; set; }


        [Display(Name = "Home Team")]
        public int? HomeTeamId { get; set; }
        public virtual Team? HomeTeam { get; set; }

        [Display(Name = "Away Team")]
        public int? AwayTeamId { get; set; }
        public virtual Team? AwayTeam { get; set; }


        [Display(Name = "Stadium")]
        public int? MatchVenueId { get; set; }
        public virtual MatchVenue? MatchVenue { get; set; }


        public DateTime Date { get; set; }

        [Display(Name = "Main Referee")]
        public int? MainRefereeId { get; set; }
        public virtual Referee? MainReferee { get; set; }

        [Display(Name ="Line Man One")]
        public int? LineManOneId { get; set; }
        public virtual Referee? LineManOne { get; set; }

        [Display(Name = "Line Man Two")]
        public int? LineManTwoId { get; set; }
        public virtual Referee? LineManTwo { get; set; }
    }
}
