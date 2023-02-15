using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class RealtimeNotificationController : Controller
    {
        private readonly IConfiguration _config;
        private readonly EmcureCERIDBContext _db;
        IHostingEnvironment _env;
        private readonly IRealTimeNotificationService _realTimeNotificationService;

        public RealtimeNotificationController(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env, IRealTimeNotificationService realTimeNotificationService)
        {
            _db = db;
            _config = config;
            _env = env;
            _realTimeNotificationService = realTimeNotificationService;
        }
        [ActionName("GetNotification")]
        [HttpPost]
        public IActionResult GetNotification()
        {
            IList<RealtimeNotificationModel> realtimeNotificationModels = new List<RealtimeNotificationModel>();
            realtimeNotificationModels = _realTimeNotificationService.GetRealTimeNotificationData();

            IList<RealtimeNotificationModel> top5realtimeNotificationModels = new List<RealtimeNotificationModel>();
            top5realtimeNotificationModels = _realTimeNotificationService.GetTop5RealTimeNotificationData();
            IList<RealtimeNotificationModel> top5uPrealtimeNotificationModels = new List<RealtimeNotificationModel>();
            top5uPrealtimeNotificationModels = _realTimeNotificationService.GetTop5upRealTimeNotificationData();
            return Json(new { data = new { realtimeNotificationModels = realtimeNotificationModels, top5realtimeNotificationModels = top5realtimeNotificationModels, top5uPrealtimeNotificationModels = top5uPrealtimeNotificationModels } }, new JsonSerializerSettings());
        }

        [ActionName("UpdateViewNotification")]
        [HttpPost]
        public IActionResult UpdateViewNotification(int ProjectID)
        {
            int data = _realTimeNotificationService.UpdateViewNotification(ProjectID);

            return Json(new { data = data });

        }
        [ActionName("UpdateAllNotification")]
        [HttpPost]
        public IActionResult UpdateAllNotification()
        {
            int data = _realTimeNotificationService.UpdateAllNotification();

            return Json(new { data = data });
            //int data = _realTimeNotificationService.UpdateViewNotification(ProjectID);

            //return Json(new { data = data });

        }

    }
}
