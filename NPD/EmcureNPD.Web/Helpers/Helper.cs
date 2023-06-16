using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.Provider;
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

        public bool IsAccessToPIDF(int MoudleId,int? PIDFID)    //         //  row
        {
            if (PIDFID == null ||  PIDFID <= 0 )
                return true;

            var pidfStatusID = GetPidfStatus(PIDFID);
            bool IsAcceesible = false;
            switch (MoudleId)
            {
                case (int)ModuleEnum.PIDF:
                    if (pidfStatusID == 1 || pidfStatusID == 2)
                        IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.IPD:
                    if ( pidfStatusID == 3 ||  pidfStatusID == 5 ||  pidfStatusID == 6 ||  pidfStatusID == 9)
                        IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;
                case (int)ModuleEnum.Medical:
                    if ( pidfStatusID == 3 ||  pidfStatusID == 5 ||  pidfStatusID == 6 ||  pidfStatusID == 9)
                    IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.CommercialManagement:
                    if ( pidfStatusID == 7 ||  pidfStatusID == 10 ||  pidfStatusID == 11 ||  pidfStatusID == 12 ||  pidfStatusID == 13 ||  pidfStatusID == 14 ||  pidfStatusID == 15 ||  pidfStatusID == 16 ||  pidfStatusID == 17)
                    IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.APIListManagement:
                    if ( pidfStatusID == 7 ||  pidfStatusID == 10 ||  pidfStatusID == 11 ||  pidfStatusID == 12 ||  pidfStatusID == 13 ||  pidfStatusID == 14 ||  pidfStatusID == 15 ||  pidfStatusID == 16 ||  pidfStatusID == 17)
                    IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.PBF:
                    if ( pidfStatusID == 7 ||  pidfStatusID == 10 ||  pidfStatusID == 11 ||  pidfStatusID == 12 ||  pidfStatusID == 13 ||  pidfStatusID == 14 ||  pidfStatusID == 15 ||  pidfStatusID == 16 ||  pidfStatusID == 17)
                    IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.Finance:
                    if ( pidfStatusID == 7 ||  pidfStatusID == 10 ||  pidfStatusID == 11 ||  pidfStatusID == 12 ||  pidfStatusID == 13 ||  pidfStatusID == 14 ||  pidfStatusID == 15 ||  pidfStatusID == 16 ||  pidfStatusID == 17)
                        IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                case (int)ModuleEnum.ManagementHOD:
                    if ( pidfStatusID == 18 ||  pidfStatusID == 20 ||  pidfStatusID == 21 ||  pidfStatusID == 22)
                        IsAcceesible = true;
                    else
                        IsAcceesible = false;
                    break;

                //case (int)ModuleEnum.Projectmanagment:
                //    if ( pidfStatusID == 22)
                //    IsAcceesible = true;
                //    else
                //        IsAcceesible = false;
                //    break;
            }

            return IsAcceesible;
        }
        private int GetPidfStatus(int? PIDFId)
        {
            try
            {
                HttpResponseMessage responseMessage;
                _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + PIDFId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsonResponse);
                    return data._object.StatusId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}