using EmcureNPD.Business.Models;
using EmcureNPD.Schedule.Services.Interfaces;
using EmcureNPD.Scheduler.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Claims;
using EmcureNPD.Scheduler.Helpers;
using EmcureNPD.Web.Models;

namespace EmcureNPD.Schedule.Services.Implementations {
    public class SchedulerService : ISchedulerService {

        private IConfiguration configuration;
        private ILoggerService loggerService;
        private IAPIService apiService;
        private IHttpContextAccessor httpContextAccessor;
        protected HttpContext HttpContext => httpContextAccessor.HttpContext;
        public SchedulerService(IConfiguration _configuration, ILoggerService _loggerService,IAPIService _apiService, IHttpContextAccessor _httpContextAccessor) {
            configuration = _configuration;
            loggerService = _loggerService;
            apiService = _apiService;
            httpContextAccessor = _httpContextAccessor;
        }

        public void GetToken() {
            try {
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
                loggerService.ServiceLog("\n------------------------Scheduler Started on { " + DateTime.Now.ToString()+" }---------------------------");                
                SendReminderAPI();
                loggerService.ServiceLog("------------------------Scheduler End on { " + DateTime.Now.ToString() + " }---------------------------");
            } catch (Exception ex) {
                loggerService.Log(ex);
            }
        }
        //public void SendReminderAPI(UserSessionEntity oUserDetail)}       
            public void SendReminderAPI() {
            try {
                //var token = oUserDetail.UserToken;
                loggerService.ServiceLog("Scheduler Called API for Send Reminder");
                HttpResponseMessage responseMessage = apiService.APICommunication(APIURLHelper.SendReminder, HttpMethod.Post,string.Empty).Result;
                if (responseMessage.IsSuccessStatusCode) {
                    loggerService.ServiceLog("Scheduler Called API Responded with Success Status Code");
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<SendReminderModel>>(jsonResponse);
                    SendReminderModel ObjRemoinderModel = data._object;
                    loggerService.ServiceLog(ObjRemoinderModel.LogMessage);
                }
            } catch (Exception ex) {
                loggerService.Log(ex); 
            }
        }
    }
}

