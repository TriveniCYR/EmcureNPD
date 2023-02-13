using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace EmcureNPD.Web.Helpers
{
    public class Helper : IHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetLoggedInUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString(UserHelper.LoggedInUserId));
        }

        public string GetToken()
        {
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            return token;
        }
    }
}