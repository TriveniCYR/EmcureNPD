using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace EmcureNPD.Web.Helpers
{
    public class Helper : IHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _cofiguration;
        //private readonly IHelper _helper;
        public Helper(IHttpContextAccessor httpContextAccessor, Microsoft.Extensions.Configuration.IConfiguration configuration)//, IHelper helper
        {
            _httpContextAccessor = httpContextAccessor;
            _cofiguration = configuration;
            //_helper = helper;
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
        public void LogExceptions(Exception ex)
        {
            try
            {
                _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                System.Net.Http.HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.LogException, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(ex))).Result;

            }
            catch (Exception e)
            {

            }

        }
		public bool _isEmptyOrInvalid(string token,DateTime VallidTo)
		{
			if (string.IsNullOrEmpty(token))
			{
				return true;
			}

			var jwtToken = new JwtSecurityToken(token);
			return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (VallidTo < jwtToken.ValidTo);
		}
	}
}