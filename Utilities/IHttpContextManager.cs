using System.Security.Claims;

namespace MatchReservationSystem.Utilities
{
    public interface IHttpContextManager
    {
        public ClaimsPrincipal? GetLoggedInUser();
        public void AddCookieToResponse(KeyValuePair<string, string> cookie, DateTimeOffset? dateTimeOffset = null);
        public string? GetRequestCookieValue(string cookieName);
    }
}
