using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace EmcureCERI.Web.Controllers
{
   // [Authorize]
    public class DashboardController : Controller
    {
        private readonly IConfiguration _config;
        private readonly EmcureCERIDBContext _db;
        IHostingEnvironment _env;
        private readonly IDashboardReportService _dashboardReportService;

        public DashboardController(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env, IDashboardReportService dashboardReportService)
        {
            _db = db;
            _config = config;
            _env = env;
            _dashboardReportService = dashboardReportService;
        }
      //  [Authorize]
        //[HttpPost]
        // [Authorize(Roles = "Admin, Prescriber, Approvers, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development")]
        public IActionResult Index()
        {            
            return View();
        }
        //[ActionName("GetDashboardData")]
        //public IActionResult GetDashboardData(string fromDate, string toDate)
        //{
        //    string fromdate;
        //    string todate;
        //    if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
        //    {
        //        fromdate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //        todate = DateTime.Now.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        fromdate = fromDate;
        //        todate = toDate;
        //    }

        //    ModelState.Clear();
        //    DashboardDetails dashboardDetails = new DashboardDetails();
        //    var model = _dashboardReportService.dashboardDetails(fromdate, todate);

        //    dashboardDetails.dashboardTabDatas.totalInitial = model.dashboardTabDatas.totalInitial;
        //    dashboardDetails.dashboardTabDatas.totalRejected = model.dashboardTabDatas.totalRejected;
        //    dashboardDetails.dashboardTabDatas.totalInitialApproved = model.dashboardTabDatas.totalInitialApproved;
        //    dashboardDetails.dashboardTabDatas.totalPartialApproved = model.dashboardTabDatas.totalPartialApproved;
        //    dashboardDetails.dashboardTabDatas.totalPendingFinanceApproval = model.dashboardTabDatas.totalPendingFinanceApproval;
        //    dashboardDetails.dashboardTabDatas.totalFinanceApproved = model.dashboardTabDatas.totalFinanceApproved;
        //    dashboardDetails.dashboardTabDatas.totalFinalApproved = model.dashboardTabDatas.totalFinalApproved;
        //    dashboardDetails.dashboardTabDatas.totalFinalRejected = model.dashboardTabDatas.totalFinalRejected;                                 

        //    return View("Index", dashboardDetails);            
        //}

        [ActionName("GetDashboardData1")]
       [HttpPost]
        public IActionResult GetDashboardData1(DashboardRequestVM dashboardRequestVM)//string fromDate, string toDate)
        {
            string fromdate;
            string todate;
            string countryName;
            int CompanyID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")); 

            if (string.IsNullOrEmpty(dashboardRequestVM.fromDate) && string.IsNullOrEmpty(dashboardRequestVM.toDate) && string.IsNullOrEmpty(dashboardRequestVM.countryName))
            {
                fromdate = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
                todate = DateTime.Now.ToString("yyyy-MM-dd");
                countryName = null;
            }
            else
            {                
                fromdate = dashboardRequestVM.fromDate;
                todate = dashboardRequestVM.toDate;
                countryName = dashboardRequestVM.countryName;
                
            }

            ModelState.Clear();
            DashboardDetails dashboardDetails = new DashboardDetails();
            var model = _dashboardReportService.dashboardDetails(fromdate, todate, countryName, CompanyID);

            dashboardDetails.dashboardTabDatas.totalInitial = model.dashboardTabDatas.totalInitial;
            dashboardDetails.dashboardTabDatas.totalRejected = model.dashboardTabDatas.totalRejected;
            dashboardDetails.dashboardTabDatas.totalInitialApproved = model.dashboardTabDatas.totalInitialApproved;
            dashboardDetails.dashboardTabDatas.totalPartialApproved = model.dashboardTabDatas.totalPartialApproved;
            dashboardDetails.dashboardTabDatas.totalPendingFinanceApproval = model.dashboardTabDatas.totalPendingFinanceApproval;
            dashboardDetails.dashboardTabDatas.totalFinanceApproved = model.dashboardTabDatas.totalFinanceApproved;
            dashboardDetails.dashboardTabDatas.totalFinalApproved = model.dashboardTabDatas.totalFinalApproved;
            dashboardDetails.dashboardTabDatas.totalFinalRejected = model.dashboardTabDatas.totalFinalRejected;

            //dynamic exo = new System.Dynamic.ExpandoObject();

            //foreach (var ddata in model.dashboardWorldMapDatas)
            //{
            //    ((IDictionary<String, Object>)exo).Add(ddata.countryCode, ddata.maxCount);
            //}
            dynamic exo = new System.Dynamic.ExpandoObject();
            string sampledata = "";
            foreach (var ddata in model.dashboardWorldMapDatas)
            {
                sampledata += ddata.countryCode + ":" + ddata.maxCount + ",";
                //((IDictionary<String, Object>)exo).Add(ddata.countryCode, ddata.maxCount);
            }

            dashboardDetails.dashboardWorldMapDatas = model.dashboardWorldMapDatas;
            sampledata = sampledata.Remove(sampledata.Length - 1, 1);
            sampledata = "{" + sampledata + "}";
            return Json(new { data = dashboardDetails }, new JsonSerializerSettings());            
        }

        //[ActionName("GetDashboardWorldMapData")]
        //[HttpPost]
        public JsonResult GetDashboardWorldMapData()
        {
            string fromdate;
            string todate;
            string countryName;
            int  CompanyID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID"));

            fromdate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                todate = DateTime.Now.ToString("yyyy-MM-dd");
                countryName = null;
            

            ModelState.Clear();
            DashboardDetails dashboardDetails = new DashboardDetails();
            var model = _dashboardReportService.dashboardDetails(fromdate, todate, countryName, CompanyID);



            string sampledata = "";
            foreach (var ddata in model.dashboardWorldMapDatas)
            {
                sampledata += "\"" + ddata.countryCode + "\"" + ":" + "\""  + ddata.maxCount.ToString() + ".00" + "\"" + ",";
                //((IDictionary<String, Object>)exo).Add(ddata.countryCode, ddata.maxCount);
            }

            dashboardDetails.dashboardWorldMapDatas = model.dashboardWorldMapDatas;
            sampledata = sampledata.Remove(sampledata.Length - 1, 1);
            //sampledata = "{" + sampledata + "}";

            return  Json(sampledata);
        }
    }
}