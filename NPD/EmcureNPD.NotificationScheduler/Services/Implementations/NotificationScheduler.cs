using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using EmcureNPD.NotificationScheduler.Interfaces;
using EmcureNPD.NotificationScheduler.Services.Interfaces;
using EmcureNPD.NotificationScheduler.Helpers;
using EmcureNPD.Business.Models;
using Newtonsoft.Json;
using EmcureNPD.Web.Models;

namespace EmcureNPD.NotificationScheduler.Services.Implementations
{
	public class NotificationScheduler : INotificationScheduler
	{

        private IConfiguration configuration;
        private ILoggerService loggerService;
        private IAPIService apiService;
        private IHttpContextAccessor httpContextAccessor;
        protected HttpContext HttpContext => httpContextAccessor.HttpContext;
        public NotificationScheduler(IConfiguration _configuration, ILoggerService _loggerService, IAPIService _apiService, IHttpContextAccessor _httpContextAccessor)
        {
            configuration = _configuration;
            loggerService = _loggerService;
            apiService = _apiService;
            httpContextAccessor = _httpContextAccessor;
        }

        public void GetToken()
        {
            try
            {
                //var loginViewModel = new LoginViewModel();
                //loginViewModel.Email = configuration.GetSection("UserCredentials:Email").Value;
                //loginViewModel.Password = configuration.GetSection("UserCredentials:Password").Value;
                //if (!(string.IsNullOrEmpty(loginViewModel.Email) && string.IsNullOrEmpty(loginViewModel.Password))) {
                //    HttpResponseMessage responseMessage = apiService.APICommunication(APIURLHelper.LoginURL, HttpMethod.Post, string.Empty, new StringContent(JsonConvert.SerializeObject(loginViewModel))).Result;
                //    if (responseMessage.IsSuccessStatusCode) {
                //        string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                //        var oUserDetail = JsonConvert.DeserializeObject<APIResponseEntity<UserSessionEntity>>(jsonResponse);
                //       // SendReminderAPI(oUserDetail._object);
                //    } 
                //}
                loggerService.ServiceLog("\n------------------------Scheduler Started on { " + DateTime.Now.ToString() + " }---------------------------");
				SendNotificationAPI();
                loggerService.ServiceLog("------------------------Scheduler End on { " + DateTime.Now.ToString() + " }---------------------------");
            }
            catch (Exception ex)
            {
                loggerService.Log(ex);
            }
        }
        
        public void SendNotificationAPI()
        {
            try
            {
                //var token = oUserDetail.UserToken;
                loggerService.ServiceLog("Scheduler Called API for Send Notification");
                HttpResponseMessage responseMessage = apiService.APICommunication(APIURLHelper.SendNotification, HttpMethod.Post, string.Empty).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    loggerService.ServiceLog("Scheduler Called API Responded with Success Status Code");
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                   var data = JsonConvert.DeserializeObject<APIResponseEntity<SendReminderModel>>(jsonResponse);
                   SendReminderModel ObjRemoinderModel = data._object;
                    loggerService.ServiceLog(ObjRemoinderModel.LogMessage);
                }
            }
            catch (Exception ex)
            {
                loggerService.Log(ex);
            }
        }
    }
}

