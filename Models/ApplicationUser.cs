using MatchReservationSystem.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MatchReservationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [Required, MaxLength(100)]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required, MaxLength(100)]
        public string LastName { get; set;}

        
        [Required]
        public DateTime Birthdate { get; set; }

        public string? Address { get; set; }


        [Required]
        public Gender Gender { get; set; }


        [Required, MaxLength(100)]
        public City City { get; set; }


        public bool IsAccepted { get; set; }
    }
}
