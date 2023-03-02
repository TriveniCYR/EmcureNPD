using EmcureNPD.Resource;
using EmcureNPD.Web.Helpers;
using EmcureNPD.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using EmcureNPD.Business.Models;

namespace EmcureNPD.Web.Controllers
{
    public class NotificationsController : Controller
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        #endregion
        public NotificationsController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;

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
                throw;
            }
            
        }
    }
}
