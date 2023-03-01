using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using System.Net.Http.Headers; 
using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Models;
using EmcureCERI.Web.Models.FileUpload;
using EmcureCERI.Web.Models.PIDFPViewModels; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OfficeOpenXml;
using Microsoft.Office.Interop.Word;


namespace EmcureCERI.Web.Controllers
{
    public class ProductController : Controller 
    {
        
        private readonly IPIDFService _pidf;
        private readonly EmcureCERIDBContext _db;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ProductController(IPIDFService pIDFService ,IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this._pidf = pIDFService;
            this._sharedLocalizer = sharedLocalizer; 
            _db = new EmcureCERIDBContext(); 

        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult RegisteredProduct(int id = 0)
        {
            return View();
        }

        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult RegisteredPIDF(int id = 0)
        {
            return View();
        }

        public JsonResult GetPIDF()
        {
            List<PIDFViewModel> PIDFList = new List<PIDFViewModel>();
            ServiceResponseList<PIDFDetails> allPIDF = new ServiceResponseList<PIDFDetails>();
            //  allPIDF = _pidf.GetAllPatientsByPrescriber(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")));
            allPIDF = _pidf.GetAllPIDF();
           
            foreach (var item in allPIDF.Results)
            {
                PIDFViewModel model = new PIDFViewModel();
                model.Id = item.Id;
                model.ProductId = item.ProductId;
                model.ProductName = item.ProductName;
                model.Strengths = item.Strengths;
                //model.ProfitPerpack = item.ProfitPerpack;
                model.Region = item.Region;
                model.Country = item.Country;
                model.IsStatus = item.IsStatus;
               
                // model.CStatus = GetSingleStatus(item.IsStatus);
                // var baseline = _baselinedata.GetBaselineDataByPatientId(item.Id);
                // if (baseline != null)
                // {
                //     model.BStatus = GetSingleStatus(baseline.IsStatus);
                //    model.IsBaselineDataByAdmin = baseline.IsConfirmedByAdmin;
                // }
                // else
                // {
                //    model.BStatus = "Pending";
                //   model.IsBaselineDataByAdmin = false;
                // }
                // model.FStatus = GetStatus(model.CStatus, model.BStatus);
                PIDFList.Add(model);
            }
            return Json(new { data = PIDFList }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult PIDFAddOrEdit(string returnUrl = null)
        {


            List<SelectListItem> RegionLists = new List<SelectListItem>();
            // RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"].Value });

            RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"].Value, Selected = true });
            var regionLists = _db.Master_Continent.Select(o => o.Continent).ToList();
            foreach (var item in regionLists)
            {
                RegionLists.Add(new SelectListItem() { Value = item, Text = item });
            }
            ViewBag.RegionList = RegionLists;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult PIDFAddOrEdit(PIDFViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            

                if (ModelState.IsValid)
            { 

               PIDFDetails pidfDetail = new PIDFDetails(); 
                pidfDetail.ProductId = IncreamentUnique("EM-", _db.PIDFDetails.Select(o => o.ProductId).LastOrDefault());
                pidfDetail.ProductName = model.ProductName;
                pidfDetail.Strengths = model.Strengths;
                pidfDetail.Region = model.Region;
                pidfDetail.Country = model.Country;
                pidfDetail.PatentStatus = model.PatentStatus;
                pidfDetail.Packing = model.Packing;  

                _db.PIDFDetails.Add(pidfDetail);
                    _db.SaveChanges();
                
            } 

            
            return View(model);
        }

        protected virtual string IncreamentUnique(string prefix, string unique)
        {
            if (unique != null && unique != "")
            {
                var suffixNum = unique.Split(prefix);
                int increment = Convert.ToInt32(suffixNum[1]) + 1;
                return string.Concat(prefix, increment.ToString("0000"));
            }
            else
            {
                return "";
            }
        }
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult AddPIDF(int id = 0)
        {


            List<SelectListItem> RegionLists = new List<SelectListItem>();
            List<SelectListItem> CountryList = new List<SelectListItem>();
            // RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"].Value });

            RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"].Value, Selected = true });
            var regionLists = _db.Master_Continent.Select(o => o.Continent).ToList();
            foreach (var item in regionLists)
            {
                RegionLists.Add(new SelectListItem() { Value = item, Text = item });
            }
            CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"].Value, Selected = true });
            var countryList = _db.Master_Country.Select(o => o.Country).ToList();
            foreach (var item in countryList)
            {
                CountryList.Add(new SelectListItem() { Value = item, Text = item });
            }
            ViewBag.RegionList = RegionLists;

            ViewBag.CountryList = CountryList;




            PIDFViewModel pidf = new PIDFViewModel();
            pidf.Id = id;
            return View(pidf);
           
        }
       
        [HttpPost]
        public ActionResult AddPIDF(PIDFViewModel model)
        {
            if (ModelState.IsValid)
            {
                PIDFDetails pidfDetail = new PIDFDetails();
                pidfDetail.ProductId = IncreamentUnique("EM-", _db.PIDFDetails.Select(o => o.ProductId).LastOrDefault());
                pidfDetail.ProductName = model.ProductName;
                pidfDetail.Strengths = model.Strengths;
                pidfDetail.Region = model.Region;
                pidfDetail.Country = model.Country; 

                _db.PIDFDetails.Add(pidfDetail);
                _db.SaveChanges(); 

                return Json(new { data = "success" }, new JsonSerializerSettings());
            }

            List<SelectListItem> RegionLists = new List<SelectListItem>();
            List<SelectListItem> CountryList = new List<SelectListItem>();
            // RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"].Value });

            RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"].Value, Selected = true });
            var regionLists = _db.Master_Continent.Select(o => o.Continent).ToList();
            foreach (var item in regionLists)
            {
                RegionLists.Add(new SelectListItem() { Value = item, Text = item });
            }
            CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"].Value, Selected = true });
            var countryList = _db.Master_Country.Select(o => o.Country).ToList();
            foreach (var item in countryList)
            {
                CountryList.Add(new SelectListItem() { Value = item, Text = item });
            }
            ViewBag.RegionList = RegionLists;

            ViewBag.CountryList = CountryList;
            RedirectToAction("RegisteredProduct", "Product");

            return View(model);
            
        }

    }
}