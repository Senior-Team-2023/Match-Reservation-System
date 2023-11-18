
namespace MatchReservationSystem.Models
{
    public class Match
    {
        public int Id { get; set; }


        public int? HomeTeamId { get; set; }
        public virtual Team? HomeTeam { get; set; }


        public int? AwayTeamId { get; set; }
        public virtual Team? AwayTeam { get; set; }



        public int? MatchVenueId { get; set; }
        public virtual MatchVenue? MatchVenue { get; set; }


        public DateTime Date { get; set; }


        public int? MainRefereeId { get; set; }
        public virtual Referee? MainReferee { get; set; }


        public int? LineManOneId { get; set; }
        public virtual Referee? LineManOne { get; set; }


        public int? LineManTwoId { get; set; }
        public virtual Referee? LineManTwo { get; set; }
    }
}
