using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Models;
using EmcureCERI.Web.Models.DRFViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using EmcureCERI.Business.Contract;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Web.Models.PIDFPViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using EmcureCERI.Web.Models.FileUpload;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using EmcureCERI.Web.Models;
using System.Data;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Business.Contract.ServiceContracts;
using Microsoft.AspNetCore.SignalR;
using EmcureCERI.Web.Hubs;
using Microsoft.EntityFrameworkCore;

namespace EmcureCERI.Web.Controllers
{
    [Authorize]
    public class PIDFController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IPidfServiceNew _pidfServiceNew;
        private readonly IDRFService _DRF;
        private readonly EmcureCERIDBContext _db;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        IHostingEnvironment _env;
        public PIDFController(IPidfServiceNew pidfServiceNew, IDRFService dRFService, IStringLocalizer<SharedResource> sharedLocalizer, IConfiguration config, IHostingEnvironment env, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            _config = config;
            this._pidfServiceNew = pidfServiceNew;
            _DRF = dRFService;
            this._sharedLocalizer = sharedLocalizer;
            _db = new EmcureCERIDBContext();
            this._env = env;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }
        [Authorize]
        public IActionResult Index()
        {
            //IList<PIDFDetailsNew> pIDFDetailsNews = new List<PIDFDetailsNew>();
            //pIDFDetailsNews = _pidfServiceNew.GetAllPIDF(PIDFNumber);
            //if(pIDFDetailsNews.Count>0)
            //{
            //    return View(pIDFDetailsNews);
            //}
            //else
            //{
            return View();
            //}
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        [HttpGet]
        //[AllowAnonymous]
        public IActionResult PIDF(string returnUrl = null)
        {
            //List<SelectListItem> RegionLists = new List<SelectListItem>();
            //List<SelectListItem> CountryList = new List<SelectListItem>();
            List<Master_Continent> regionLists = new List<Master_Continent>();
            List<Master_Country> countryList = new List<Master_Country>();
            // RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"] });

            //RegionLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"], Selected = true });
            regionLists = _db.Master_Continent.ToList();
            //foreach (var item in regionLists)
            //{
            //    RegionLists.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Continent });
            //}
            //CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Continent"], Selected = true });
            countryList = _db.Master_Country.ToList();
            //foreach (var item in countryList)
            //{
            //    CountryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Country });
            //}
            ViewBag.RegionList = regionLists;

            ViewBag.CountryList = countryList;

            List<SelectListItem> ContinentList = new List<SelectListItem>();
            ContinentList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"], Selected = true });
            var COLists = _db.Master_Continent
                 .Where(x => x.IsActive == true)
                .Select(x => new { x.Id, x.Continent });
            foreach (var item in COLists)
            {
                ContinentList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Continent });
            }
            ViewBag.ContinentList = ContinentList;


            return View();
        }

        
        [HttpPost]
        public JsonResult GetRegionList()
        {
            List<Master_Continent> regionLists = new List<Master_Continent>();
            regionLists = _db.Master_Continent.ToList();

            return Json(new { data = regionLists }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult GetCountryList(int id)
        {
            List<Master_Country> countryList = new List<Master_Country>();
            countryList = _db.Master_Country.Where(x => x.ContinentID == id).ToList();

            return Json(new { data = countryList }, new JsonSerializerSettings());
        }

        [HttpPost]
        public JsonResult GetPackingList()
        {
            List<Tbl_Master_Packing> packingLists = new List<Tbl_Master_Packing>();
            packingLists = _db.Tbl_Master_Packing.ToList();

            return Json(new { data = packingLists }, new JsonSerializerSettings());
        }
       // [Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        public JsonResult GetPIDFList()
        {
           
            IList<ApprovedPIDFParentList> pIDFParentCountrywiseLists = new List<ApprovedPIDFParentList>();

            pIDFParentCountrywiseLists = _pidfServiceNew.GetAllApprovedPidfList();

            if (pIDFParentCountrywiseLists.Count > 0)
            {
                for (int i = 0; i < pIDFParentCountrywiseLists.Count; i++)                
                {
                    IList<AprovedPidfCountryList> childrens = new List<AprovedPidfCountryList>();
                    List<AprovedPidfCountryList> parentchildrens = new List<AprovedPidfCountryList>();
                    var tempPidfID = pIDFParentCountrywiseLists[i].PIDFID;
                    childrens = _pidfServiceNew.GetAllApprovedPidfCountryList(Convert.ToInt32(tempPidfID));

                    if (childrens.Count > 0)
                    {
                        for (int j = 0; j < childrens.Count; j++)
                        {
                            AprovedPidfCountryList tempchildrens = new AprovedPidfCountryList();
                            tempchildrens.PIDFID = null;
                            tempchildrens.PIDFNo = null;
                            tempchildrens.ProjectorProductName = childrens[j].ProjectorProductName;
                            tempchildrens.CountryName = childrens[j].CountryName;
                            tempchildrens.PackSizeName = childrens[j].PackSizeName;
                            tempchildrens.PackingName = childrens[j].PackingName;
                            tempchildrens.PidfStrength = childrens[j].PidfStrength;
                            tempchildrens.CIFPricePerPack1 = childrens[j].CIFPricePerPack1;
                            tempchildrens.CIFPricePerPack2 = childrens[j].CIFPricePerPack2;
                            tempchildrens.CIFPricePerPack3 = childrens[j].CIFPricePerPack3;
                            tempchildrens.QtyOneyear = childrens[j].QtyOneyear;
                            tempchildrens.QtyTwoyear = childrens[j].QtyTwoyear;
                            tempchildrens.QtyThreeyear = childrens[j].QtyThreeyear;
                            tempchildrens.VolOneyear = childrens[j].VolOneyear;
                            tempchildrens.VolTwoyear = childrens[j].VolTwoyear;
                            tempchildrens.VolThreeyear = childrens[j].VolThreeyear;
                            //tempchildrens.Action = childrens[j].Action;
                            parentchildrens.Add(tempchildrens);
                        }

                        pIDFParentCountrywiseLists[i].children = parentchildrens;
                    }

                }
            }

            return Json(new { data = pIDFParentCountrywiseLists });
            
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        public IActionResult PIDFList(string PIDFNumber)
        {           

            HttpContext.Session.SetString("Action", "PIDF");
            HttpContext.Session.SetString("ListPageURL", "/PIDF/PIDFStrengthList");
            HttpContext.Session.SetString("DetailsPageURL", "/PIDF/PIDFShowDetails");
            HttpContext.Session.SetString("ButtonDetailPage", "PIDF Details");
            HttpContext.Session.SetString("ButtonListPage", "PIDF List");
            HttpContext.Session.SetString("ButtonSummaryPage", "PIDF List");
            HttpContext.Session.SetString("SummaryPageURL", "/PIDF/PIDFList");
            HttpContext.Session.SetString("GanttSummaryPageTitle", "Project Summary");
            HttpContext.Session.SetString("PidfAction", "PIDF");
            return View();            
        }

        [Authorize(Roles = "HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development,Prescriber")]
        [HttpPost]
        public ActionResult AddPIDFDetails(PIDFHeaderAndDetails pIDFHeaderAndDetails)
        {
            pIDFHeaderAndDetails.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            try
            {                
                int data = _pidfServiceNew.AddPIDFDetails(pIDFHeaderAndDetails);
                if (data > 0)
                {
                    var PIDFID = _db.Tbl_PIDF_Header.AsNoTracking().Select(x => x.PidfID).LastOrDefault();
                   
                    if(pIDFHeaderAndDetails.uploadedfilesdetails.Count>0)
                    {
                        for (int i=0;i< pIDFHeaderAndDetails.uploadedfilesdetails.Count;i++)
                        {
                            UploadedFileModel uploadedFileModel = new UploadedFileModel();
                            uploadedFileModel.PIDFID = Convert.ToInt32(PIDFID);
                            uploadedFileModel.SaveFilePath = pIDFHeaderAndDetails.uploadedfilesdetails[i].SaveFilePath;
                            uploadedFileModel.SaveFileName = pIDFHeaderAndDetails.uploadedfilesdetails[i].SaveFileName;
                            _pidfServiceNew.InsertUploadFileDetails(uploadedFileModel);
                        }
                            
                    }


                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            catch (Exception ex)
            {
                return View(pIDFHeaderAndDetails);
            }
        }

        [Authorize(Roles = "HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development,Prescriber")]
        [HttpPost]
        public ActionResult UpdatePIDFDetails(PIDFHeaderAndDetails pIDFHeaderAndDetails)
        {
            if (ModelState.IsValid)
            {
                int data = _pidfServiceNew.UpdatePIDFDetails(pIDFHeaderAndDetails);
                if (data > 0)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            else
            {
                return View(pIDFHeaderAndDetails);
            }
        }

        //[Authorize(Roles = "Approvers, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("UploadFiles")]
        public IActionResult UploadFiles()
        {
            long size = 0;
            var files = Request.Form.Files;
            if(files.Count>0)
            {
                List<FileModel> listFileModel = new List<FileModel>();
                foreach (var file in files)
                {
                   
                    var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    string[] validExtensionArray = {".XLS", ".XLSX",  ".xlsx", ".xls",  ".pdf", ".PDF", ".doc", ".DOC", ".txt", ".TXT", ".docx", ".DOCS" };

                    if (validExtensionArray.Contains(Path.GetExtension(filename)))
                    {
                        var fileNameWithTimestamp = Path.GetFileNameWithoutExtension(filename) + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(filename);
                        var temp1 = _config.GetSection("UploadFilePath");
                        var filepath = _env.ContentRootPath  + _config.GetSection("UploadFilePath").Value + $@"\{fileNameWithTimestamp}";
                        //var filepath = _config.GetSection("UploadFilePath").Value + $@"\{fileNameWithTimestamp}";
                        size += file.Length;
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        FileModel temp = new FileModel();
                        // temp.SaveFilePath = filepath;
                        temp.SaveFilePath = _config.GetSection("dbUploadFilePath").Value + $@"\{fileNameWithTimestamp}";
                        temp.SaveFileName = fileNameWithTimestamp;

                        listFileModel.Add(temp);

                    }
                    else
                    {
                        string format = "Please upload a file in (xls,xlsx,doc,pdf,docx) these format only.";
                        return Json(new { msg = format, success = false });
                    }

                    
                }
                string message = $"{files.Count} file(s) /{ size} bytes uploaded successfully!";
                return Json(new { msg= message,fileDetails= listFileModel ,success=true});
            }
            else
            {
                string message = "Please select a file.";
                return Json(new { msg=message,success=false });
            }
            
        }

        [Authorize(Roles = "Senior Project Manager,Prescriber")]        
        public IActionResult PIDFNew()
        {
            return View();
        }

        //[Authorize(Roles = "Senior Project Manager,Prescriber")]
        [Authorize]
        public IActionResult PIDFNewCountry(int ID)
        {
            @ViewBag.CountryPidfID = ID;
            return View();
        }

        [Authorize]
        public IActionResult PIDFStrengthList()
        {
            return View();
        }



        [Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development,Prescriber")]
        [HttpGet]
        [ActionName("GetPIDFStrengthList")]
        public ActionResult GetPIDFStrengthList()
        {
            //var result = _pidfServiceNew.GetPIDFStrengthList();
            //return Json(result);

            IList<PIDFParentStrengthList> pIDFParentStrengthLists = new List<PIDFParentStrengthList>();

            pIDFParentStrengthLists = _pidfServiceNew.PIDFParentStrengthList();
           

            if (pIDFParentStrengthLists.Count>0)
            {
                for(int i=0; i< pIDFParentStrengthLists.Count;i++)
                {
                    IList<children> childrens = new List<children>();
                    List<children> parentchildrens = new List<children>();
                    childrens = _pidfServiceNew.PIDFChildrenStrengthList(Convert.ToInt32(pIDFParentStrengthLists[i].PIDFID));

                    if(childrens.Count>0)
                    {
                        for(int j=0;j< childrens.Count;j++)
                        {
                            children tempchildrens = new children();
                            tempchildrens.ProjectorProductName = childrens[j].ProjectorProductName;
                            tempchildrens.PIDFID = null;
                            tempchildrens.PIDFNo = null;
                            tempchildrens.ProductName = null;
                            tempchildrens.PlantName = null;
                            tempchildrens.FormulationName = null;
                            tempchildrens.StatusID = null;
                            tempchildrens.Status = null;
                            tempchildrens.Action = null;
                            parentchildrens.Add(tempchildrens);
                        }

                        pIDFParentStrengthLists[i].children = parentchildrens;
                    }
                   
                }
            }

            return Json( new { data = pIDFParentStrengthLists });
        }

        [Authorize]
        public IActionResult PIDFCountryWiseList(int ID)
        {
            @ViewBag.PIDFCountryListPIDFID = ID;
            return View();
        }

        [Authorize(Roles = "Senior Project Manager,ROI Manager,Prescriber")]
        public IActionResult PIDFEdit(int ID,int CountryID,int StrengthID)
        {
            @ViewBag.PIDFEditPIDFID = ID;
            @ViewBag.PIDFEditCountryID = CountryID;
            @ViewBag.PIDFStrengthID = StrengthID;
            return View();
        }
        public IActionResult PIDFShowDetails(int ID)
        {
            @ViewBag.PIDFShowDetailsPIDFID = ID;            
            return View();
        }

        [Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development,Prescriber")]
        [HttpGet]
        [ActionName("GetPIDFCountrywiseList")]
        public ActionResult GetPIDFCountrywiseList(int ID)
        {
            //var result = _pidfServiceNew.GetPIDFCountrywiseList(ID);
            //return Json(result);

            IList<PIDFParentCountrywiseList> pIDFParentCountrywiseLists = new List<PIDFParentCountrywiseList>();

            pIDFParentCountrywiseLists = _pidfServiceNew.PIDFParentCountrywiseList(ID);

            if (pIDFParentCountrywiseLists.Count > 0)
            {
                for (int i = 0; i < pIDFParentCountrywiseLists.Count; i++)
                {
                    IList<countryChildren> childrens = new List<countryChildren>();
                    List<countryChildren> parentchildrens = new List<countryChildren>();
                    childrens = _pidfServiceNew.PIDFChildrenCountrywiseList(ID);

                    if (childrens.Count > 0)
                    {
                        for (int j = 0; j < childrens.Count; j++)
                        {
                            countryChildren tempchildrens = new countryChildren();
                            tempchildrens.PIDFID = null;
                            tempchildrens.PIDFNo = null;
                            tempchildrens.ProjectorProductName = childrens[j].ProjectorProductName;
                            tempchildrens.CountryName = childrens[j].CountryName;
                            tempchildrens.PackSizeName = childrens[j].PackSizeName;
                            tempchildrens.PackingName = childrens[j].PackingName;
                            tempchildrens.PidfStrength = childrens[j].PidfStrength;
                            tempchildrens.CIFPricePerPack1 = childrens[j].CIFPricePerPack1;
                            tempchildrens.CIFPricePerPack2 = childrens[j].CIFPricePerPack2;
                            tempchildrens.CIFPricePerPack3 = childrens[j].CIFPricePerPack3;
                            tempchildrens.QtyOneyear = childrens[j].QtyOneyear;
                            tempchildrens.QtyTwoyear = childrens[j].QtyTwoyear;
                            tempchildrens.QtyThreeyear = childrens[j].QtyThreeyear;
                            tempchildrens.VolOneyear = childrens[j].VolOneyear;
                            tempchildrens.VolTwoyear = childrens[j].VolTwoyear;
                            tempchildrens.VolThreeyear = childrens[j].VolThreeyear;
                            tempchildrens.Action = childrens[j].Action;
                            parentchildrens.Add(tempchildrens);
                        }

                        pIDFParentCountrywiseLists[i].children = parentchildrens;
                    }

                }
            }

            return Json(new { data = pIDFParentCountrywiseLists });

        }


        [Authorize(Roles = "Senior Project Manager,ROI Manager,Prescriber")]
        [HttpPost]
        [ActionName("InsertInitialPIDFDetails")]
        [Obsolete]
        public async Task<ActionResult> InsertInitialPIDFDetailsAsync(PidfHeaderNew clsPidfHeaderNew)
        {
            var projectName = clsPidfHeaderNew.ProjectorProductName.ToUpper();
            var chkduplicate = (from x in _db.Tbl_PIDF_HeaderNew where x.ProjectorProductName.ToUpper() == projectName select x.ProjectorProductName).ToList();
            if (ModelState.IsValid)
            {
                if (chkduplicate.Count > 0)
                {
                    ViewBag.DuplicateProjectName = "Project Name " + clsPidfHeaderNew.ProjectorProductName.Trim() + " is already exists in database.";

                }
                else
                {

                    //Add strength list                     
                    DataTable tblDetails = new DataTable();
                    tblDetails.Columns.Add(new DataColumn("RowID", typeof(int)));
                    tblDetails.Columns.Add(new DataColumn("PidfStrength", typeof(string)));
                    tblDetails.Columns.Add(new DataColumn("UnitID", typeof(int)));                    

                    if (clsPidfHeaderNew.PidfStrengthList.Length > 0)
                    {
                        List<PidfNewStrength> List = JsonConvert.DeserializeObject<List<PidfNewStrength>>(clsPidfHeaderNew.PidfStrengthList);
                        int intRowID = 1;
                        foreach (var ddata in List)
                        {
                            tblDetails.Rows.Add(intRowID, ddata.Strength, Convert.ToInt16(ddata.UOMid));
                            intRowID++;
                        }
                    }                   

                    Tbl_PIDF_HeaderNew _tbl_PIDF_HeaderNew = new Tbl_PIDF_HeaderNew();
                    _tbl_PIDF_HeaderNew.PidfID = 0;
                    _tbl_PIDF_HeaderNew.PIDFNo = "p1";
                    _tbl_PIDF_HeaderNew.PidfDate = System.DateTime.Now.Date;
                    _tbl_PIDF_HeaderNew.ProjectorProductID = clsPidfHeaderNew.ProjectorProductID;
                    _tbl_PIDF_HeaderNew.ProjectorProductName = clsPidfHeaderNew.ProjectorProductName;
                    _tbl_PIDF_HeaderNew.ProductID = clsPidfHeaderNew.ProductID;
                    _tbl_PIDF_HeaderNew.ProductName = clsPidfHeaderNew.ProductName;
                    _tbl_PIDF_HeaderNew.PlantID = clsPidfHeaderNew.PlantID;
                    _tbl_PIDF_HeaderNew.PlantName = clsPidfHeaderNew.PlantName;
                    _tbl_PIDF_HeaderNew.StrengthID = clsPidfHeaderNew.StrengthID;
                    _tbl_PIDF_HeaderNew.StrengthName = clsPidfHeaderNew.StrengthName;
                    _tbl_PIDF_HeaderNew.UnitID = clsPidfHeaderNew.UnitID;
                    _tbl_PIDF_HeaderNew.UnitName = clsPidfHeaderNew.UnitName;
                    _tbl_PIDF_HeaderNew.FormulationID = clsPidfHeaderNew.FormulationID;
                    _tbl_PIDF_HeaderNew.FormulationName = clsPidfHeaderNew.FormulationName;
                    _tbl_PIDF_HeaderNew.WorkflowID = clsPidfHeaderNew.WorkflowID;
                    _tbl_PIDF_HeaderNew.WorkflowName = clsPidfHeaderNew.WorkflowName;
                    _tbl_PIDF_HeaderNew.ProductManuID = clsPidfHeaderNew.ProductManuID;
                    _tbl_PIDF_HeaderNew.ProductManuName = clsPidfHeaderNew.ProductManuName;
                    _tbl_PIDF_HeaderNew.Strengths = clsPidfHeaderNew.Strengths;
                    _tbl_PIDF_HeaderNew.PidfStatusID = clsPidfHeaderNew.PidfStatusID;
                    _tbl_PIDF_HeaderNew.PidfStatus = clsPidfHeaderNew.PidfStatus;
                    _tbl_PIDF_HeaderNew.BatchSize = clsPidfHeaderNew.BatchSize;
                    _tbl_PIDF_HeaderNew.PackSize = clsPidfHeaderNew.PackSize;
                    _tbl_PIDF_HeaderNew.PackSizeID = clsPidfHeaderNew.PackSizeID;
                    _tbl_PIDF_HeaderNew.PackSizeName = clsPidfHeaderNew.PackSizeName;
                    _tbl_PIDF_HeaderNew.PackingID = clsPidfHeaderNew.PackingID;
                    _tbl_PIDF_HeaderNew.PackingName = clsPidfHeaderNew.PackingName;
                    _tbl_PIDF_HeaderNew.CurrencyID = clsPidfHeaderNew.CurrencyID;
                    _tbl_PIDF_HeaderNew.CurrencyName = clsPidfHeaderNew.CurrencyName;
                    _tbl_PIDF_HeaderNew.COGS = clsPidfHeaderNew.COGS;
                    _tbl_PIDF_HeaderNew.Freight = clsPidfHeaderNew.Freight;
                    _tbl_PIDF_HeaderNew.TotalCIFCost = clsPidfHeaderNew.TotalCIFCost;
                    _tbl_PIDF_HeaderNew.CIFPricePerUnit = clsPidfHeaderNew.CIFPricePerUnit;
                    _tbl_PIDF_HeaderNew.CIFPricePerPack = clsPidfHeaderNew.CIFPricePerPack;
                    _tbl_PIDF_HeaderNew.ProfitPerPack = clsPidfHeaderNew.ProfitPerPack;
                    _tbl_PIDF_HeaderNew.PercentCont = clsPidfHeaderNew.PercentCont;
                    _tbl_PIDF_HeaderNew.QtyOneyear = clsPidfHeaderNew.QtyOneyear;
                    _tbl_PIDF_HeaderNew.QtyTwoyear = clsPidfHeaderNew.QtyTwoyear;
                    _tbl_PIDF_HeaderNew.QtyThreeyear = clsPidfHeaderNew.QtyThreeyear;
                    _tbl_PIDF_HeaderNew.VolOneyear = clsPidfHeaderNew.VolOneyear;
                    _tbl_PIDF_HeaderNew.VolTwoyear = clsPidfHeaderNew.VolTwoyear;
                    _tbl_PIDF_HeaderNew.VolThreeyear = clsPidfHeaderNew.VolThreeyear;
                    _tbl_PIDF_HeaderNew.ContriOne = clsPidfHeaderNew.ContriOne;
                    _tbl_PIDF_HeaderNew.ContriTwo = clsPidfHeaderNew.ContriTwo;
                    _tbl_PIDF_HeaderNew.ContriThree = clsPidfHeaderNew.ContriThree;
                    _tbl_PIDF_HeaderNew.COGS1 = clsPidfHeaderNew.COGS1;
                    _tbl_PIDF_HeaderNew.COGS2 = clsPidfHeaderNew.COGS2;
                    _tbl_PIDF_HeaderNew.COGS3 = clsPidfHeaderNew.COGS3;
                    _tbl_PIDF_HeaderNew.ContributionThreeYear = clsPidfHeaderNew.ContributionThreeYear;
                    _tbl_PIDF_HeaderNew.CostofThreeBatches = clsPidfHeaderNew.CostofThreeBatches;
                    _tbl_PIDF_HeaderNew.RandDCost = clsPidfHeaderNew.RandDCost;
                    _tbl_PIDF_HeaderNew.FilingCost = clsPidfHeaderNew.FilingCost;
                    _tbl_PIDF_HeaderNew.StabilityCost = clsPidfHeaderNew.StabilityCost;
                    _tbl_PIDF_HeaderNew.TotalInvest = clsPidfHeaderNew.TotalInvest;
                    _tbl_PIDF_HeaderNew.AnalyticalCost = clsPidfHeaderNew.AnalyticalCost;
                    _tbl_PIDF_HeaderNew.BECost = clsPidfHeaderNew.BECost;
                    _tbl_PIDF_HeaderNew.RLDCost = clsPidfHeaderNew.RLDCost;
                    _tbl_PIDF_HeaderNew.OtherCost = clsPidfHeaderNew.OtherCost;
                    _tbl_PIDF_HeaderNew.APISource = clsPidfHeaderNew.APISource;
                    _tbl_PIDF_HeaderNew.ROI = clsPidfHeaderNew.ROI;
                    _tbl_PIDF_HeaderNew.RejectionReason = clsPidfHeaderNew.RejectionReason;
                    _tbl_PIDF_HeaderNew.ApprovedById1 = clsPidfHeaderNew.ApprovedById1;
                    _tbl_PIDF_HeaderNew.ApprovedDate1 = clsPidfHeaderNew.ApprovedDate1;
                    _tbl_PIDF_HeaderNew.ApprovedByID1StatusID = clsPidfHeaderNew.ApprovedByID1StatusID;
                    _tbl_PIDF_HeaderNew.ApprovedByID1Remark = clsPidfHeaderNew.ApprovedByID1Remark;
                    _tbl_PIDF_HeaderNew.ApprovedById2 = clsPidfHeaderNew.ApprovedById2;
                    _tbl_PIDF_HeaderNew.ApprovedByDate2 = clsPidfHeaderNew.ApprovedByDate2;
                    _tbl_PIDF_HeaderNew.ApprovedByID2StatusID = clsPidfHeaderNew.ApprovedByID2StatusID;
                    _tbl_PIDF_HeaderNew.ApprovedByID2Remark = clsPidfHeaderNew.ApprovedByID2Remark;
                    _tbl_PIDF_HeaderNew.ApprovedById3 = clsPidfHeaderNew.ApprovedById3;
                    _tbl_PIDF_HeaderNew.ApprovedByDate3 = clsPidfHeaderNew.ApprovedByDate3;
                    _tbl_PIDF_HeaderNew.ApprovedByID3StatusID = clsPidfHeaderNew.ApprovedByID3StatusID;
                    _tbl_PIDF_HeaderNew.ApprovedByID3Remark = clsPidfHeaderNew.ApprovedByID3Remark;
                    _tbl_PIDF_HeaderNew.ApprovedById4 = clsPidfHeaderNew.ApprovedById4;
                    _tbl_PIDF_HeaderNew.ApprovedByDate4 = clsPidfHeaderNew.ApprovedByDate4;
                    _tbl_PIDF_HeaderNew.ApprovedByID4StatusID = clsPidfHeaderNew.ApprovedByID4StatusID;
                    _tbl_PIDF_HeaderNew.ApprovedByID4Remark = clsPidfHeaderNew.ApprovedByID4Remark;
                    _tbl_PIDF_HeaderNew.PidfLastStatusID = clsPidfHeaderNew.PidfLastStatusID;
                    _tbl_PIDF_HeaderNew.PidfLastStatus = clsPidfHeaderNew.PidfLastStatus;
                    _tbl_PIDF_HeaderNew.DueDate = System.DateTime.Now.Date;
                    _tbl_PIDF_HeaderNew.IsActive = true;
                    _tbl_PIDF_HeaderNew.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    _tbl_PIDF_HeaderNew.CreatedDate = System.DateTime.Now.Date;
                    _tbl_PIDF_HeaderNew.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    _tbl_PIDF_HeaderNew.ModifiedDate = System.DateTime.Now.Date;

                    int data = _pidfServiceNew.AddInitialPidfDetails(_tbl_PIDF_HeaderNew, tblDetails);
                    if (data == 1)
                    {
                        ModelState.Clear();

                        //Add email notification
                        //add mail code
                        Int64 tempPidfID;
                        var tempPidf = _db.Tbl_PIDF_HeaderNew.OrderByDescending(x=> x.PidfID).FirstOrDefault();
                        tempPidfID = tempPidf.PidfID;
                        string userMessage = "Project Name : " + clsPidfHeaderNew.ProjectorProductName;
                        string messageTime = Convert.ToString(DateTime.Now.Second) + "seconds ago.";
                        string userName = HttpContext.Session.GetString("CurrentUserName") + " has created following project : ";
                        string strEmailMessage = userName + "</br>" + "Project Name : " + clsPidfHeaderNew.ProjectorProductName;


                        await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                        ModelState.Clear();

                        //send email notification code added by yogesh balapure on date 08/02/2020
                        //get smtp details 
                        SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                        EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Pidf Create");
                        SMTPDetailsVM sMTPDetailsVM = new SMTPDetailsVM();
                        EmailDetailsVM emailDetailsVM = new EmailDetailsVM();

                        if (sMTPDetailsModel != null)
                        {
                            sMTPDetailsVM.AliasName = sMTPDetailsModel.AliasName;
                            sMTPDetailsVM.HostName = sMTPDetailsModel.HostName;
                            sMTPDetailsVM.FromMail = sMTPDetailsModel.FromMail;
                            sMTPDetailsVM.FromPassword = sMTPDetailsModel.FromPassword;
                            sMTPDetailsVM.IsEnableSSL = sMTPDetailsModel.IsEnableSSL;
                            sMTPDetailsVM.PortNumber = sMTPDetailsModel.PortNumber;
                            sMTPDetailsVM.IsMailStatus = sMTPDetailsModel.IsMailStatus;
                            sMTPDetailsVM.IsDefaultCredentials = sMTPDetailsModel.IsDefaultCredentials;
                            sMTPDetailsVM.IsWithoutPassword = sMTPDetailsModel.IsWithoutPassword;
                        }

                        if (emailDetailsModel != null)
                        {
                            //get details
                            emailDetailsVM.ToMail = emailDetailsModel.ToList;
                            List<string> testCC = new List<string>();
                            if (!string.IsNullOrEmpty(emailDetailsModel.CCList))
                            {
                                if (emailDetailsModel.CCList.Contains(";"))
                                {
                                    string[] splitcc = emailDetailsModel.CCList.Split(";");
                                    foreach (var ccdata in splitcc)
                                    {
                                        testCC.Add(ccdata.Trim());
                                    }
                                }
                                else
                                {
                                    testCC.Add(emailDetailsModel.CCList);
                                }
                            }


                            List<string> testBCC = new List<string>();
                            if (!string.IsNullOrEmpty(emailDetailsModel.BCCList))
                            {
                                if (emailDetailsModel.BCCList.Contains(";"))
                                {
                                    string[] splitbcc = emailDetailsModel.BCCList.Split(";");
                                    foreach (var ccdata in splitbcc)
                                    {
                                        testBCC.Add(ccdata.Trim());
                                    }
                                }
                                else
                                {
                                    testBCC.Add(emailDetailsModel.BCCList);
                                }
                            }

                            emailDetailsVM.CCMail = testCC;
                            emailDetailsVM.BCCMail = testBCC;
                            emailDetailsVM.Subject = emailDetailsModel.MailSubject;
                            //emailDetailsVM.Body = emailDetailsModel.MailBody;
                            clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                            string tempurl = _config.GetSection("ApplicationURL:NewPidfUrlLink").Value + tempPidfID;
                            emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                            emailDetailsVM.DispalyName = "";
                        }

                        if (sMTPDetailsModel != null && emailDetailsModel != null)
                        {
                            EmailHelper emailHelper = new EmailHelper();
                            if(Convert.ToBoolean(_config.GetSection("MailSend:IsPidfCreate").Value) == true)
                            {
                                var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                            }                            
                        }
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
                return Json(new { data = "fail", message = ViewBag.DuplicateProjectName }, new JsonSerializerSettings());
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, HOD Of Dossier, President – Commercial, Finance Manager, President – Research & Development,Prescriber")]
        [HttpPost]
        [ActionName("GetDropdownsForPIDFAddDetails")]
        public ActionResult GetDropdownsForPIDFAddDetails()
        {
            PIDFNewVM pIDFNewVM = new PIDFNewVM();
            //For pidf header
            List<clsProduct> ProductList = new List<clsProduct>();
            List<clsPlant> PlantList = new List<clsPlant>();
            List<clsFormulation> FormulationList = new List<clsFormulation>();
            List<clsUnits> UnitList = new List<clsUnits>();
            List<clsStrength> StrengthList = new List<clsStrength>();
            List<clsProductManufacturer> ProductManufacturerList = new List<clsProductManufacturer>();
            List<clsCurrency> CurrencyList = new List<clsCurrency>();
            List<clsPidfWorkflow> WorkflowList = new List<clsPidfWorkflow>();

            //For Pidf country details
            List<clsCountry> clsCountries = new List<clsCountry>();
            List<clsPacking> PackingList = new List<clsPacking>();
            List<clsPackSize> PackSizeList = new List<clsPackSize>();

            //Get Workflow
            var WorkflowResult = (from p in _db.Tbl_Master_Pidf_Workflow
                                  where p.IsActive==true
                                  select new { p.Id, p.WorkflowName }).ToList();
            if (WorkflowResult == null || WorkflowResult.Count <= 0)
            {
                WorkflowResult = null;
            }
            else
            {
                foreach (var ddata in WorkflowResult)
                {
                    WorkflowList.Add(new clsPidfWorkflow { WorkflowID = ddata.Id,  WorkflowName = ddata.WorkflowName });
                }
            }

            //Get Formulation
            var FormulationResult = (from p in _db.Tbl_Master_Formulation
                                     where p.IsActive==true
                                     select new { p.Id, p.Formulation }).ToList();
            if(FormulationResult == null || FormulationResult.Count <=0)
            {
                FormulationList = null;
            }
            else
            {
                foreach (var ddata in FormulationResult)
                {
                    FormulationList.Add(new clsFormulation { FormulationID = ddata.Id, FormulationName = ddata.Formulation });
                }
            }

            //for Strength
            var StrengthResult = (from p in _db.Tbl_Master_Strength
                                  where p.IsActive==true
                                   select new { p.Id, p.Strength }).ToList();
            if (StrengthResult == null || StrengthResult.Count <= 0)
            {
                StrengthList = null;
            }
            else
            {
                foreach (var ddata in StrengthResult)
                {
                    StrengthList.Add(new clsStrength {  StrengthID = ddata.Id,  StrengthName = ddata.Strength });
                }
            }
            //For Units
            var UnitResult = (from p in _db.Tbl_Master_Unit
                              where p.IsActive==true
                                 select new { p.Id, p.UnitName }).ToList();
            if (UnitResult == null || UnitResult.Count <= 0)
            {
                UnitList = null;
            }
            else
            {
                foreach (var ddata in UnitResult)
                {
                    UnitList.Add(new  clsUnits {  UnitID = ddata.Id,  UnitName = ddata.UnitName });
                }
            }
            //For Plant
            var PlantResult = (from p in _db.Tbl_Master_ProductManufacture
                               where p.IsActive==true
                                  select new { p.Id, p.ProductManufacture }).ToList();
            if (PlantResult == null || PlantResult.Count <= 0)
            {
                PlantList = null;
            }
            else
            {
                foreach (var ddata in PlantResult)
                {
                    PlantList.Add(new   clsPlant {   PlantID = ddata.Id,   PlantName = ddata.ProductManufacture });
                }
            }
            //For Product
            var ProductResult = (from p in _db.Tbl_Master_Product
                                 where p.IsActive==true
                               select new { p.Id, p.ProductName }).ToList();
            if (ProductResult == null || ProductResult.Count <= 0)
            {
                ProductList = null;
            }
            else
            {
                foreach (var ddata in ProductResult)
                {
                    ProductList.Add(new  clsProduct {  ProductID = ddata.Id,  ProductName = ddata.ProductName });
                }
            }

            //For Packing
            var PackingResult = (from p in _db.Tbl_Master_PackStyle
                                 where p.IsActive==true
                                 select new { p.Id, p.PackStyle }).ToList();
            if (PackingResult == null || PackingResult.Count <= 0)
            {
                ProductList = null;
            }
            else
            {
                foreach (var ddata in PackingResult)
                {
                    PackingList.Add(new  clsPacking {  PackingID = ddata.Id,  PackingName = ddata.PackStyle });
                }
            }
            //For PackSize
            var PackSizeResult = (from p in _db.Tbl_Master_PackSize
                                  where p.IsActive==true
                                 select new { p.Id, p.PackSize }).ToList();
            if (PackSizeResult == null || PackSizeResult.Count <= 0)
            {
                PackSizeList = null;
            }
            else
            {
                foreach (var ddata in PackSizeResult)
                {
                    PackSizeList.Add(new  clsPackSize {  PackSizeID = ddata.Id,  PackSizeName = ddata.PackSize });
                }
            }
            //For CIS Region Countries
            pIDFNewVM.clsPidfCountryDetailsNew.clsCountries = GetCountryListForPIDF(3);
            pIDFNewVM.clsPidfCountryDetailsNew.clsPackings = PackingList;
            pIDFNewVM.clsPidfCountryDetailsNew.clsPackSizes = PackSizeList;
            //For LATAM  Region Countries
            pIDFNewVM.LATAMRegionDetails.clsCountries = GetCountryListForPIDF(5);
            pIDFNewVM.LATAMRegionDetails.clsPackings = PackingList;
            pIDFNewVM.LATAMRegionDetails.clsPackSizes = PackSizeList;
            //For ASIA Region Countries
            pIDFNewVM.ASIARegionDetails.clsCountries = GetCountryListForPIDF(2);
            pIDFNewVM.ASIARegionDetails.clsPackings = PackingList;
            pIDFNewVM.ASIARegionDetails.clsPackSizes = PackSizeList;
            //For AFRICA Region Countries
            pIDFNewVM.AFRICARegionDetails.clsCountries = GetCountryListForPIDF(1);
            pIDFNewVM.AFRICARegionDetails.clsPackings = PackingList;
            pIDFNewVM.AFRICARegionDetails.clsPackSizes = PackSizeList;
            //For MENA Region Countries
            pIDFNewVM.MENARegionDetails.clsCountries = GetCountryListForPIDF(6);
            pIDFNewVM.MENARegionDetails.clsPackings = PackingList;
            pIDFNewVM.MENARegionDetails.clsPackSizes = PackSizeList;

            pIDFNewVM.clsPidfHeaderNew.ProductList = ProductList;
            pIDFNewVM.clsPidfHeaderNew.PlantList = PlantList;
            pIDFNewVM.clsPidfHeaderNew.StrengthList = StrengthList;
            pIDFNewVM.clsPidfHeaderNew.UnitList = UnitList;
            pIDFNewVM.clsPidfHeaderNew.FormulationList = FormulationList;
            pIDFNewVM.clsPidfHeaderNew.WorkflowList = WorkflowList;

            return Json(new { success = true, data = pIDFNewVM });
        }

        
        [Authorize]
        [HttpPost]
        [ActionName("GetPIDFDetailsForAddCountryDetails")]
        public ActionResult GetPIDFDetailsForAddCountryDetails(int PidfID)
        {
            PIDFNewVM pIDFNewVM = new PIDFNewVM();
           var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == Convert.ToInt64(PidfID) select new { x.ProjectorProductName, x.ProductName,x.PlantName,x.FormulationName,x.WorkflowName,x.PidfStatusID}).FirstOrDefault();
           var strengthresult = _pidfServiceNew.GetPidfStrengthDetails(Convert.ToInt64(PidfID));

            HttpContext.Session.SetString("DrfID", PidfID.ToString());
            HttpContext.Session.SetString("ProductID", result.ProjectorProductName.ToString());
            HttpContext.Session.SetString("Action", "PIDF");
            HttpContext.Session.SetString("ListPageURL", "/PIDF/PIDFStrengthList");
            HttpContext.Session.SetString("DetailsPageURL", "/PIDF/PIDFShowDetails");
            HttpContext.Session.SetString("ButtonDetailPage", "PIDF Details");
            HttpContext.Session.SetString("ButtonListPage", "PIDF List");
            HttpContext.Session.SetString("PidfAction", "PIDF");
            HttpContext.Session.SetString("PidfStatusID", result.PidfStatusID.ToString());


            clsAllContinentData _clsAllContinentData = new clsAllContinentData();
            _clsAllContinentData = _pidfServiceNew.GetAllPidfNewDetails(Convert.ToInt64(PidfID));

            //Get CIS
            List<clsPidfCountryDetailsNew> CISCountryDetails = new List<clsPidfCountryDetailsNew>();
            List<clsPidfCountryDetailsNew> LATAMCountryDetails = new List<clsPidfCountryDetailsNew>();
            List<clsPidfCountryDetailsNew> ASIACountryDetails = new List<clsPidfCountryDetailsNew>();
            List<clsPidfCountryDetailsNew> AFRICACountryDetails = new List<clsPidfCountryDetailsNew>();
            List<clsPidfCountryDetailsNew> MENACountryDetails = new List<clsPidfCountryDetailsNew>();

            if (_clsAllContinentData.CIS_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.CIS_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    CISCountryDetails.Add(objEntity);
                }
            }
            //else
            //{
            //    CISCountryDetails = null;
            //}


            if (_clsAllContinentData.LATAM_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.LATAM_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    LATAMCountryDetails.Add(objEntity);
                }
            }
            //else
            //{
            //    LATAMCountryDetails = null;
            //}


            if (_clsAllContinentData.ASIA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.ASIA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    ASIACountryDetails.Add(objEntity);
                }
            }
            //else
            //{
            //    ASIACountryDetails = null;
            //}


            if (_clsAllContinentData.AFRICA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.AFRICA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    AFRICACountryDetails.Add(objEntity);
                }
            }
            //else
            //{
            //    AFRICACountryDetails = null;
            //}


            if (_clsAllContinentData.MENA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.MENA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    MENACountryDetails.Add(objEntity);
                }
            }
            //else
            //{
            //    MENACountryDetails = null;
            //}

            List<FileModel> uploadFileList = new List<FileModel>();
            var fileresult = (from x in _db.Tbl_PIDF_UploadFileDetails where x.PIDFID == PidfID select new { x.FilePath, x.FileName }).ToList();
            if (fileresult.Count > 0 && fileresult != null)
            {
                foreach (var ddata in fileresult)
                {
                    uploadFileList.Add(new FileModel { SaveFilePath = ddata.FilePath, SaveFileName = ddata.FileName });
                }
            }
            else
            {
                uploadFileList = null;
            }




            return Json(new { success = true, data = result, strengthlist = strengthresult, cisCountryDetails = CISCountryDetails, latamCountryDetails = LATAMCountryDetails, asiaCountryDetails = ASIACountryDetails, africaCountryDetails = AFRICACountryDetails, menaCountryDetails = MENACountryDetails,uploadfilelist = uploadFileList });
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize(Roles = "Senior Project Manager,ROI Manager,CIS Country Manager,Prescriber")]
        [HttpPost]
        [ActionName("InsertCISCountryDetails")]
        public ActionResult InsertCISCountryDetails(clsPidfCountryDetailsNew clsPidfCountryDetailsNew)
        {
            if (ModelState.IsValid)
            {
                //Get pidfno
                var result=(from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == clsPidfCountryDetailsNew.PidfID select new {x.PIDFNo}).FirstOrDefault();

                DataTable tblDetails = GetStringData(clsPidfCountryDetailsNew.CountryDetailsList,result.PIDFNo);

                int data = _pidfServiceNew.AddPidfCountryDetails(tblDetails);
                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,Prescriber")]
        [HttpPost]
        [ActionName("InsertLATAMCountryDetails")]
        public ActionResult InsertLATAMCountryDetails(clsPidfCountryDetailsNew LATAMRegionDetails)
        {
            if (ModelState.IsValid)
            {
                //Get pidfno
                var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == LATAMRegionDetails.PidfID select new { x.PIDFNo }).FirstOrDefault();

                DataTable tblDetails = GetStringData(LATAMRegionDetails.CountryDetailsList, result.PIDFNo);

                int data = _pidfServiceNew.AddPidfCountryDetails(tblDetails);
                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }


        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize(Roles = "Senior Project Manager,ROI Manager,ASIA Country Manager,Prescriber")]
        [HttpPost]
        [ActionName("InsertASIACountryDetails")]
        public ActionResult InsertASIACountryDetails(clsPidfCountryDetailsNew ASIARegionDetails)
        {
            if (ModelState.IsValid)
            {
                //Get pidfno
                var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == ASIARegionDetails.PidfID select new { x.PIDFNo }).FirstOrDefault();

                DataTable tblDetails = GetStringData(ASIARegionDetails.CountryDetailsList, result.PIDFNo);

                int data = _pidfServiceNew.AddPidfCountryDetails(tblDetails);
                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize(Roles = "Senior Project Manager,ROI Manager,AFRICA Country Manager,Prescriber")]
        [HttpPost]
        [ActionName("InsertAFRICACountryDetails")]
        public ActionResult InsertAFRICACountryDetails(clsPidfCountryDetailsNew AFRICARegionDetails)
        {
            if (ModelState.IsValid)
            {
                //Get pidfno
                var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == AFRICARegionDetails.PidfID select new { x.PIDFNo }).FirstOrDefault();

                DataTable tblDetails = GetStringData(AFRICARegionDetails.CountryDetailsList, result.PIDFNo);

                int data = _pidfServiceNew.AddPidfCountryDetails(tblDetails);
                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize(Roles = "Senior Project Manager,ROI Manager,MENA Country Manager, Prescriber")]
        [HttpPost]
        [ActionName("InsertMENACountryDetails")]
        public ActionResult InsertMENACountryDetails(clsPidfCountryDetailsNew MENARegionDetails)
        {
            if (ModelState.IsValid)
            {
                //Get pidfno
                var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == MENARegionDetails.PidfID select new { x.PIDFNo }).FirstOrDefault();

                DataTable tblDetails = GetStringData(MENARegionDetails.CountryDetailsList, result.PIDFNo);

                int data = _pidfServiceNew.AddPidfCountryDetails(tblDetails);
                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        private List<clsCountry> GetCountryListForPIDF(int id)
        {
            List<clsCountry> countryList = new List<clsCountry>();
            var result = _db.Master_Country.AsNoTracking().Where(x => x.ContinentID == id).ToList();
            if(result == null || result.Count <=0)
            {
                countryList = null;
            }
            else
            {
                foreach (var ddata in result)
                {
                    countryList.Add(new clsCountry { CountryID = ddata.Id, CountryName = ddata.Country });
                }
            }
            return countryList;
        }

        private List<clsCountry> GetAllCountryListForPIDF()
        {
            List<clsCountry> countryList = new List<clsCountry>();
            var result = _db.Master_Country.AsNoTracking().ToList();
            if (result == null || result.Count <= 0)
            {
                countryList = null;
            }
            else
            {
                foreach (var ddata in result)
                {
                    countryList.Add(new clsCountry { CountryID = ddata.Id, CountryName = ddata.Country });
                }
            }
            return countryList;
        }

        private DataTable GetStringData(string jsonstring,string pidfno)
        {
            try
            {
                DataTable tblDetails = new DataTable();
                tblDetails.Columns.Add(new DataColumn("RowID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PidfNo", typeof(string)));
                tblDetails.Columns.Add(new DataColumn("ContinentID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("CountryID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("StrengthID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PackSizeID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("PackingID", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerPack1", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolOneyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerPack2", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolTwoyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("CIFPricePerPack3", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("QtyThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("VolThreeyear", typeof(decimal)));
                tblDetails.Columns.Add(new DataColumn("IsActive", typeof(bool)));
                tblDetails.Columns.Add(new DataColumn("Createdby", typeof(int)));
                tblDetails.Columns.Add(new DataColumn("Modifiedby", typeof(int)));

                //tblDetails.Columns.Add(new DataColumn("", typeof(decimal)));
                //tblDetails.Columns.Add(new DataColumn("", typeof(decimal)));

                if (jsonstring.Length > 0)
                {
                    List<CountryDetailsNew> List = JsonConvert.DeserializeObject<List<CountryDetailsNew>>(jsonstring);
                    int intRowID = 1;
                    foreach (var ddata in List)
                    {
                        tblDetails.Rows.Add(intRowID, Convert.ToInt16(ddata.PidfID), pidfno, Convert.ToInt16(ddata.ContinentID), Convert.ToInt16(ddata.CountryID), Convert.ToInt16(ddata.StrengthID), Convert.ToInt16(ddata.PackSizeID), Convert.ToInt16(ddata.PackingID), Convert.ToDecimal(ddata.CIFPricePerPack1), Convert.ToDecimal(ddata.QtyOneyear), Convert.ToDecimal(ddata.VolOneyear), Convert.ToDecimal(ddata.CIFPricePerPack2), Convert.ToDecimal(ddata.QtyTwoyear), Convert.ToDecimal(ddata.VolTwoyear), Convert.ToDecimal(ddata.CIFPricePerPack3), Convert.ToDecimal(ddata.QtyThreeyear), Convert.ToDecimal(ddata.VolThreeyear), true, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")));
                        intRowID++;
                    }
                }

                return tblDetails;
            }
            catch(Exception ex)
            {
                return null;
            }
            


        }

        //Code added by Yogesh on date 20/2/2020
        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("GetPIDFAllCountryDetails")]
        public ActionResult GetPIDFAllCountryDetails(int PidfID)
        {
            PIDFNewVM pIDFNewVM = new PIDFNewVM();
            var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == Convert.ToInt64(PidfID) select new { x.ProjectorProductName, x.ProductName, x.PlantName, x.FormulationName }).FirstOrDefault();
            var strengthresult = _pidfServiceNew.GetPidfStrengthDetails(Convert.ToInt64(PidfID));

            clsAllContinentData _clsAllContinentData = new clsAllContinentData();
            _clsAllContinentData = _pidfServiceNew.GetAllPidfNewDetails(Convert.ToInt64(PidfID));
                       
                //Get CIS
                List<clsPidfCountryDetailsNew> CISCountryDetails = new List<clsPidfCountryDetailsNew>();
                List<clsPidfCountryDetailsNew> LATAMCountryDetails = new List<clsPidfCountryDetailsNew>();
                List<clsPidfCountryDetailsNew> ASIACountryDetails = new List<clsPidfCountryDetailsNew>();
                List<clsPidfCountryDetailsNew> AFRICACountryDetails = new List<clsPidfCountryDetailsNew>();
                List<clsPidfCountryDetailsNew> MENACountryDetails = new List<clsPidfCountryDetailsNew>();

            if(_clsAllContinentData.CIS_ContinentCountries.Count>0)
            {
                foreach (var ddata in _clsAllContinentData.CIS_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    CISCountryDetails.Add(objEntity);
                }
            }
            else
            {
                CISCountryDetails = null;
            }
                

            if (_clsAllContinentData.LATAM_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.LATAM_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    LATAMCountryDetails.Add(objEntity);
                }
            }
            else
            {
                LATAMCountryDetails = null;
            }
            

            if (_clsAllContinentData.ASIA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.ASIA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    ASIACountryDetails.Add(objEntity);
                }
            }
            else
            {
                ASIACountryDetails = null;
            }
            

            if (_clsAllContinentData.AFRICA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.AFRICA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    AFRICACountryDetails.Add(objEntity);
                }
            }
            else
            {
                AFRICACountryDetails = null;
            }
            

            if (_clsAllContinentData.MENA_ContinentCountries.Count > 0)
            {
                foreach (var ddata in _clsAllContinentData.MENA_ContinentCountries)
                {
                    clsPidfCountryDetailsNew objEntity = new clsPidfCountryDetailsNew();
                    objEntity.PidfDetailID = ddata.PidfDetailID;
                    objEntity.PidfID = ddata.PidfID;
                    objEntity.PidfNo = ddata.PidfNo;
                    objEntity.ContinentID = Convert.ToInt32(ddata.ContinentID);
                    objEntity.ContinentName = ddata.ContinentName;
                    objEntity.CountryID = Convert.ToInt32(ddata.CountryID);
                    objEntity.CountryName = ddata.CountryName;
                    objEntity.StrengthID = ddata.StrengthID;
                    objEntity.PidfStrength = ddata.PidfStrength;
                    objEntity.PatentStatusID = Convert.ToInt32(ddata.PatentStatusID);
                    objEntity.PatentStatus = ddata.PatentStatus;
                    objEntity.PackSizeID = Convert.ToInt32(ddata.PackSizeID);
                    objEntity.PackSizeName = ddata.PackSizeName;
                    objEntity.PackingID = Convert.ToInt32(ddata.PackingID);
                    objEntity.PackingName = ddata.PackingName;
                    objEntity.CIFPricePerPack = ddata.CIFPricePerPack;
                    objEntity.CIFPricePerPack1 = ddata.CIFPricePerPack1;
                    objEntity.CIFPricePerPack2 = ddata.CIFPricePerPack2;
                    objEntity.CIFPricePerPack3 = ddata.CIFPricePerPack3;
                    objEntity.QtyOneyear = ddata.QtyOneyear;
                    objEntity.QtyTwoyear = ddata.QtyTwoyear;
                    objEntity.QtyThreeyear = ddata.QtyThreeyear;
                    objEntity.VolOneyear = ddata.VolOneyear;
                    objEntity.VolTwoyear = ddata.VolTwoyear;
                    objEntity.VolThreeyear = ddata.VolThreeyear;
                    objEntity.ContriOne = ddata.ContriOne;
                    objEntity.ContriTwo = ddata.ContriTwo;
                    objEntity.ContriThree = ddata.ContriThree;
                    objEntity.COGS1 = ddata.COGS1;
                    objEntity.COGS2 = ddata.COGS2;
                    objEntity.COGS3 = ddata.COGS3;
                    objEntity.BatchSize = ddata.BatchSize;
                    objEntity.PackSize = ddata.PackSize;
                    objEntity.CurrencyID = Convert.ToInt32(ddata.CurrencyID);
                    objEntity.CurrencyName = ddata.CurrencyName;
                    objEntity.COGS = ddata.COGS;
                    objEntity.Freight = ddata.Freight;
                    objEntity.TotalCIFCost = ddata.TotalCIFCost;
                    objEntity.CIFPricePerUnit = ddata.CIFPricePerUnit;
                    objEntity.ProfitPerPack = ddata.ProfitPerPack;
                    objEntity.PercentCont = ddata.PercentCont;
                    objEntity.ContributionThreeYear = ddata.ContributionThreeYear;
                    objEntity.CostofThreeBatches = ddata.CostofThreeBatches;
                    objEntity.RandDCost = ddata.RandDCost;
                    objEntity.FilingCost = ddata.FilingCost;
                    objEntity.StabilityCost = ddata.StabilityCost;
                    objEntity.TotalInvest = ddata.TotalInvest;
                    objEntity.RejectionReason = ddata.RejectionReason;
                    objEntity.AnalyticalCost = ddata.AnalyticalCost;
                    objEntity.BECost = ddata.BECost;
                    objEntity.RLDCost = ddata.RLDCost;
                    objEntity.OtherCost = ddata.OtherCost;
                    objEntity.APISource = ddata.APISource;
                    objEntity.ROI = ddata.ROI;
                    objEntity.IsActive = ddata.IsActive;
                    objEntity.Createdby = Convert.ToInt32(ddata.Createdby);
                    objEntity.CreatedDate = ddata.CreatedDate;
                    objEntity.Modifiedby = Convert.ToInt32(ddata.Modifiedby);
                    objEntity.ModifiedDate = ddata.ModifiedDate;
                    MENACountryDetails.Add(objEntity);
                }
            }
            else
            {
                MENACountryDetails = null;
            }
            

            return Json(new { success = true, data = result, strengthlist = strengthresult,cisCountryDetails = CISCountryDetails,latamCountryDetails = LATAMCountryDetails,asiaCountryDetails = ASIACountryDetails, africaCountryDetails = AFRICACountryDetails, menaCountryDetails = MENACountryDetails });
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
         [HttpPost]
        [ActionName("GetCountryDetails")]
        public ActionResult GetCountryDetails(int PidfID,int CountryID,int StrengthID)
        {
            PIDFNewVM pIDFNewVM = new PIDFNewVM();
            var result = (from x in _db.Tbl_PIDF_HeaderNew where x.PidfID == Convert.ToInt64(PidfID) select new { x.ProjectorProductName, x.ProductName, x.PlantName, x.FormulationName,x.WorkflowName ,x.PidfStatusID }).FirstOrDefault();
            var strengthresult = _pidfServiceNew.GetPidfStrengthDetails(Convert.ToInt64(PidfID));

            List<clsCountry> clsCountries= GetAllCountryListForPIDF();

            PidfCountryDetailsNew _pidfCountryDetailsNew = new PidfCountryDetailsNew();
            _pidfCountryDetailsNew = _pidfServiceNew.GetCountryDetails(Convert.ToInt64(PidfID), CountryID, StrengthID);

            List<FileModel> uploadFileList = new List<FileModel>();
            var fileresult = (from x in _db.Tbl_PIDF_UploadFileDetails where x.PIDFID == PidfID select new { x.FilePath, x.FileName }).ToList();
            if(fileresult.Count>0 && fileresult !=null)
            {
                foreach (var ddata in fileresult)
                {
                    uploadFileList.Add(new FileModel { SaveFilePath = ddata.FilePath, SaveFileName = ddata.FileName });
                }
            }
            else
            {
                uploadFileList = null;
            }
            

            if (_pidfCountryDetailsNew != null)
            {
                
            }
            else
            {
                _pidfCountryDetailsNew = null;
            }          


            return Json(new { success = true, data = result, strengthlist = strengthresult, cisCountryDetails = _pidfCountryDetailsNew, allCountryList = clsCountries,uploadfilelist= uploadFileList });
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetAllCountryDetails")]
        public ActionResult GetAllCountryDetails(int PidfID)
        {
            IList<PidfCountryDetailsNew> _pidfCountryDetailsNew = new List<PidfCountryDetailsNew>();
            _pidfCountryDetailsNew = _pidfServiceNew.GetAllCountryDetails(PidfID);
            return Json(new { success = true, data = _pidfCountryDetailsNew });
        }

        [Authorize(Roles = "Senior Project Manager,ROI Manager,Prescriber")]
        [HttpPost]
        [ActionName("UpdatePidfCountryDetails")]
        //public ActionResult UpdatePidfCountryDetails(clsPidfCountryDetailsNew clsPidfCountryDetailsNew)
        public ActionResult UpdatePidfCountryDetails(clsPidfCountryDetailsNewUpdateModel clsPidfCountryDetailsNew)
        {
            if (ModelState.IsValid)
            {
                PidfCountryDetailsNew entity = new PidfCountryDetailsNew();
                entity.PidfDetailID = Convert.ToInt64(clsPidfCountryDetailsNew.PidfDetailID);
                entity.PidfID = Convert.ToInt64(clsPidfCountryDetailsNew.PidfID);
                entity.PidfNo = clsPidfCountryDetailsNew.PidfNo;
                entity.ContinentID = clsPidfCountryDetailsNew.ContinentID;
                entity.ContinentName = clsPidfCountryDetailsNew.ContinentName;
                entity.CountryID = clsPidfCountryDetailsNew.CountryID;
                entity.CountryName = clsPidfCountryDetailsNew.CountryName;
                entity.StrengthID = Convert.ToInt64(clsPidfCountryDetailsNew.StrengthID);
                entity.PatentStatusID = clsPidfCountryDetailsNew.PatentStatusID;
                entity.PatentStatus = clsPidfCountryDetailsNew.PatentStatus;
                entity.PackSizeID = clsPidfCountryDetailsNew.PackSizeID;
                entity.PackSizeName = clsPidfCountryDetailsNew.PackSizeName;
                entity.PackingID = clsPidfCountryDetailsNew.PackingID;
                entity.PackingName = clsPidfCountryDetailsNew.PackingName;
                entity.CIFPricePerPack = clsPidfCountryDetailsNew.CIFPricePerPack;
                entity.CIFPricePerPack1 = clsPidfCountryDetailsNew.CIFPricePerPack1;
                entity.CIFPricePerPack2 = clsPidfCountryDetailsNew.CIFPricePerPack2;
                entity.CIFPricePerPack3 = clsPidfCountryDetailsNew.CIFPricePerPack3;
                entity.QtyOneyear = clsPidfCountryDetailsNew.QtyOneyear;
                entity.QtyTwoyear = clsPidfCountryDetailsNew.QtyTwoyear;
                entity.QtyThreeyear = clsPidfCountryDetailsNew.QtyThreeyear;
                entity.VolOneyear = clsPidfCountryDetailsNew.VolOneyear;
                entity.VolTwoyear = clsPidfCountryDetailsNew.VolTwoyear;
                entity.VolThreeyear = clsPidfCountryDetailsNew.VolThreeyear;
                entity.ContriOne = clsPidfCountryDetailsNew.ContriOne;
                entity.ContriTwo = clsPidfCountryDetailsNew.ContriTwo;
                entity.ContriThree = clsPidfCountryDetailsNew.ContriThree;
                entity.COGS1 = clsPidfCountryDetailsNew.COGS1;
                entity.COGS2 = clsPidfCountryDetailsNew.COGS2;
                entity.COGS3 = clsPidfCountryDetailsNew.COGS3;
                entity.BatchSize = clsPidfCountryDetailsNew.BatchSize;
                entity.PackSize = clsPidfCountryDetailsNew.PackSize;
                entity.CurrencyID = clsPidfCountryDetailsNew.CurrencyID;
                entity.CurrencyName = clsPidfCountryDetailsNew.CurrencyName;
                entity.COGS = clsPidfCountryDetailsNew.COGS;
                entity.Freight = clsPidfCountryDetailsNew.Freight;
                entity.TotalCIFCost = clsPidfCountryDetailsNew.TotalCIFCost;
                entity.CIFPricePerUnit = clsPidfCountryDetailsNew.CIFPricePerUnit;
                entity.ProfitPerPack = clsPidfCountryDetailsNew.ProfitPerPack;
                entity.PercentCont = clsPidfCountryDetailsNew.PercentCont;
                entity.ContributionThreeYear = clsPidfCountryDetailsNew.ContributionThreeYear;
                entity.CostofThreeBatches = clsPidfCountryDetailsNew.CostofThreeBatches;
                entity.RandDCost = clsPidfCountryDetailsNew.RandDCost;
                entity.FilingCost = clsPidfCountryDetailsNew.FilingCost;
                entity.StabilityCost = clsPidfCountryDetailsNew.StabilityCost;
                entity.TotalInvest = clsPidfCountryDetailsNew.TotalInvest;
                entity.RejectionReason = clsPidfCountryDetailsNew.RejectionReason;
                entity.AnalyticalCost = clsPidfCountryDetailsNew.AnalyticalCost;
                entity.BECost = clsPidfCountryDetailsNew.BECost;
                entity.RLDCost = clsPidfCountryDetailsNew.RLDCost;
                entity.OtherCost = clsPidfCountryDetailsNew.OtherCost;
                entity.APISource = clsPidfCountryDetailsNew.APISource;
                entity.ROI = clsPidfCountryDetailsNew.ROI;
                entity.IsActive = true;
                entity.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                entity.CreatedDate = System.DateTime.Now.Date;
                entity.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                entity.ModifiedDate = System.DateTime.Now.Date;


                int data = _pidfServiceNew.UpdatePidfCountryDetails(entity);

                if (data == 1)
                {
                    if (clsPidfCountryDetailsNew.uploadedfilesdetails.Length > 0)
                    {
                        //Delete all data from [Tbl_PIDF_UploadFileDetails]               
                        var result = _db.Tbl_PIDF_UploadFileDetails.AsNoTracking().Where(c => c.PIDFID == clsPidfCountryDetailsNew.PidfID).ToList();

                        if (result != null && result.Count > 0)
                        {
                            int deltemp = _pidfServiceNew.DeleteAllUploadFileDetails(Convert.ToInt16(clsPidfCountryDetailsNew.PidfID));
                        }
                        List<FileModel> List = JsonConvert.DeserializeObject<List<FileModel>>(clsPidfCountryDetailsNew.uploadedfilesdetails);

                        foreach (var ddata in List)
                        {
                            UploadedFileModel uploadedFileModel = new UploadedFileModel();
                            uploadedFileModel.PIDFID = Convert.ToInt32(clsPidfCountryDetailsNew.PidfID);
                            uploadedFileModel.SaveFilePath = ddata.SaveFilePath;
                            uploadedFileModel.SaveFileName = ddata.SaveFileName;
                            int inttest = _pidfServiceNew.InsertUploadFileDetails(uploadedFileModel);
                        }
                    }
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
                          
        }
      
        [Authorize]
        public IActionResult ApprovedPIDFShowDetails(int ID)
        {
            @ViewBag.PIDFShowDetailsPIDFID = ID;
            //ViewBag.ActionName= HttpContext.Session.GetString("Action");
            string strAction = HttpContext.Session.GetString("PidfAction");
            HttpContext.Session.SetString("Action", strAction);
            ViewBag.ActionName = HttpContext.Session.GetString("PidfAction");
            @ViewBag.CurrentUserRolePIDF = HttpContext.Session.GetString("CurrentUserRole");
            return View();
        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
         [HttpGet]
        [ActionName("GetPIDFTaskSubTask")]
        public ActionResult GetPIDFTaskSubTask(string PidfID)
        {
            var result = _pidfServiceNew.GetPIDFTAskSubTaskList(PidfID);
            return Json(result);
        }

        [Authorize(Roles = "Prescriber,Senior Project Manager,Regulatory Manager")]
        [HttpPost]
        [ActionName("AddPIDFTaskDetails")]
        [Obsolete]
        public async Task<ActionResult> AddPIDFTaskDetails(DRFTaskAddModel dRFTaskAddModel)
        {
            string strAction = HttpContext.Session.GetString("Action");
            string strProjectName = "";
            string userName = HttpContext.Session.GetString("CurrentUserName") + " has created the following task: ";

            var taskName = dRFTaskAddModel.DRFAddTaskName.Trim().ToUpper();
            var chkduplicate = (from x in _db.Tbl_Master_ProjectTask_Mapping where x.TaskName.ToUpper() == taskName && x.Drfid == dRFTaskAddModel.DRFAddTaskDRFID && x.Action == strAction select x).ToList();
            var duplicateStep = "";
            if (ModelState.IsValid)
            {
                if (chkduplicate.Count > 0)
                {
                    duplicateStep = "Step " + dRFTaskAddModel.DRFAddTaskName.Trim() + " is already exists in database.";
                }
                else
                {
                    Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = new Tbl_Master_ProjectTask_Mapping();
                    tbl_Master_ProjectTask_Mapping.TaskName = dRFTaskAddModel.DRFAddTaskName;
                    tbl_Master_ProjectTask_Mapping.ParentID = 0;                    
                    tbl_Master_ProjectTask_Mapping.Drfid = dRFTaskAddModel.DRFAddTaskDRFID;
                    tbl_Master_ProjectTask_Mapping.StartDate = dRFTaskAddModel.DRFAddTaskStartDate;
                    tbl_Master_ProjectTask_Mapping.EndDate = dRFTaskAddModel.DRFAddTaskEndDate;
                    tbl_Master_ProjectTask_Mapping.PriorityID = dRFTaskAddModel.DRFAddTaskPriorityID;
                    tbl_Master_ProjectTask_Mapping.TaskStatusID = dRFTaskAddModel.DRFAddTaskTaskStatusID;
                    tbl_Master_ProjectTask_Mapping.TaskDuration = dRFTaskAddModel.DRFAddTaskTaskDuration;
                    tbl_Master_ProjectTask_Mapping.TotalPercentage = dRFTaskAddModel.DRFAddTaskTotalPercentage;
                    tbl_Master_ProjectTask_Mapping.EmpID = dRFTaskAddModel.DRFAddTaskEmpID;
                    tbl_Master_ProjectTask_Mapping.IsActive = true;
                    tbl_Master_ProjectTask_Mapping.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.CreatedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.Action = strAction;

                    int data = _DRF.insertDRFTaskDetails(tbl_Master_ProjectTask_Mapping);

                    //get last identity and fetch project name

                    //var result1 = (from TDI in _db.Tbl_DRF_Initialization
                    //               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                    //               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                    //               where TDI.InitializationID == dRFTaskAddModel.DRFAddTaskDRFID
                    //               select new { TDR.ProjectName }).FirstOrDefault();
                    var result1 = (from TDI in _db.Tbl_PIDF_HeaderNew
                                       //join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                                       //join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                                   where TDI.PidfID == dRFTaskAddModel.DRFAddTaskDRFID
                                   select new { TDI.ProjectorProductName }).FirstOrDefault();
                    strProjectName = result1.ProjectorProductName;

                    string userMessage = "Project Name : " + result1.ProjectorProductName + "</br>" + "Task Name : " + dRFTaskAddModel.DRFAddTaskName;
                    string messageTime = Convert.ToString(DateTime.Now.Second) + "seconds ago.";

                    string strEmailMessage = userName + "</br>" + "Project Name : " + result1.ProjectorProductName + "</br>" + "Task Name : " + dRFTaskAddModel.DRFAddTaskName;

                    await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                    ModelState.Clear();

                    //send email notification code added by yogesh balapure on date 08/02/2020
                    //get smtp details 
                    SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                    EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Pidf Task Create");
                    SMTPDetailsVM sMTPDetailsVM = new SMTPDetailsVM();
                    EmailDetailsVM emailDetailsVM = new EmailDetailsVM();

                    if (sMTPDetailsModel != null)
                    {
                        sMTPDetailsVM.AliasName = sMTPDetailsModel.AliasName;
                        sMTPDetailsVM.HostName = sMTPDetailsModel.HostName;
                        sMTPDetailsVM.FromMail = sMTPDetailsModel.FromMail;
                        sMTPDetailsVM.FromPassword = sMTPDetailsModel.FromPassword;
                        sMTPDetailsVM.IsEnableSSL = sMTPDetailsModel.IsEnableSSL;
                        sMTPDetailsVM.PortNumber = sMTPDetailsModel.PortNumber;
                        sMTPDetailsVM.IsMailStatus = sMTPDetailsModel.IsMailStatus;
                        sMTPDetailsVM.IsDefaultCredentials = sMTPDetailsModel.IsDefaultCredentials;
                        sMTPDetailsVM.IsWithoutPassword = sMTPDetailsModel.IsWithoutPassword;
                    }

                    if (emailDetailsModel != null)
                    {
                        //get details
                        emailDetailsVM.ToMail = emailDetailsModel.ToList;
                        List<string> testCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.CCList))
                        {
                            if (emailDetailsModel.CCList.Contains(";"))
                            {
                                string[] splitcc = emailDetailsModel.CCList.Split(";");
                                foreach (var ccdata in splitcc)
                                {
                                    testCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testCC.Add(emailDetailsModel.CCList);
                            }
                        }
                            

                        List<string> testBCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.BCCList))
                        {
                            if (emailDetailsModel.BCCList.Contains(";"))
                            {
                                string[] splitbcc = emailDetailsModel.BCCList.Split(";");
                                foreach (var ccdata in splitbcc)
                                {
                                    testBCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testBCC.Add(emailDetailsModel.BCCList);
                            }
                        }
                            
                        emailDetailsVM.CCMail = testCC;
                        emailDetailsVM.BCCMail = testBCC;
                        emailDetailsVM.Subject = emailDetailsModel.MailSubject;
                        //emailDetailsVM.Body = emailDetailsModel.MailBody;
                        clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                        string tempurl= _config.GetSection("ApplicationURL:ApprovedPidfUrlLink").Value + dRFTaskAddModel.DRFAddTaskDRFID;
                        emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                        emailDetailsVM.DispalyName = "";
                    }

                    if (sMTPDetailsModel != null && emailDetailsModel != null)
                    {
                        EmailHelper emailHelper = new EmailHelper();
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsTaskCreate").Value) == true)
                        {
                            var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                        }
                            
                    }

                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }

                return Json(new { data = "fail", message = duplicateStep }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail", message = "Please enter proper values." }, new JsonSerializerSettings());

            }

        }

        [Authorize(Roles = "Prescriber,Senior Project Manager,Regulatory Manager")]
        [HttpPost]
        [ActionName("AddPIDFSubTaskDetails")]
        public ActionResult AddPIDFSubTaskDetails(DRFSubTaskAddModel dRFSubTaskAddModel)
        {
            string strAction = HttpContext.Session.GetString("Action");
            //string strProjectName = "";
            var subTaskName = dRFSubTaskAddModel.DRFAddSubTaskName.Trim().ToUpper();
            var chkduplicate = (from x in _db.Tbl_Master_ProjectTask_Mapping where x.TaskName.ToUpper() == subTaskName && x.Drfid == dRFSubTaskAddModel.DRFAddSubTaskDRFID && x.ParentID != dRFSubTaskAddModel.DRFAddSubTaskTaskID && x.Action== strAction select x).ToList();
            var duplicateStep = "";
            if (ModelState.IsValid)
            {
                if (chkduplicate.Count > 0)
                {
                    duplicateStep = "Step " + dRFSubTaskAddModel.DRFAddSubTaskName.Trim() + " is already exists in database.";
                }
                else
                {
                    Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = new Tbl_Master_ProjectTask_Mapping();
                    tbl_Master_ProjectTask_Mapping.TaskName = dRFSubTaskAddModel.DRFAddSubTaskName;
                    tbl_Master_ProjectTask_Mapping.ParentID = dRFSubTaskAddModel.DRFAddSubTaskTaskID;
                    tbl_Master_ProjectTask_Mapping.Drfid = dRFSubTaskAddModel.DRFAddSubTaskDRFID;
                    tbl_Master_ProjectTask_Mapping.StartDate = dRFSubTaskAddModel.DRFAddSubTaskStartDate;
                    tbl_Master_ProjectTask_Mapping.EndDate = dRFSubTaskAddModel.DRFAddSubTaskEndDate;
                    tbl_Master_ProjectTask_Mapping.PriorityID = dRFSubTaskAddModel.DRFAddSubTaskTaskPriorityID;
                    tbl_Master_ProjectTask_Mapping.TaskStatusID = dRFSubTaskAddModel.DRFAddSubTaskTaskStatusID;
                    tbl_Master_ProjectTask_Mapping.TaskDuration = dRFSubTaskAddModel.DRFAddSubTaskTaskDuration;
                    tbl_Master_ProjectTask_Mapping.TotalPercentage = dRFSubTaskAddModel.DRFAddSubTaskTotalPercentage;
                    tbl_Master_ProjectTask_Mapping.EmpID = dRFSubTaskAddModel.DRFAddSubTaskEmpID;
                    tbl_Master_ProjectTask_Mapping.IsActive = true;
                    tbl_Master_ProjectTask_Mapping.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.CreatedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.Action = strAction;

                    int data = _DRF.insertDRFSubTaskDetails(tbl_Master_ProjectTask_Mapping);

                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }

                return Json(new { data = "fail", message = duplicateStep }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail", message = "Please enter proper values." }, new JsonSerializerSettings());

            }

        }

        //[Authorize(Roles = "Senior Project Manager,ROI Manager,LATAM Country Manager,CIS Country Manager,ASIA Country Manager,AFRICA Country Manager,MENA Country Manager, Sr. Vice President-Emerging Market, President – Commercial, General Manager - Finance, President – Research & Development,Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("GetDRFList")]
        public ActionResult GetDRFList(int PidfID)
        {         

            IList<Tbl_DRF_Initialization> list = new List<Tbl_DRF_Initialization>();
            list = _pidfServiceNew.GetDRFList(PidfID);
            return Json(new { data = list });
        }
        [Authorize]
        [HttpPost]
        public JsonResult UpdatePidfInitialApproval(UpdateApprovalPIDFStatusRequestModels updateApprovalPIDFStatusRequestModels)
        {
            try
            {
                int? id = updateApprovalPIDFStatusRequestModels.pidfID;
                //Get pidf details
                Tbl_PIDF_HeaderNew tbl_PIDF_HeaderNew = _db.Tbl_PIDF_HeaderNew.AsNoTracking().Where(x => x.PidfID == Convert.ToInt64(id)).FirstOrDefault();
                if(tbl_PIDF_HeaderNew != null )
                {
                    tbl_PIDF_HeaderNew.PidfStatusID = 12;
                    tbl_PIDF_HeaderNew.PidfStatus = "Initial Approved";

                    tbl_PIDF_HeaderNew.IsActive = true;
                    tbl_PIDF_HeaderNew.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_PIDF_HeaderNew.ModifiedDate = DateTime.Now;
                    _db.Entry(tbl_PIDF_HeaderNew).State = EntityState.Modified;
                    _db.SaveChanges();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            catch(Exception ex)
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
            //List<Master_Country> countryList = new List<Master_Country>();
            //countryList = _db.Master_Country.Where(x => x.ContinentID == id).ToList();

            //return Json(new { data = countryList }, new JsonSerializerSettings());
        }


        [Authorize(Roles = "Senior Project Manager,ROI Manager,Prescriber")]
        [HttpPost]
        [ActionName("UpdatePidfUploadFileDetails")]
        //public ActionResult UpdatePidfCountryDetails(clsPidfCountryDetailsNew clsPidfCountryDetailsNew)
        public ActionResult UpdatePidfUploadFileDetails(clsPidfCountryDetailsNewUpdateModel clsPidfCountryDetailsNew)
        {
            if (clsPidfCountryDetailsNew.uploadedfilesdetails.Length > 0)
            {
                //Delete all data from [Tbl_PIDF_UploadFileDetails]               
                var result = _db.Tbl_PIDF_UploadFileDetails.AsNoTracking().Where(c => c.PIDFID == clsPidfCountryDetailsNew.PidfID).ToList();

                if (result != null && result.Count > 0)
                {
                    int deltemp = _pidfServiceNew.DeleteAllUploadFileDetails(Convert.ToInt16(clsPidfCountryDetailsNew.PidfID));
                }
                List<FileModel> List = JsonConvert.DeserializeObject<List<FileModel>>(clsPidfCountryDetailsNew.uploadedfilesdetails);

                foreach (var ddata in List)
                {
                    UploadedFileModel uploadedFileModel = new UploadedFileModel();
                    uploadedFileModel.PIDFID = Convert.ToInt32(clsPidfCountryDetailsNew.PidfID);
                    uploadedFileModel.SaveFilePath = ddata.SaveFilePath;
                    uploadedFileModel.SaveFileName = ddata.SaveFileName;
                    int inttest = _pidfServiceNew.InsertUploadFileDetails(uploadedFileModel);
                }
            }                  
            return Json(new { data = "success" }, new JsonSerializerSettings());             

        }
    }
}
