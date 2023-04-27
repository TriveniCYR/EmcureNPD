using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class UserController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private IHostingEnvironment _env;
        private readonly IHelper _helper;

        #endregion Properties

        public UserController(IConfiguration configuration, IHostingEnvironment env,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _env = env;
            _helper = helper;
        }

        public IActionResult User()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.UserManagement, rolId);
            ViewBag._objPermission = objPermssion;
            return View();
        }

        public IActionResult UserManage(int? UserId)
        {
            MasterUserEntity masterUser;
            if (UserId == null || UserId <= 0)
            {
                masterUser = new MasterUserEntity();
                return View(masterUser);
            }
            else
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetUserById + "/" + UserId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<MasterUserEntity>>(jsonResponse);
                    if (data._object is null)
                        return NotFound();

                    return View(data._object);
                }
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserManage(int UserId, MasterUserEntity masterUser)
        {
            try
            {
                masterUser.StringPassword = masterUser.Password;
                masterUser.LoggedUserId = _helper.GetLoggedInUserId();
                if (UserId <= 0)
                {
                    if (CheckEmailAddressExists(masterUser.EmailAddress))
                    {
                        ModelState.AddModelError("EmailExist", "Email Address already exists in database.");
                        return View("UserManage", masterUser);
                    }
                    else
                    {
                        ModelState.Remove("EmailExist");
                    }
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return RedirectToAction("UserManage", new { UserId = masterUser.UserId });
            }
            try
            {
                masterUser.WebApplicationUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SaveUser, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(masterUser))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    TempData[UserHelper.SuccessMessage] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);

                    return RedirectToAction(nameof(User));
                }
                else
                {
                    TempData[UserHelper.ErrorMessage] = Convert.ToString(responseMessage.Content.ReadAsStringAsync().Result);
                    return RedirectToAction("UserManage", new { UserId = masterUser.UserId });
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return RedirectToAction("UserManage", new { UserId = masterUser.UserId });
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangeProfile()
        {
            var UserId = _helper.GetLoggedInUserId();
            MasterUserEntity masterUser = new MasterUserEntity();
            try
            { 
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetUserById + "/" + UserId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<MasterUserEntity>>(jsonResponse);
                    if (data._object is null)
                        return NotFound();

                    MasterUserEntityChangeProfile user = new MasterUserEntityChangeProfile();
                    user.FullName = data._object.FullName;
                    user.Address = data._object.Address;
                    user.MobileNumber = data._object.MobileNumber;
                    user.MobileCountryId = data._object.MobileCountryId;
                    return View(user);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return View(masterUser);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeProfile(MasterUserEntityChangeProfile masterUser)
        {
            try
            {
                ViewBag.Message = null;                
                masterUser.UserId = _helper.GetLoggedInUserId();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.ChangeUserProfile, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(masterUser))).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                   HttpContext.Session.SetString(UserHelper.LoggedInUserName, masterUser.FullName);
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = _stringLocalizerError["ProfileChanged"];
                    return View();
                }
                else
                {
                    ViewBag.Message = _stringLocalizerError["SomeErrorOccured"];
                    return View();
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.Message = Convert.ToString(e.StackTrace);                
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(MasterUserChangePasswordEntity masterUser)
        {
            try
            {
                ViewBag.Message = null;
                if (masterUser.Oldpassword == masterUser.Password)
                {
                    ViewBag.Message = _stringLocalizerError["OldNewPasswordSame"];
                    return View();
                }
                masterUser.UserId = _helper.GetLoggedInUserId();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.ChangeUserPassword, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(masterUser))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    ViewBag.Message = _stringLocalizerError["PasswordChanged"];
                    return View();
                }
                else if (!responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.Message = _stringLocalizerError["OldNewPasswordMismatch"];
                    return View();
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.Message = Convert.ToString(e.StackTrace);
                return View();
            }
            ModelState.Clear();
            return View();
        }

        [NonAction]
        public bool CheckEmailAddressExists(string EmailAddress)
        {
            bool EmailExist = true;
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.CheckEmailAddressExists + "/" + EmailAddress, HttpMethod.Get, token).Result;
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
    }
}