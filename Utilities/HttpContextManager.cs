using System.Security.Claims;

namespace MatchReservationSystem.Utilities
{
    public class HttpContextManager : IHttpContextManager
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public HttpContextManager(IHttpContextAccessor _httpContextAccessor)
        {
            HttpContextAccessor = _httpContextAccessor;
        }

        public ClaimsPrincipal? GetLoggedInUser()
        {
            if (HttpContextAccessor == null || HttpContextAccessor.HttpContext == null)
            {
                return null;
            }
            return HttpContextAccessor.HttpContext.User;
        }

        public void AddCookieToResponse(KeyValuePair<string, string> cookie, DateTimeOffset? dateTimeOffset = null)
        {
            if (HttpContextAccessor == null
                || HttpContextAccessor.HttpContext == null
                || HttpContextAccessor.HttpContext.Request == null
                || HttpContextAccessor.HttpContext.Request.Cookies == null)
            {
                return;
            }

            // add cookie
            var cookieOptions = new CookieOptions
            {
                Expires = dateTimeOffset ?? DateTime.Now.AddYears(5),
            };
            HttpContextAccessor.HttpContext.Response.Cookies.Append(cookie.Key, cookie.Value, cookieOptions);
        }

        public string? GetRequestCookieValue(string cookieName)
        {
            if (HttpContextAccessor == null
                || HttpContextAccessor.HttpContext == null
                || HttpContextAccessor.HttpContext.Request == null
                || HttpContextAccessor.HttpContext.Request.Cookies == null)
            {
                return null;
            }

            return HttpContextAccessor.HttpContext.Request.Cookies[cookieName];
        }
    }
}
