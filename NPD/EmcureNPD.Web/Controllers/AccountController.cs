using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmcureNPD.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Properties

        private readonly IDistributedCache _cache;
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Account> _stringLocalizer;
        private readonly IHelper _helper;

        #endregion Properties

        public AccountController(IDistributedCache cache, IConfiguration configuration, IStringLocalizer<Account> stringLocalizer, IHelper helper)
        {
            _cache = cache;
            _cofiguration = configuration;
            _stringLocalizer = stringLocalizer;
            _helper = helper;
        }

        public IActionResult Login()
        {
            //APIRepository objapi = new APIRepository(_cofiguration);
            LoginViewModel loginViewModel = new LoginViewModel();
            //HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetBusinessUnit, HttpMethod.Get, string.Empty).Result;
            ////if (responseMessage.IsSuccessStatusCode)
            //{
            // string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
            // var data =JsonConvert.DeserializeObject<APIResponseEntity<List<MasterBusinessUnitEntity>>>(jsonResponse);
            // loginViewModel.masterBusinessUnitEntities = data._object;
            //}
            //loginViewModel.masterBusinessUnitEntities = BindListBusinessUnit();
            return View(loginViewModel);
        }

        #region Binding_Dropdown_code

        [NonAction] // if Method is not Action method then use NonAction
        public List<MasterBusinessUnitEntity> BindListBusinessUnit()
        {
            List<MasterBusinessUnitEntity> listBusUnit = new List<MasterBusinessUnitEntity>();
            APIRepository objapi = new APIRepository(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetBusinessUnit, HttpMethod.Get, string.Empty).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<List<MasterBusinessUnitEntity>>>(jsonResponse);
                listBusUnit = data._object;
            }
            return listBusUnit;
        }

        #endregion Binding_Dropdown_code

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                APIRepository objapi = new APIRepository(_cofiguration);

                if (!(string.IsNullOrEmpty(loginViewModel.Email) && string.IsNullOrEmpty(loginViewModel.Password)))
                {
                    HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.LoginURL, HttpMethod.Post, string.Empty, new StringContent(JsonConvert.SerializeObject(loginViewModel))).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                        var oUserDetail = JsonConvert.DeserializeObject<APIResponseEntity<UserSessionEntity>>(jsonResponse);
                        SetUserClaim(oUserDetail._object);
                        HttpContext.Session.SetInt32(UserHelper.LoggedInRoleId, oUserDetail._object.RoleId);
                        var roles = UtilityHelper.GetModuleRole<dynamic>(oUserDetail._object.RoleId);
                        if (roles == null)
                        {
                            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                            HttpResponseMessage resRoles = objapi.APICommunication(APIURLHelper.GetByPermisionRoleUsingRoleId + "/" + oUserDetail._object.RoleId, HttpMethod.Get, oUserDetail._object.UserToken).Result;
                            if (resRoles.IsSuccessStatusCode)
                            {
                                string rolJson = resRoles.Content.ReadAsStringAsync().Result;
                                var data = JsonConvert.DeserializeObject<APIResponseEntity<IEnumerable<RolePermissionModel>>>(rolJson);
                                UtilityHelper.AddModuleRole(oUserDetail._object.RoleId, data._object);
                                roles = data._object;
                            }
                        }
                        //HttpContext.Session.SetObject(UserHelper.LoggedPermission, (object)(roles));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.errormessage = _stringLocalizer["InvalidUser"].Value;
                        //loginViewModel.masterBusinessUnitEntities = BindListBusinessUnit();
                        return View(loginViewModel);
                    }
                }
                else
                {
                    ViewBag.errormessage = _stringLocalizer["InvalidUser"].Value;
                    //loginViewModel.masterBusinessUnitEntities = BindListBusinessUnit();
                    return View(loginViewModel);
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                //loginViewModel.masterBusinessUnitEntities = BindListBusinessUnit();
                return View(loginViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            HttpContext.Response.Cookies.Delete(UserHelper.EmcureNPDToken);

            return RedirectToAction("Login", "Account");
        }

        private void SetUserClaim(UserSessionEntity oUserDetail)
        {
            HttpContext.Response.Cookies.Append(UserHelper.EmcureNPDToken, oUserDetail.UserToken, new CookieOptions { Expires = oUserDetail.VallidTo });

            var claims = new List<Claim>
                            {
                                new Claim("FullName", oUserDetail.FullName),
                                new Claim("Email", oUserDetail.Email),
                                new Claim("UserId", Convert.ToString(oUserDetail.UserId)),
                                new Claim("RoleId", Convert.ToString(oUserDetail.RoleId)),
                                new Claim("IsManagement", Convert.ToString(oUserDetail.IsManagement)),
                                new Claim("AssignedBusinessUnit", oUserDetail.AssignedBusinessUnit)
                            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties();
            props.IsPersistent = true;
            props.ExpiresUtc = oUserDetail.VallidTo;

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
            HttpContext.Session.SetString(UserHelper.LoggedInUserId, Convert.ToString(oUserDetail.UserId));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (CheckEmailAddressExists(forgotPasswordViewModel.Email))
            {
                APIRepository objapi = new APIRepository(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.ForgotPassword, HttpMethod.Post, string.Empty, new StringContent(JsonConvert.SerializeObject(forgotPasswordViewModel))).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.Message = _stringLocalizer["msgLinkToResetpasswordSentOnEmail"].Value;
                }
                else // if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.Message = _stringLocalizer["SomeErrorOccurred"].Value;
                }
            }
            else
            {
                ViewBag.Message = _stringLocalizer["msgEmailAddressNotExistIndatabase"].Value;
            }
            return View(forgotPasswordViewModel);
        }

        // if CheckEmailAddressExists() is false then Email Id Exist in Db
        [NonAction]
        public bool CheckEmailAddressExists(string EmailAddress)
        {
            bool EmailExist = true;
            try
            {
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.Anonymous_CheckEmailAddressExists + "/" + EmailAddress, HttpMethod.Get, string.Empty).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    EmailExist = JsonConvert.DeserializeObject<bool>(jsonResponse);

                    return EmailExist;
                }
                return EmailExist;
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                throw e;
            }
        }

        // if CheckEmailAddressExists() is false then Email Id Exist in Db
        [NonAction]
        public bool CheckResetPasswordTokenExists(string token)
        {
            bool TokenExist = false;
            try
            {
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.Anonymous_IsTokenValid + "/" + token, HttpMethod.Get, string.Empty).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    TokenExist = JsonConvert.DeserializeObject<bool>(jsonResponse);

                    return TokenExist;
                }
                return TokenExist;
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                throw e;
            }
        }

        [HttpGet]
        public IActionResult ResetPassword([FromQuery] string userToken)
        {
            MasterUserResetPasswordEntity resetPasswordEntity = new MasterUserResetPasswordEntity();
            try
            {
                if (CheckResetPasswordTokenExists(userToken))
                {
                    string strValue = HttpContext.Request.Query["userToken"].ToString();
                    resetPasswordEntity.ForgotPasswordToken = strValue;
                    return View(resetPasswordEntity);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.Message = Convert.ToString(e.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public IActionResult ResetPassword(MasterUserResetPasswordEntity masterUserresetpassword)
        {
            try
            {
                APIRepository objapi = new APIRepository(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.ResetPassword, HttpMethod.Post, string.Empty, new StringContent(JsonConvert.SerializeObject(masterUserresetpassword))).Result;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<string>>(jsonResponse);
                if (data._object == "ResetSuccessfully")
                {
                    ViewBag.Message = _stringLocalizer["msgPasswordResetSuccessfully"].Value;
                }
                else if (data._object == "TokenExpired")
                {
                    ViewBag.Message = _stringLocalizer["msgResetPasswordTokenExpired"].Value;
                }
                else
                {
                    ViewBag.Message = _stringLocalizer["msgInvalidLink"].Value;
                }
                return View();
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.Message = Convert.ToString(e.StackTrace);
                return View();
            }
        }
    }
}