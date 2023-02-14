using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EmcureNPD.Utility.Helpers;
using System.Net.Mail;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Utility.Models;

namespace EmcureNPD.Web.Controllers
{
    public class UserController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private IHostingEnvironment _env;
        #endregion

        public UserController(IConfiguration configuration, IHostingEnvironment env,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _env = env;
        }

        public IActionResult User()
        {
			int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
			RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
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
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                masterUser.LoggedUserId = int.Parse(logUserId);
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
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return RedirectToAction("UserManage", new { UserId = masterUser.UserId });
            }
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                if (!string.IsNullOrEmpty(masterUser.Password) && masterUser.UserId <= 0)
                {
                    masterUser.Password = Utility.Utility.UtilityHelper.GenerateSHA256String(masterUser.Password);
                    masterUser.ConfirmPassowrd = masterUser.Password;
                }

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
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return RedirectToAction("UserManage", new { UserId = masterUser.UserId });
            }
        }

        public IActionResult ChangePassword()
        {
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
                masterUser.UserId = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
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
                ViewBag.Message = Convert.ToString(e.StackTrace);
                return View();
            }
            ModelState.Clear();
            return View();
        }

        [NonAction]
        // if CheckEmailAddressExists() is false then Email Id Exist in Db
        public bool CheckEmailAddressExists(string EmailAddress)
        {
            bool EmailExist= true;
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
                throw e;
            }
        }

    }
}
