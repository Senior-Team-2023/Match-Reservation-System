using MatchReservationSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace MatchReservationSystem.Utilities
{
    public class HttpContextUserManager
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IHttpContextManager HttpContextManager;
        public HttpContextUserManager(IHttpContextManager httpContextManager, UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            HttpContextManager = httpContextManager;
        }
        public string GetUserId()
        {
            var userClaim = HttpContextManager.GetLoggedInUser();
            if (userClaim == null)
            {
                return "";
            }
            var userId = UserManager.GetUserId(userClaim);
            return userId ?? "";
        }
        public string GetUserName()
        {
            var userClaim = HttpContextManager.GetLoggedInUser();
            if (userClaim == null)
            {
                return "";
            }
            var userName = UserManager.GetUserName(userClaim);
            return userName ?? "";
        }

        public async Task<string> GetUserFullName()
        {
            var user = HttpContextManager.GetLoggedInUser();

            // Check if user exists and is authenticated
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                // Get the full name asynchronously using GetFullNameAsync method
                return await UserManager.GetFullNameAsync(user);
            }
            return "";
        }

        //public async Task<string> GetUserFullName()
        //{
        //    var fullName = HttpContextManager.GetRequestCookieValue(CookiesKeys.UserFullNameCookieKey);

        //    if (string.IsNullOrEmpty(fullName))
        //    {
        //        var user = HttpContextManager.GetLoggedInUser();

        //        // Check if user exists and is authenticated
        //        if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
        //        {
        //            // Get the full name asynchronously using GetFullNameAsync method
        //            fullName = await UserManager.GetFullNameAsync(user);

        //            // If the full name is obtained, add it to the cookie
        //            if (!string.IsNullOrEmpty(fullName))
        //            {
        //                var cookieKeyValuePair = new KeyValuePair<string, string>(CookiesKeys.UserFullNameCookieKey, fullName);
        //                HttpContextManager.AddCookieToResponse(cookieKeyValuePair);
        //            }
        //        }
        //    }

        //    return fullName ?? "";
        //}



    }
}
