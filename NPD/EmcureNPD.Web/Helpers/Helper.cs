using EmcureNPD.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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
        public int GetLoggedInRoleId()
        {
            try
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string IsManagementUser()
        {
            return _httpContextAccessor.HttpContext.Session.GetString(UserHelper.IsManagement);
        }
        public string GetToken()
        {
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            return token;
        }
        public string GetAssignedBusinessUnit()
        {
            return (_httpContextAccessor.HttpContext.Session.GetString(UserHelper.AssignedBusinessUnit));
        }
    }
}