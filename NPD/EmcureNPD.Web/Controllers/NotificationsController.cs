using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class NotificationsController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        private readonly IHelper _helper;

        #endregion Properties

        public NotificationsController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;
            _helper = helper;
        }

        public IActionResult AllNotification()
        {
            try
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetAllNotification, HttpMethod.Post, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<DataTableResponseModel>(jsonResponse);
                    // return data.Data;
                    return View(data);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw;
            }
        }

        [OutputCache(Duration = 120, VaryByParam = "RoleId")]
        [HttpGet]
        public IActionResult GetFilteredNotifications(string ColumnName, string SortDir, int start, int length)
        {
            try
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetFilteredNotifications + "/" + ColumnName + "/" + SortDir + "/" + start + "/" + length, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = jsonResponse; //JsonConvert.DeserializeObject<DataTableResponseModel>(jsonResponse);
                    //return data.Data;
                    return Json(data);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw;
            }
        }

        public IActionResult ClickedNotification()
        {
            try
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.NotificationsClickedByUser, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<DataTableResponseModel>(jsonResponse);
                    // return data.Data;
                    return View(data);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw;
            }
        }
    }
}