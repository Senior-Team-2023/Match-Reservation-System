using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MatchReservationSystem.Models
{
    public static class UserManagerExtensions
    {
        public static async Task<string> GetFullNameAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var applicationUser = await userManager.FindByIdAsync(userId);
            var fullName = $"{applicationUser.FirstName} {applicationUser.LastName}";
            return fullName;
        }
    }
}
