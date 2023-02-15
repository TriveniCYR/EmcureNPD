using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly EmcureCERIDBContext _db;
        IHostingEnvironment _env;
        private readonly IReportsService _IReportsService;

        public ReportsController(EmcureCERIDBContext db, IConfiguration config, IHostingEnvironment env, IReportsService ReportsService)
        {
            _db = db;
            _config = config;
            _env = env;
            _IReportsService = ReportsService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult ManufacturingReports()
        {
            return View();
        }

        [ActionName("GetDropdownsForReports")]
        public ActionResult GetDropdownsForReports()
      {
            common commonVModel = new common();

            List<clsStatus> statusList = new List<clsStatus>();
            var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(x => x.IsActive == true && x.Status != null).Select(x => new {  x.Status }).Distinct();
            ////var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(x => x.IsActive == true && x.Status != null).Select(x => new { x.Status }).ToList();

            if (result == null )
            {
                statusList = null;
            }
            else
            {
                foreach (var ddata in result)
                {
                    statusList.Add(new clsStatus {  Status = ddata.Status });
                }
            }

            List<clsMolecule> moleculeList = new List<clsMolecule>();
            var result1 = _db.Tbl_DRF_Initialization.AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.GenericName)
                .Select(x => new { x.InitializationID, x.GenericName }).ToList();
            if (result1 == null || result1.Count <= 0)
            {
                moleculeList = null;
            }
            else
            {
                foreach (var ddata in result1)
                {
                    moleculeList.Add(new clsMolecule { InitializationID = ddata.InitializationID, GenericName = ddata.GenericName });
                }
            }
           
          


            commonVModel.StatusList = statusList;
            commonVModel.MoleculeList = moleculeList;
            return Json(new { success = true, data = commonVModel });
        }


        [Authorize]
        [HttpGet]
        [ActionName("GetAllReportsList")]
        public ActionResult GetAllReportsList()
        {

            string role = HttpContext.Session.GetString("CurrentUserRole");
           


           var List = _IReportsService.GetAllReportList();
            return Json(new { data = List });


        }


        [Authorize]
        [HttpGet]
        [ActionName("GetReportTimeline")]
        public ActionResult GetReportTimeline()
        {

            string role = HttpContext.Session.GetString("CurrentUserRole");



            var List = _IReportsService.GetReportofMoleculeTimeline();
            return Json(new { data = List });


        }
        public IActionResult TimelineReports()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetDRFUpdateHistoryDetails")]
        public ActionResult GetDRFUpdateHistoryDetails()
        {
            int tempInitializationID = Convert.ToInt32( HttpContext.Session.GetString("ReportDRFID")) ;
            var HistoryList = _IReportsService.GetDRFHistoryDetailsByID(tempInitializationID);            
            return Json(new { data = HistoryList });
        }
        public IActionResult DRFUpdateHistoryReport(int DRFID)
        {
            HttpContext.Session.SetString("ReportDRFID", Convert.ToString(DRFID));
            return View();
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetManufacturingDetailsReports")]
        public ActionResult GetManufacturingDetailsReports()
        {
            int tempInitializationID = Convert.ToInt32(HttpContext.Session.GetString("ReportDRFID"));
            var HistoryList = _IReportsService.GetReportofManfacturing();
            return Json(new { data = HistoryList });
        }
        public IActionResult DRFList()
        {
            return View();
        }
       
    }
}
