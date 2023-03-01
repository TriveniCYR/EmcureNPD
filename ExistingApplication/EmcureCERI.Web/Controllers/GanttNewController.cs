using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class GanttNewController : Controller
    {
        private  EmcureCERIDBContext _db = new EmcureCERIDBContext();
        [Authorize(Roles = "Senior Project Manager,Regulatory Manager,Prescriber")]
        public IActionResult Index()
        {
            ViewBag.DRFID = Convert.ToInt32(HttpContext.Session.GetString("DrfID"));
            ViewBag.GanttProjectName = HttpContext.Session.GetString("ProductID");
            ViewBag.Action = HttpContext.Session.GetString("Action");
            ViewBag.ListPageURL = HttpContext.Session.GetString("ListPageURL");
            ViewBag.DetailsPageURL = HttpContext.Session.GetString("DetailsPageURL");
            ViewBag.ButtonDetailPage = HttpContext.Session.GetString("ButtonDetailPage");
            ViewBag.ButtonListPage = HttpContext.Session.GetString("ButtonListPage");
            return View();
        }
        [Authorize(Roles = "Senior Project Manager,Regulatory Manager,Prescriber")]
        [ActionName("GanttDetails")]
        public IActionResult Index(int drfid)//,string strAction)
        {
            HttpContext.Session.SetString("DrfID", drfid.ToString());
            //HttpContext.Session.SetString("Action", strAction);
            string strAction= HttpContext.Session.GetString("Action");
            ViewBag.ListPageURL = HttpContext.Session.GetString("ListPageURL");
            ViewBag.DetailsPageURL = HttpContext.Session.GetString("DetailsPageURL");
            ViewBag.ButtonDetailPage = HttpContext.Session.GetString("ButtonDetailPage");
            ViewBag.ButtonListPage = HttpContext.Session.GetString("ButtonListPage");


            string strProjectName=null;
            if(strAction == "DRF")
            {
                var result = _db.Tbl_DRF_Initialization.Where(t => t.InitializationID == drfid).Select(x => x.DRFNo).FirstOrDefault();
                strProjectName = result.ToString();
            }
            else if(strAction == "PIDF")
            {
                var result = _db.Tbl_PIDF_HeaderNew.Where(t => t.PidfID == drfid).Select(x => x.PIDFNo).FirstOrDefault();
                strProjectName = result.ToString();
            }
            else//for brazil            
            {
                var result = _db.Tbl_DRF_Initialization.Where(t => t.InitializationID == drfid).Select(x => x.DRFNo).FirstOrDefault();
                strProjectName = result.ToString();
            }

            //HttpContext.Session.SetString("ProductID", result.ToString());
            HttpContext.Session.SetString("ProductID", strProjectName);
            ViewBag.DRFID = Convert.ToInt32(HttpContext.Session.GetString("DrfID"));
            ViewBag.GanttProjectName = HttpContext.Session.GetString("ProductID");
            ViewBag.Action = HttpContext.Session.GetString("Action");
            return View("Index");
        }

        [Authorize(Roles = "Senior Project Manager,Regulatory Manager,Prescriber")]
        [ActionName("GanttSummary")]
        public IActionResult GanttSummary()
        {
            ViewBag.ButtonSummaryPage = HttpContext.Session.GetString("ButtonSummaryPage");
            ViewBag.SummaryPageURL = HttpContext.Session.GetString("SummaryPageURL");
            ViewBag.GanttSummaryPageTitle = HttpContext.Session.GetString("GanttSummaryPageTitle");
            return View();
        }
    }
}
