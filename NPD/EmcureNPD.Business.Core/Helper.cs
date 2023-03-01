using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Http;

namespace EmcureNPD.Business.Core
{
    public class Helper : IHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserSessionEntity GetLoggedInUser()
        {
            return ((UserSessionEntity)_httpContextAccessor.HttpContext.Items["User"]);
        }
    }
}
