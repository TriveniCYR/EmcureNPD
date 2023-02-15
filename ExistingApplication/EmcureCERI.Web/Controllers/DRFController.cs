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
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using EmcureCERI.Business.Core.ServiceImplementations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using EmcureCERI.Web.Hubs;
using EmcureCERI.Web.Models;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Classes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace EmcureCERI.Web.Controllers
{
    [Authorize]
    public class DRFController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IDRFService _DRF;
        private readonly EmcureCERIDBContext _db;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMasterSubTaskService _SubTask;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public DRFController(IConfiguration config, IHostingEnvironment env, IDRFService dRFService, IStringLocalizer<SharedResource> sharedLocalizer, IMasterSubTaskService masterSubTaskService, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            this._DRF = dRFService;
            this._sharedLocalizer = sharedLocalizer;
            _db = new EmcureCERIDBContext();
            this._SubTask = masterSubTaskService;
            _config = config;
            _env = env;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }

        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public IActionResult GetGantt()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpGet]
        public ActionResult DRFList(int id = 0)
        {
            return View();
        }

       // [Authorize(Roles = "Prescriber")]
       [Authorize]
        [HttpGet]
        public ActionResult DRFAddOrEdit()
        {
            GetDropdownList();
            return View();
        }

        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult DRFShowDetails(string Id)
        {
            var DRFList = (from drf in _db.DRFDetails
                           join c in _db.Tbl_DSR_NewProductContinentMapping on drf.Id equals c.NewProductID
                           join cc in _db.Master_Continent on c.ContinentID equals cc.Id
                           join ccc in _db.Master_Country on c.CountryID equals ccc.Id
                           join f in _db.Tbl_Master_Formulation on drf.FormulationID equals f.Id
                           join tc in _db.Tbl_Master_TherapeuticCategory on drf.TherapeuticCategoryID equals tc.Id
                          // join pm in _db.Tbl_Master_ProductManufacture on drf.ProductManufactureID equals pm.Id
                           from pm in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == drf.ProductManufactureID).DefaultIfEmpty()
                           join dt in _db.Tbl_Master_DossierTemplate on drf.DossierTemplateID equals dt.Id
                           join dc in _db.Tbl_Master_DrugCategory on drf.DrugCategoryID equals dc.Id
                           join pd in _db.PrescriberDetails on drf.ProjectManagerID equals pd.AspNetUserId
                           where drf.ProductId == Id
                           select new
                           {
                               Id = drf.Id,
                               ProductId = drf.ProductId,
                               ProductName = drf.ProductName,
                               APIName = drf.APIName,
                               APIVolume = drf.APIVolume,
                               APIVendor = drf.APIVendor,
                               Strength = drf.Strength,
                               ModuleName = drf.ModuleName,
                               Formulation = f.Formulation,
                               Continent = cc.Continent,
                               Country = ccc.Country,
                               CountryID = c.CountryID,
                               RegistrationDate = drf.RegistrationDate.Value.ToString("dd/MM/yyyy"),
                               ReRegistrationDate = drf.ReRegistrationDate.Value.ToString("dd/MM/yyyy"),
                               DossierFillingDate = drf.CreatedDate.Value.ToString("dd/MM/yyyy"),
                               RegisterPlant = drf.RegisterPlant,
                               ManufacturingPlant = pm.ProductManufacture,
                               TherapeuticCategory = tc.TherapeuticCategory,
                               DossierTemplate = dt.DossierTemplate,
                               DrugCategory = dc.DrugCategory,
                               SubmissionChecklist = drf.SubmissionChecklist,
                               WHOPQ = drf.WHOPQ,
                               ProjectManager = pd.FirstName + " " + pd.LastName
                           }).FirstOrDefault();

            HttpContext.Session.SetString("DrfID", DRFList.Id.ToString());
            HttpContext.Session.SetString("ProductID", DRFList.ProductId.ToString());
            HttpContext.Session.SetString("ProjectManager", DRFList.ProjectManager.ToString());
            HttpContext.Session.SetString("Action","DRF");
            ViewBag.DRFID = DRFList.Id;
            ViewBag.DRFProductID = DRFList.ProductId;
            ViewBag.DRFProductName = DRFList.ProductName;
            ViewBag.DRFAPIName = DRFList.APIName;
            ViewBag.DRFAPIVolume = DRFList.APIVolume;
            ViewBag.DRFAPIVendor = DRFList.APIVendor;
            ViewBag.DRFStrength = DRFList.Strength;
            ViewBag.DRFModuleName = DRFList.ModuleName;
            ViewBag.DRFRegistrationDate = DRFList.RegistrationDate;
            ViewBag.DRFReRegistrationDate = DRFList.ReRegistrationDate;
            ViewBag.DRFRegisterPlant = DRFList.RegisterPlant;
            ViewBag.DRFTherapeuticCategory = DRFList.TherapeuticCategory;
            ViewBag.DRFManufacturingPlant = DRFList.ManufacturingPlant;
            ViewBag.DRFFormulation = DRFList.Formulation;
            ViewBag.DRFDossierTemplate = DRFList.DossierTemplate;
            ViewBag.DRFDrugCategory = DRFList.DrugCategory;
            ViewBag.DRFSubmissionChecklist = DRFList.SubmissionChecklist;
            ViewBag.DRFWHOPQ = DRFList.WHOPQ;
            ViewBag.DRFContinent = DRFList.Continent;
            ViewBag.DRFCountry = DRFList.Country;
            ViewBag.DRFCountryID = DRFList.CountryID;
            return View();
        }

        //[Authorize(Roles = "Prescriber")]
        //[HttpPost]
        //[ActionName("GetCountryList")]
        //public ActionResult GetCountryList(DRFContinentCountry dRFContinentCountry)
        //{
        //    //ViewBag.CountryList =null;
        //    List<SelectListItem> CountryList = new List<SelectListItem>();
        //    CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
        //    var COLists = _db.Master_Country
        //         .Where(x => x.IsActive == true && x.ContinentID== dRFContinentCountry.ContinentID)
        //        .Select(x => new { x.Id, x.Country });
        //    foreach (var item in COLists)
        //    {
        //        CountryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Country });
        //    }

        //    ViewBag.CountryList = CountryList;
        //   // return new OkObjectResult(new { "t" });
        //    return Json(new { success = true, message = _sharedLocalizer["Country List Retrive Successfully"].Value,Data= CountryList }); 
        //    //return this.Request.CreateResponse(HttpStatusCode.OK,"Done");

        //}

        public JsonResult GetCountryList(DRFContinentCountry dRFContinentCountry)
        {
            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var COLists = _db.Master_Country
                .AsNoTracking()
                 .Where(x => x.IsActive == true && x.ContinentID == dRFContinentCountry.ContinentID)
                 .OrderBy(x => x.Country)
                .Select(x => new { x.Id, x.Country });
            foreach (var item in COLists)
            {
                CountryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Country });
            }

            return Json(new SelectList(CountryList, "Value", "Text"));
        }

       // [Authorize(Roles = "Prescriber")]
       [Authorize]
        [HttpPost]
        [ActionName("GetPIDFListForAttached")]
        public ActionResult GetPIDFListForAttached(PIDFByCountryInDRF pIDFByCountryInDRF)
        {
            //var PIDFLists = _db.PIDFDetails
            //     .Where(x => x.CountryId == pIDFByCountryInDRF.CountryID)
            //    .Select(x => new { x.Id, x.ProductId,x.ProductName, x.Country,x.Region });
            //Tbl_PIDF_Header join Tbl_Master_PIDFStatus on projectStatus bring list of only "Approved PIDF" status with country

            var PIDFLists = (from pidf in _db.Tbl_PIDF_Header
                             join pidfc in _db.Tbl_PIDF_CountryDetails on pidf.PidfID equals pidfc.PidfID
                             join cc in _db.Master_Continent on pidfc.ContinentID equals cc.Id
                             join ccc in _db.Master_Country on pidfc.CountryID equals ccc.Id
                             where pidfc.CountryID == pIDFByCountryInDRF.CountryID && pidf.PidfStatusID == 5
                             select new
                             {
                                 Id = pidf.PidfID,
                                 ProductId = pidf.PIDFNo,
                                 ProductName = pidf.ProjectorProductName,
                                 Region = cc.Continent,
                                 Country = ccc.Country,
                             }).ToList();


            return Json(new { success = true, message = _sharedLocalizer["Country List Retrive Successfully"].Value, Data = PIDFLists });

        }

        protected virtual string IncreamentUnique(string prefix, string unique)
        {
            if (unique != null && unique != "")
            {
                var suffixNum = unique.Split(prefix);
                int increment = Convert.ToInt32(suffixNum[1]) + 1;
                return string.Concat(prefix, increment.ToString("000000"));
            }
            else
            {
                return "";
            }
        }

        [Authorize]
       // [Authorize(Roles = "Prescriber")]
        [HttpPost]
        public ActionResult DRFAddOrEdit(DRFViewModel dRFViewModel)
        {

            var productName = dRFViewModel.ProductName.Trim().ToUpper();
            var chkduplicate = (from x in _db.DRFDetails where x.ProductName.ToUpper() == productName select x).ToList();
            GetDropdownList();
            if (ModelState.IsValid)
            {

                if (chkduplicate.Count > 0)
                {
                    ViewBag.Duplicate = "Project Name " + dRFViewModel.ProductName.Trim() + " is already exists in database.";

                }
                else
                {
                    DRFDetails dRFDetails = new DRFDetails();
                    var uniqueID = _db.DRFDetails.Select(o => o.ProductId).LastOrDefault();


                    if (uniqueID != null)
                    {
                        dRFDetails.ProductId = IncreamentUnique("EM - ", uniqueID);
                    }
                    else
                    {
                        dRFDetails.ProductId = "EM - 000001";
                    }

                    dRFDetails.ProductName = dRFViewModel.ProductName.Trim();
                    dRFDetails.APIName = dRFViewModel.APIName.Trim();
                    dRFDetails.APIVendor = dRFViewModel.APIVendor.Trim();
                    dRFDetails.APIVolume = dRFViewModel.APIVolume.Trim();
                    dRFDetails.Strength = dRFViewModel.Strength.Trim();
                    dRFDetails.ModuleName = dRFViewModel.ModuleName.Trim();
                    dRFDetails.RegistrationDate = Convert.ToDateTime(dRFViewModel.RegistrationDate.Trim());
                    dRFDetails.ReRegistrationDate = Convert.ToDateTime(dRFViewModel.ReRegistrationDate.Trim());
                    dRFDetails.RegisterPlant = dRFViewModel.RegisterPlant;
                    dRFDetails.TherapeuticCategoryID = dRFViewModel.TherapeuticCategoryID;
                    dRFDetails.ProductManufactureID = dRFViewModel.ProductManufactureID;
                    dRFDetails.FormulationID = dRFViewModel.FormulationID;
                    dRFDetails.DrugCategoryID = dRFViewModel.DrugCategoryID;
                    dRFDetails.DossierTemplateID = dRFViewModel.DossierTemplateID;
                    dRFDetails.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    dRFDetails.CreatedDate = DateTime.Now;
                    dRFDetails.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    dRFDetails.UpdatedDate = DateTime.Now;
                    dRFDetails.SubmissionChecklist = dRFViewModel.SubmissionChecklist.ToString();
                    dRFDetails.WHOPQ = dRFViewModel.WHOPQ;
                    dRFDetails.ProjectManagerID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    if (dRFViewModel.PIDFID == null || dRFViewModel.PIDFID == 0)
                    {
                        dRFDetails.ProjectStatusID = 6; //Pending Approvel
                        dRFDetails.LastProjectStatusID = 6;
                    }
                    else
                    {
                        dRFDetails.ProjectStatusID = 1; //Completed
                        dRFDetails.LastProjectStatusID = 1;
                    }

                    _DRF.AddDRFDetails(dRFDetails);

                    var primaryID = _db.DRFDetails.Select(o => o.Id).LastOrDefault();

                    Tbl_DSR_NewProductContinentMapping tbl_DSR_NewProductContinentMapping = new Tbl_DSR_NewProductContinentMapping();
                    tbl_DSR_NewProductContinentMapping.NewProductID = Convert.ToInt32(primaryID);
                    tbl_DSR_NewProductContinentMapping.ContinentID = dRFViewModel.ContinentID;
                    tbl_DSR_NewProductContinentMapping.CountryID = dRFViewModel.CountryID;
                    tbl_DSR_NewProductContinentMapping.CreatedDate = DateTime.Now;
                    tbl_DSR_NewProductContinentMapping.ModifyDate = DateTime.Now;
                    _DRF.DRFContinentCountryMapping(tbl_DSR_NewProductContinentMapping);
                    //_db.Tbl_DSR_NewProductContinentMapping.Add(tbl_DSR_NewProductContinentMapping);

                    if (dRFViewModel.PIDFID != null && dRFViewModel.PIDFID != 0)
                    {
                        Tbl_DRF_PIDF_Mapping tbl_DRF_PIDF_Mapping = new Tbl_DRF_PIDF_Mapping();
                        tbl_DRF_PIDF_Mapping.DRFID = Convert.ToInt32(primaryID);
                        tbl_DRF_PIDF_Mapping.PIDFID = dRFViewModel.PIDFID;
                        _DRF.DRFPIDFMapping(tbl_DRF_PIDF_Mapping);
                        //_db.Tbl_DRF_PIDF_Mapping.Add(tbl_DRF_PIDF_Mapping);
                    }

                    if (dRFViewModel.RegistrationFeesArray.Length > 0)
                    {
                        List<RegistrationFeesArray> feesList = JsonConvert.DeserializeObject<List<RegistrationFeesArray>>(dRFViewModel.RegistrationFeesArray);

                        for (int i = 0; i < feesList.Count; i++)
                        {
                            Tbl_DRF_Percentage_Mapping tbl_DRF_Percentage_Mapping = new Tbl_DRF_Percentage_Mapping();
                            tbl_DRF_Percentage_Mapping.DRFID = Convert.ToInt32(primaryID);
                            tbl_DRF_Percentage_Mapping.PerID = feesList[i].ID;
                            tbl_DRF_Percentage_Mapping.PerValue = feesList[i].Value;
                            _DRF.DRFRegistrationFeesMapping(tbl_DRF_Percentage_Mapping);
                        }
                    }

                    if (dRFViewModel.DossierTemplateID == 1 || dRFViewModel.DossierTemplateID == 4 || (dRFViewModel.DossierTemplateID == 2 && dRFViewModel.CountryID != 40 && dRFViewModel.ContinentID == 3))
                    {
                        CreateFolders(dRFViewModel.ProductName.Trim());
                    }


                    // IList<Tbl_Master_SubTask> TaskSubTaskList = new List<Tbl_Master_SubTask>();
                    //TaskSubTaskList = _SubTask.GetAllSubTask(0);

                    IList<DRFTaskSubTaskOutput> drFTaskSubTaskOutputs = new List<DRFTaskSubTaskOutput>();
                    drFTaskSubTaskOutputs = _DRF.GetMixedTaskSubTaskListForDRFInsertion();

                    for (int i = 0; i < drFTaskSubTaskOutputs.Count; i++)
                    {
                        TaskSubTaskInputs taskSubTaskInputs = new TaskSubTaskInputs();

                        taskSubTaskInputs.TaskOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                        taskSubTaskInputs.TaskName = drFTaskSubTaskOutputs[i].TaskName;
                        //taskSubTaskInputs.SortOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                        var tempList = (from TMST in _db.Tbl_Master_SubTask
                                        join TMT in _db.Tbl_Master_Task on TMST.TaskID equals TMT.TaskID
                                        where TMST.SubTaskName == drFTaskSubTaskOutputs[i].TaskName
                                        select new
                                        {
                                            TMST.SubTaskName,
                                            TMT.TaskName
                                        }).ToList();

                        if (tempList.Count > 0)
                        {

                            var tempID = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                                          where TMPM.TaskName == tempList[0].TaskName && TMPM.Drfid == Convert.ToInt32(primaryID) && TMPM.Action=="DRF"
                                          select new
                                          {
                                              TMPM.ProjectTaskMappingID
                                          }).ToList();

                            taskSubTaskInputs.ParentID = Convert.ToInt32(tempID[0].ProjectTaskMappingID);

                        }
                        else
                        {
                            taskSubTaskInputs.ParentID = 0;
                        }


                        taskSubTaskInputs.DRFID = Convert.ToInt32(primaryID);
                        taskSubTaskInputs.StartDate = DateTime.Today;
                        taskSubTaskInputs.EndDate = DateTime.Today.AddDays(1);
                        taskSubTaskInputs.PriorityID = 1;// Id of Normal Priority.
                        taskSubTaskInputs.Priority = "Normal";
                        taskSubTaskInputs.TaskStatusID = 8;//Initial Status
                        taskSubTaskInputs.TaskStatus = "Initial";
                        taskSubTaskInputs.TaskDuration = 1;
                        taskSubTaskInputs.TotalPercentage = 0;
                        taskSubTaskInputs.EmpID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        // taskSubTaskInputs.Type = "";
                        taskSubTaskInputs.IsActive = true;
                        taskSubTaskInputs.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        taskSubTaskInputs.CreatedDate = DateTime.Today;
                        taskSubTaskInputs.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        taskSubTaskInputs.ModifiedDate = DateTime.Today;

                        int data = _DRF.InsertTaskSubTaskDetails(taskSubTaskInputs);
                    }

                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }

                return Json(new { data = "fail", message = ViewBag.Duplicate }, new JsonSerializerSettings());
            }
            else
            {
                return View(dRFViewModel);

            }

        }


        public void CreateFolders(string ProjectName)
        {
            //string ProjectFolder = @"D:\Emcure\" + "EM-" + ProjectName + "0000";


            string ProjectFolder = _config.GetSection("EmcureFolderPath").Value+ "root_EM-" + ProjectName + "000000/" + "EM-" + ProjectName + "000000"; 

            if (!Directory.Exists(ProjectFolder))
            {
                Directory.CreateDirectory(ProjectFolder);
            }

            //Module 1
            string Module1 = ProjectFolder + @"\Module1";
            if (!Directory.Exists(Module1))
            {
                Directory.CreateDirectory(Module1);
            }

            string subModule12 = Module1 + @"\12-Cover";
            if (!Directory.Exists(subModule12))
            {
                Directory.CreateDirectory(subModule12);
            }

            string subModule13 = Module1 + @"\13-Form";
            if (!Directory.Exists(subModule13))
            {
                Directory.CreateDirectory(subModule13);
            }

            string subModule14 = Module1 + @"\14-PI";
            if (!Directory.Exists(subModule14))
            {
                Directory.CreateDirectory(subModule14);
            }

            string subModule141 = subModule14 + @"\141-SPC";
            if (!Directory.Exists(subModule141))
            {
                Directory.CreateDirectory(subModule141);
            }

            string subModule142 = subModule14 + @"\142-Labeling";
            if (!Directory.Exists(subModule142))
            {
                Directory.CreateDirectory(subModule142);
            }

            string subModule143 = subModule14 + @"\143-Leaflet";
            if (!Directory.Exists(subModule143))
            {
                Directory.CreateDirectory(subModule143);
            }

            string subModule144 = subModule14 + @"\144-Artwork";
            if (!Directory.Exists(subModule144))
            {
                Directory.CreateDirectory(subModule144);
            }

            string subModule145 = subModule14 + @"\145-Samples";
            if (!Directory.Exists(subModule145))
            {
                Directory.CreateDirectory(subModule145);
            }

            string subModule15 = Module1 + @"\15-Expert";
            if (!Directory.Exists(subModule15))
            {
                Directory.CreateDirectory(subModule15);
            }

            string subModule151 = subModule15 + @"\151-Quality";
            if (!Directory.Exists(subModule151))
            {
                Directory.CreateDirectory(subModule151);
            }

            string subModule152 = subModule15 + @"\152-Nonclinical";
            if (!Directory.Exists(subModule152))
            {
                Directory.CreateDirectory(subModule152);
            }

            string subModule153 = subModule15 + @"\153-Clinical";
            if (!Directory.Exists(subModule153))
            {
                Directory.CreateDirectory(subModule153);
            }

            string subModule16 = Module1 + @"\16-Environment Risk";
            if (!Directory.Exists(subModule16))
            {
                Directory.CreateDirectory(subModule16);
            }

            string subModule161 = subModule16 + @"\161-Non GMO";
            if (!Directory.Exists(subModule161))
            {
                Directory.CreateDirectory(subModule161);
            }

            string subModule17 = Module1 + @"\17-Pharmacovigilance";
            if (!Directory.Exists(subModule17))
            {
                Directory.CreateDirectory(subModule17);
            }

            string subModule171 = subModule17 + @"\171-Phvig-system";
            if (!Directory.Exists(subModule171))
            {
                Directory.CreateDirectory(subModule171);
            }

            string subModule172 = subModule17 + @"\172-Risk mgmt-system";
            if (!Directory.Exists(subModule172))
            {
                Directory.CreateDirectory(subModule172);
            }

            string subModule18 = Module1 + @"\18-Certificates";
            if (!Directory.Exists(subModule18))
            {
                Directory.CreateDirectory(subModule18);
            }

            string subModule181 = subModule18 + @"\181-GMP";
            if (!Directory.Exists(subModule181))
            {
                Directory.CreateDirectory(subModule181);
            }

            string subModule182 = subModule18 + @"\182-CPP";
            if (!Directory.Exists(subModule182))
            {
                Directory.CreateDirectory(subModule182);
            }

            string subModule183 = subModule18 + @"\183-Analysis Substance";
            if (!Directory.Exists(subModule183))
            {
                Directory.CreateDirectory(subModule183);
            }

            string subModule184 = subModule18 + @"\184-Analysis Excipients";
            if (!Directory.Exists(subModule184))
            {
                Directory.CreateDirectory(subModule184);
            }

            string subModule185 = subModule18 + @"\185-Alcohol Content";
            if (!Directory.Exists(subModule185))
            {
                Directory.CreateDirectory(subModule185);
            }

            string subModule186 = subModule18 + @"\186-Pork Content";
            if (!Directory.Exists(subModule186))
            {
                Directory.CreateDirectory(subModule186);
            }

            string subModule187 = subModule18 + @"\187-Certificate-TSE";
            if (!Directory.Exists(subModule187))
            {
                Directory.CreateDirectory(subModule187);
            }

            string subModule188 = subModule18 + @"\188-Diluent Coloring Agents";
            if (!Directory.Exists(subModule188))
            {
                Directory.CreateDirectory(subModule188);
            }

            string subModule189 = subModule18 + @"\189-Patent Information";
            if (!Directory.Exists(subModule189))
            {
                Directory.CreateDirectory(subModule189);
            }

            string subModule1810 = subModule18 + @"\1810-Letter Access- DMF";
            if (!Directory.Exists(subModule1810))
            {
                Directory.CreateDirectory(subModule1810);
            }

            string subModule19 = Module1 + @"\19-pricing";
            if (!Directory.Exists(subModule19))
            {
                Directory.CreateDirectory(subModule19);
            }

            string subModule191 = subModule19 + @"\191-price-list";
            if (!Directory.Exists(subModule191))
            {
                Directory.CreateDirectory(subModule191);
            }

            //Module 2

            string Module2 = ProjectFolder + @"\Module2";
            if (!Directory.Exists(Module2))
            {
                Directory.CreateDirectory(Module2);
            }

            string subModule22 = Module2 + @"\22-intro";
            if (!Directory.Exists(subModule22))
            {
                Directory.CreateDirectory(subModule22);
            }

            string subModule23 = Module2 + @"\23-qos";
            if (!Directory.Exists(subModule23))
            {
                Directory.CreateDirectory(subModule23);
            }

            string subModule24 = Module2 + @"\24-nonclin-over";
            if (!Directory.Exists(subModule24))
            {
                Directory.CreateDirectory(subModule24);
            }

            string subModule25 = Module2 + @"\25-clin-over";
            if (!Directory.Exists(subModule25))
            {
                Directory.CreateDirectory(subModule25);
            }

            string subModule26 = Module2 + @"\26-nonclin-sum";
            if (!Directory.Exists(subModule26))
            {
                Directory.CreateDirectory(subModule26);
            }

            string subModule27 = Module2 + @"\27-clin-sum";
            if (!Directory.Exists(subModule27))
            {
                Directory.CreateDirectory(subModule27);
            }

            //Module 3

            string Module3 = ProjectFolder + @"\Module3";
            if (!Directory.Exists(Module3))
            {
                Directory.CreateDirectory(Module3);
            }

            string subModule32 = Module3 + @"\32-body-data";
            if (!Directory.Exists(subModule32))
            {
                Directory.CreateDirectory(subModule32);
            }

            string subModule32a = subModule32 + @"\32a-app";
            if (!Directory.Exists(subModule32a))
            {
                Directory.CreateDirectory(subModule32a);
            }

            string subModule32a1 = subModule32a + @"\32a1-fac-equip";
            if (!Directory.Exists(subModule32a1))
            {
                Directory.CreateDirectory(subModule32a1);
            }

            string subModule32a2 = subModule32a + @"\32a2-advent-agent";
            if (!Directory.Exists(subModule32a2))
            {
                Directory.CreateDirectory(subModule32a2);
            }

            string subModule32a3 = subModule32a + @"\32a3-excip-name-1";
            if (!Directory.Exists(subModule32a3))
            {
                Directory.CreateDirectory(subModule32a3);
            }

            string subModule32p = subModule32 + @"\32p-drug-prod";
            if (!Directory.Exists(subModule32p))
            {
                Directory.CreateDirectory(subModule32p);
            }

            string product1 = subModule32p + @"\product-1";
            if (!Directory.Exists(product1))
            {
                Directory.CreateDirectory(product1);
            }

            string subModule32p1 = product1 + @"\32p1-desc-comp";
            if (!Directory.Exists(subModule32p1))
            {
                Directory.CreateDirectory(subModule32p1);
            }

            string subModule32p2 = product1 + @"\32p2-pharm-dev";
            if (!Directory.Exists(subModule32p2))
            {
                Directory.CreateDirectory(subModule32p2);
            }

            string subModule32p3 = product1 + @"\32p3-manuf";
            if (!Directory.Exists(subModule32p3))
            {
                Directory.CreateDirectory(subModule32p3);
            }

            string subModule32p4 = product1 + @"\32p4-contr-excip";
            if (!Directory.Exists(subModule32p4))
            {
                Directory.CreateDirectory(subModule32p4);
            }

            string excipient1 = subModule32p4 + @"\excipient 1";
            if (!Directory.Exists(excipient1))
            {
                Directory.CreateDirectory(excipient1);
            }

            string subModule32p5 = product1 + @"\32p5-contr-drug-prod";
            if (!Directory.Exists(subModule32p5))
            {
                Directory.CreateDirectory(subModule32p5);
            }

            string subModule32p51 = subModule32p5 + @"\32p51-spec";
            if (!Directory.Exists(subModule32p51))
            {
                Directory.CreateDirectory(subModule32p51);
            }

            string subModule32p52 = subModule32p5 + @"\32p52-analyt-proc";
            if (!Directory.Exists(subModule32p52))
            {
                Directory.CreateDirectory(subModule32p52);
            }

            string subModule32p53 = subModule32p5 + @"\32p53-val-analyt-proc";
            if (!Directory.Exists(subModule32p53))
            {
                Directory.CreateDirectory(subModule32p53);
            }

            string subModule32p54 = subModule32p5 + @"\32p54-batch-analys";
            if (!Directory.Exists(subModule32p54))
            {
                Directory.CreateDirectory(subModule32p54);
            }

            string subModule32p55 = subModule32p5 + @"\32p55-charac-imp";
            if (!Directory.Exists(subModule32p55))
            {
                Directory.CreateDirectory(subModule32p55);
            }

            string subModule32p56 = subModule32p5 + @"\32p56-justif-spec";
            if (!Directory.Exists(subModule32p56))
            {
                Directory.CreateDirectory(subModule32p56);
            }

            string subModule32p6 = product1 + @"\32p6-ref-stand";
            if (!Directory.Exists(subModule32p6))
            {
                Directory.CreateDirectory(subModule32p6);
            }
            string subModule32p7 = product1 + @"\32p7-cont-closure-sys";
            if (!Directory.Exists(subModule32p7))
            {
                Directory.CreateDirectory(subModule32p7);
            }
            string subModule32p8 = product1 + @"\32p8-stab";
            if (!Directory.Exists(subModule32p8))
            {
                Directory.CreateDirectory(subModule32p8);
            }

            string subModule32r = subModule32 + @"\32r-reg-info";
            if (!Directory.Exists(subModule32r))
            {
                Directory.CreateDirectory(subModule32r);
            }

            string subModule32s = subModule32 + @"\32s-drug-sub";
            if (!Directory.Exists(subModule32s))
            {
                Directory.CreateDirectory(subModule32s);
            }

            string substance1manufacturer1 = subModule32s + @"\substance-1-manufacturer-1";
            if (!Directory.Exists(substance1manufacturer1))
            {
                Directory.CreateDirectory(substance1manufacturer1);
            }

            string subModule32s1 = substance1manufacturer1 + @"\32s1-gen-info";
            if (!Directory.Exists(subModule32s1))
            {
                Directory.CreateDirectory(subModule32s1);
            }

            string subModule32s2 = substance1manufacturer1 + @"\32s2-manuf";
            if (!Directory.Exists(subModule32s2))
            {
                Directory.CreateDirectory(subModule32s2);
            }

            string subModule32s3 = substance1manufacturer1 + @"\32s3-charac";
            if (!Directory.Exists(subModule32s3))
            {
                Directory.CreateDirectory(subModule32s3);
            }

            string subModule32s4 = substance1manufacturer1 + @"\32s4-contr-drug-sub";
            if (!Directory.Exists(subModule32s4))
            {
                Directory.CreateDirectory(subModule32s4);
            }

            string subModule32s41 = subModule32s4 + @"\32s41-spec";
            if (!Directory.Exists(subModule32s41))
            {
                Directory.CreateDirectory(subModule32s41);
            }

            string subModule32s42 = subModule32s4 + @"\32s42- analyt-proc";
            if (!Directory.Exists(subModule32s42))
            {
                Directory.CreateDirectory(subModule32s42);
            }

            string subModule32s43 = subModule32s4 + @"\32s43-val-analyt-proc";
            if (!Directory.Exists(subModule32s43))
            {
                Directory.CreateDirectory(subModule32s43);
            }

            string subModule32s44 = subModule32s4 + @"\32s44-batch-analys";
            if (!Directory.Exists(subModule32s44))
            {
                Directory.CreateDirectory(subModule32s44);
            }

            string subModule32s45 = subModule32s4 + @"\32s45-justif-spec";
            if (!Directory.Exists(subModule32s45))
            {
                Directory.CreateDirectory(subModule32s45);
            }

            string subModule32s5 = substance1manufacturer1 + @"\32s5-ref-stand";
            if (!Directory.Exists(subModule32s5))
            {
                Directory.CreateDirectory(subModule32s5);
            }

            string subModule32s6 = substance1manufacturer1 + @"\32s6-cont-closure-sys";
            if (!Directory.Exists(subModule32s6))
            {
                Directory.CreateDirectory(subModule32s6);
            }

            string subModule32s7 = substance1manufacturer1 + @"\32s7-stab";
            if (!Directory.Exists(subModule32s7))
            {
                Directory.CreateDirectory(subModule32s7);
            }

            string subModule33 = Module3 + @"\33-lit-ref";
            if (!Directory.Exists(subModule33))
            {
                Directory.CreateDirectory(subModule33);
            }

            //Module 4

            string Module4 = ProjectFolder + @"\Module4";
            if (!Directory.Exists(Module4))
            {
                Directory.CreateDirectory(Module4);
            }

            string subModule42 = Module4 + @"\42-stud-rep";
            if (!Directory.Exists(subModule42))
            {
                Directory.CreateDirectory(subModule42);
            }

            string subModule421 = subModule42 + @"\421-pharmacol";
            if (!Directory.Exists(subModule421))
            {
                Directory.CreateDirectory(subModule421);
            }

            string subModule4211 = subModule421 + @"\4211-prim-pd";
            if (!Directory.Exists(subModule4211))
            {
                Directory.CreateDirectory(subModule4211);
            }

            string subModule4212 = subModule421 + @"\4212-sec-pd";
            if (!Directory.Exists(subModule4212))
            {
                Directory.CreateDirectory(subModule4212);
            }

            string subModule4213 = subModule421 + @"\4213-safety-pharmacol";
            if (!Directory.Exists(subModule4213))
            {
                Directory.CreateDirectory(subModule4213);
            }

            string subModule4214 = subModule421 + @"\4214-pd-drug-interact";
            if (!Directory.Exists(subModule4214))
            {
                Directory.CreateDirectory(subModule4214);
            }

            string subModule422 = subModule42 + @"\422-pk";
            if (!Directory.Exists(subModule422))
            {
                Directory.CreateDirectory(subModule422);
            }

            string subModule4221 = subModule422 + @"\4221-analyt-met-val";
            if (!Directory.Exists(subModule4221))
            {
                Directory.CreateDirectory(subModule4221);
            }

            string subModule4222 = subModule422 + @"\4222-absorp";
            if (!Directory.Exists(subModule4222))
            {
                Directory.CreateDirectory(subModule4222);
            }

            string subModule4223 = subModule422 + @"\4223-distrib";
            if (!Directory.Exists(subModule4223))
            {
                Directory.CreateDirectory(subModule4223);
            }

            string subModule4224 = subModule422 + @"\4224-metab";
            if (!Directory.Exists(subModule4224))
            {
                Directory.CreateDirectory(subModule4224);
            }

            string subModule4225 = subModule422 + @"\4225-excr";
            if (!Directory.Exists(subModule4225))
            {
                Directory.CreateDirectory(subModule4225);
            }

            string subModule4226 = subModule422 + @"\4226-pk-drug-interact";
            if (!Directory.Exists(subModule4226))
            {
                Directory.CreateDirectory(subModule4226);
            }

            string subModule4227 = subModule422 + @"\4227-other-pk-stud";
            if (!Directory.Exists(subModule4227))
            {
                Directory.CreateDirectory(subModule4227);
            }

            string subModule423 = subModule42 + @"\423-tox";
            if (!Directory.Exists(subModule423))
            {
                Directory.CreateDirectory(subModule423);
            }

            string subModule4231 = subModule423 + @"\4231-single-dose-tox";
            if (!Directory.Exists(subModule4231))
            {
                Directory.CreateDirectory(subModule4231);
            }

            string subModule4232 = subModule423 + @"\4232-repeat-dose-tox";
            if (!Directory.Exists(subModule4232))
            {
                Directory.CreateDirectory(subModule4232);
            }
            string subModule4233 = subModule423 + @"\4233-genotox";
            if (!Directory.Exists(subModule4233))
            {
                Directory.CreateDirectory(subModule4233);
            }
            string subModule42331 = subModule4233 + @"\42331-in-vitro";
            if (!Directory.Exists(subModule42331))
            {
                Directory.CreateDirectory(subModule42331);
            }
            string subModule42332 = subModule4233 + @"\42332-in-vivo";
            if (!Directory.Exists(subModule42331))
            {
                Directory.CreateDirectory(subModule42331);
            }

            string subModule4234 = subModule423 + @"\4234-carcigen";
            if (!Directory.Exists(subModule4234))
            {
                Directory.CreateDirectory(subModule4234);
            }

            string subModule42341 = subModule4234 + @"\42341-lt-stud";
            if (!Directory.Exists(subModule42341))
            {
                Directory.CreateDirectory(subModule42341);
            }

            string subModule42342 = subModule4234 + @"\42342-smt-stud";
            if (!Directory.Exists(subModule42342))
            {
                Directory.CreateDirectory(subModule42342);
            }

            string subModule42343 = subModule4234 + @"\42343-other-stud";
            if (!Directory.Exists(subModule42343))
            {
                Directory.CreateDirectory(subModule42343);
            }

            string subModule4235 = subModule423 + @"\4235-repro-dev-tox";
            if (!Directory.Exists(subModule4235))
            {
                Directory.CreateDirectory(subModule4235);
            }

            string subModule42351 = subModule4235 + @"\42351-fert-embryo-dev";
            if (!Directory.Exists(subModule42351))
            {
                Directory.CreateDirectory(subModule42351);
            }
            string subModule42352 = subModule4235 + @"\42352-embryo-fetal-dev";
            if (!Directory.Exists(subModule42352))
            {
                Directory.CreateDirectory(subModule42352);
            }
            string subModule42353 = subModule4235 + @"\42353-pre-postnatal-dev";
            if (!Directory.Exists(subModule42353))
            {
                Directory.CreateDirectory(subModule42353);
            }
            string subModule42354 = subModule4235 + @"\42354-juv";
            if (!Directory.Exists(subModule42354))
            {
                Directory.CreateDirectory(subModule42354);
            }

            string subModule4236 = subModule423 + @"\4236-loc-tol";
            if (!Directory.Exists(subModule4236))
            {
                Directory.CreateDirectory(subModule4236);
            }

            string subModule4237 = subModule423 + @"\4237-other-tox-stud";
            if (!Directory.Exists(subModule4237))
            {
                Directory.CreateDirectory(subModule4237);
            }

            string subModule42371 = subModule4237 + @"\42371-antigen";
            if (!Directory.Exists(subModule42371))
            {
                Directory.CreateDirectory(subModule42371);
            }
            string subModule42372 = subModule4237 + @"\42372-immunotox";
            if (!Directory.Exists(subModule42372))
            {
                Directory.CreateDirectory(subModule42372);
            }
            string subModule42373 = subModule4237 + @"\42373-mechan-stud";
            if (!Directory.Exists(subModule42373))
            {
                Directory.CreateDirectory(subModule42373);
            }
            string subModule42374 = subModule4237 + @"\42374-dep";
            if (!Directory.Exists(subModule42374))
            {
                Directory.CreateDirectory(subModule42374);
            }
            string subModule42375 = subModule4237 + @"\42375-metab";
            if (!Directory.Exists(subModule42375))
            {
                Directory.CreateDirectory(subModule42375);
            }
            string subModule42376 = subModule4237 + @"\42376-imp";
            if (!Directory.Exists(subModule42376))
            {
                Directory.CreateDirectory(subModule42376);
            }
            string subModule42377 = subModule4237 + @"\42377-other";
            if (!Directory.Exists(subModule42377))
            {
                Directory.CreateDirectory(subModule42377);
            }
            string subModule43 = Module4 + @"\43-lit-ref ";
            if (!Directory.Exists(subModule43))
            {
                Directory.CreateDirectory(subModule43);
            }

            //Module 5
            string Module5 = ProjectFolder + @"\Module5";
            if (!Directory.Exists(Module5))
            {
                Directory.CreateDirectory(Module5);
            }

            string subModule52 = Module5 + @"\52-tab-list";
            if (!Directory.Exists(subModule52))
            {
                Directory.CreateDirectory(subModule52);
            }

            string subModule53 = Module5 + @"\53-clin-stud-rep";
            if (!Directory.Exists(subModule53))
            {
                Directory.CreateDirectory(subModule53);
            }

            string subModule531 = subModule53 + @"\531-rep-biopharm-stud";
            if (!Directory.Exists(subModule531))
            {
                Directory.CreateDirectory(subModule531);
            }

            string subModule5311 = subModule531 + @"\5311-ba-stud-rep";
            if (!Directory.Exists(subModule5311))
            {
                Directory.CreateDirectory(subModule5311);
            }

            string subModule53111 = subModule5311 + @"\study-report-1";
            if (!Directory.Exists(subModule53111))
            {
                Directory.CreateDirectory(subModule53111);
            }

            string subModule53112 = subModule5311 + @"\study-report-2";
            if (!Directory.Exists(subModule53112))
            {
                Directory.CreateDirectory(subModule53112);
            }

            string subModule53113 = subModule5311 + @"\study-report-3";
            if (!Directory.Exists(subModule53113))
            {
                Directory.CreateDirectory(subModule53113);
            }

            string subModule5312 = subModule531 + @"\5312-compar-ba-be-stud-rep";
            if (!Directory.Exists(subModule5312))
            {
                Directory.CreateDirectory(subModule5312);
            }

            string subModule53121 = subModule5312 + @"\study-report-1";
            if (!Directory.Exists(subModule53121))
            {
                Directory.CreateDirectory(subModule53121);
            }

            string subModule53122 = subModule5312 + @"\study-report-2";
            if (!Directory.Exists(subModule53122))
            {
                Directory.CreateDirectory(subModule53122);
            }

            string subModule53123 = subModule5312 + @"\study-report-3";
            if (!Directory.Exists(subModule53123))
            {
                Directory.CreateDirectory(subModule53123);
            }

            string subModule5313 = subModule531 + @"\5313-in-vitro-in-vivo-corr-stud-rep";
            if (!Directory.Exists(subModule5313))
            {
                Directory.CreateDirectory(subModule5313);
            }

            string subModule53131 = subModule5313 + @"\study-report-1";
            if (!Directory.Exists(subModule53131))
            {
                Directory.CreateDirectory(subModule53131);
            }

            string subModule53132 = subModule5313 + @"\study-report-2";
            if (!Directory.Exists(subModule53132))
            {
                Directory.CreateDirectory(subModule53132);
            }

            string subModule53133 = subModule5313 + @"\study-report-3";
            if (!Directory.Exists(subModule53133))
            {
                Directory.CreateDirectory(subModule53133);
            }

            string subModule5314 = subModule531 + @"\5314-bioanalyt-analyt-met";
            if (!Directory.Exists(subModule5314))
            {
                Directory.CreateDirectory(subModule5314);
            }

            string subModule53141 = subModule5314 + @"\study-report-1";
            if (!Directory.Exists(subModule53141))
            {
                Directory.CreateDirectory(subModule53141);
            }

            string subModule53142 = subModule5314 + @"\study-report-2";
            if (!Directory.Exists(subModule53142))
            {
                Directory.CreateDirectory(subModule53142);
            }

            string subModule53143 = subModule5314 + @"\study-report-3";
            if (!Directory.Exists(subModule53143))
            {
                Directory.CreateDirectory(subModule53143);
            }

            string subModule532 = subModule53 + @"\532-rep-stud-pk-human-biomat";
            if (!Directory.Exists(subModule532))
            {
                Directory.CreateDirectory(subModule532);
            }

            string subModule5321 = subModule532 + @"\5321-plasma-prot-bind-stud-rep";
            if (!Directory.Exists(subModule5321))
            {
                Directory.CreateDirectory(subModule5321);
            }

            string subModule53211 = subModule5321 + @"\study-report-1";
            if (!Directory.Exists(subModule53211))
            {
                Directory.CreateDirectory(subModule53211);
            }

            string subModule53212 = subModule5321 + @"\study-report-2";
            if (!Directory.Exists(subModule53212))
            {
                Directory.CreateDirectory(subModule53212);
            }

            string subModule53213 = subModule5321 + @"\study-report-3";
            if (!Directory.Exists(subModule53213))
            {
                Directory.CreateDirectory(subModule53213);
            }

            string subModule5322 = subModule532 + @"\5322-rep-hep-metab-interact-stud";
            if (!Directory.Exists(subModule5322))
            {
                Directory.CreateDirectory(subModule5322);
            }

            string subModule53221 = subModule5322 + @"\study-report-1";
            if (!Directory.Exists(subModule53221))
            {
                Directory.CreateDirectory(subModule53221);
            }

            string subModule53222 = subModule5322 + @"\study-report-2";
            if (!Directory.Exists(subModule53222))
            {
                Directory.CreateDirectory(subModule53222);
            }

            string subModule53223 = subModule5322 + @"\study-report-3";
            if (!Directory.Exists(subModule53223))
            {
                Directory.CreateDirectory(subModule53223);
            }

            string subModule5323 = subModule532 + @"\5322-rep-hep-metab-interact-stud";
            if (!Directory.Exists(subModule5323))
            {
                Directory.CreateDirectory(subModule5323);
            }

            string subModule53231 = subModule5323 + @"\study-report-1";
            if (!Directory.Exists(subModule53231))
            {
                Directory.CreateDirectory(subModule53231);
            }

            string subModule53232 = subModule5323 + @"\study-report-2";
            if (!Directory.Exists(subModule53232))
            {
                Directory.CreateDirectory(subModule53232);
            }

            string subModule53233 = subModule5323 + @"\study-report-3";
            if (!Directory.Exists(subModule53233))
            {
                Directory.CreateDirectory(subModule53233);
            }

            string subModule533 = subModule53 + @"\533-rep-human-pk-stud";
            if (!Directory.Exists(subModule533))
            {
                Directory.CreateDirectory(subModule533);
            }

            string subModule5331 = subModule533 + @"\5331-healthy-subj-pk-init-tol-stud-rep";
            if (!Directory.Exists(subModule5331))
            {
                Directory.CreateDirectory(subModule5331);
            }

            string subModule53311 = subModule5331 + @"\study-report-1";
            if (!Directory.Exists(subModule53311))
            {
                Directory.CreateDirectory(subModule53311);
            }

            string subModule53312 = subModule5331 + @"\study-report-2";
            if (!Directory.Exists(subModule53312))
            {
                Directory.CreateDirectory(subModule53312);
            }

            string subModule53313 = subModule5331 + @"\study-report-3";
            if (!Directory.Exists(subModule53313))
            {
                Directory.CreateDirectory(subModule53313);
            }

            string subModule5332 = subModule533 + @"\5332-patient-pk-init-tol-stud-rep";
            if (!Directory.Exists(subModule5332))
            {
                Directory.CreateDirectory(subModule5332);
            }

            string subModule53321 = subModule5332 + @"\study-report-1";
            if (!Directory.Exists(subModule53321))
            {
                Directory.CreateDirectory(subModule53321);
            }

            string subModule53322 = subModule5332 + @"\study-report-2";
            if (!Directory.Exists(subModule53322))
            {
                Directory.CreateDirectory(subModule53322);
            }

            string subModule53323 = subModule5332 + @"\study-report-3";
            if (!Directory.Exists(subModule53323))
            {
                Directory.CreateDirectory(subModule53323);
            }

            string subModule5333 = subModule533 + @"\5333-intrin-factor-pk-stud-rep";
            if (!Directory.Exists(subModule5333))
            {
                Directory.CreateDirectory(subModule5333);
            }

            string subModule53331 = subModule5333 + @"\study-report-1";
            if (!Directory.Exists(subModule53331))
            {
                Directory.CreateDirectory(subModule53331);
            }
            string subModule53332 = subModule5333 + @"\study-report-2";
            if (!Directory.Exists(subModule53332))
            {
                Directory.CreateDirectory(subModule53332);
            }
            string subModule53333 = subModule5333 + @"\study-report-3";
            if (!Directory.Exists(subModule53333))
            {
                Directory.CreateDirectory(subModule53333);
            }
            string subModule5334 = subModule533 + @"\5334-extrin-factor-pk-stud-rep";
            if (!Directory.Exists(subModule5334))
            {
                Directory.CreateDirectory(subModule5334);
            }
            string subModule53341 = subModule5334 + @"\study-report-1";
            if (!Directory.Exists(subModule53341))
            {
                Directory.CreateDirectory(subModule53341);
            }
            string subModule53342 = subModule5334 + @"\study-report-2";
            if (!Directory.Exists(subModule53342))
            {
                Directory.CreateDirectory(subModule53342);
            }
            string subModule53343 = subModule5334 + @"\study-report-3";
            if (!Directory.Exists(subModule53343))
            {
                Directory.CreateDirectory(subModule53343);
            }
            string subModule5335 = subModule533 + @"\5335-popul-pk-stud-rep";
            if (!Directory.Exists(subModule5335))
            {
                Directory.CreateDirectory(subModule5335);
            }
            string subModule53351 = subModule5335 + @"\study-report-1";
            if (!Directory.Exists(subModule53351))
            {
                Directory.CreateDirectory(subModule53351);
            }
            string subModule53352 = subModule5335 + @"\study-report-2";
            if (!Directory.Exists(subModule53352))
            {
                Directory.CreateDirectory(subModule53352);
            }
            string subModule53353 = subModule5335 + @"\study-report-3";
            if (!Directory.Exists(subModule53353))
            {
                Directory.CreateDirectory(subModule53353);
            }

            string subModule534 = subModule53 + @"\534-rep-human-pd-stud";
            if (!Directory.Exists(subModule534))
            {
                Directory.CreateDirectory(subModule534);
            }

            string subModule5341 = subModule534 + @"\5341-healthy-subj-pd-stud-rep";
            if (!Directory.Exists(subModule5341))
            {
                Directory.CreateDirectory(subModule5341);
            }

            string subModule53411 = subModule5341 + @"\study-report-1";
            if (!Directory.Exists(subModule53411))
            {
                Directory.CreateDirectory(subModule53411);
            }

            string subModule53412 = subModule5341 + @"\study-report-2";
            if (!Directory.Exists(subModule53412))
            {
                Directory.CreateDirectory(subModule53412);
            }

            string subModule53413 = subModule5341 + @"\study-report-3";
            if (!Directory.Exists(subModule53413))
            {
                Directory.CreateDirectory(subModule53413);
            }

            string subModule5342 = subModule534 + @"\5342-patient-pd-stud-rep";
            if (!Directory.Exists(subModule5342))
            {
                Directory.CreateDirectory(subModule5342);
            }

            string subModule53421 = subModule5342 + @"\study-report-1";
            if (!Directory.Exists(subModule53421))
            {
                Directory.CreateDirectory(subModule53421);
            }

            string subModule53422 = subModule5342 + @"\study-report-2";
            if (!Directory.Exists(subModule53422))
            {
                Directory.CreateDirectory(subModule53422);
            }

            string subModule53423 = subModule5342 + @"\study-report-3";
            if (!Directory.Exists(subModule53423))
            {
                Directory.CreateDirectory(subModule53423);
            }

            string subModule535 = subModule53 + @"\535-rep-effic-safety-stud";
            if (!Directory.Exists(subModule535))
            {
                Directory.CreateDirectory(subModule535);
            }

            string subModule5351 = subModule535 + @"\5351-indication-1";
            if (!Directory.Exists(subModule5351))
            {
                Directory.CreateDirectory(subModule5351);
            }

            string subModule53511 = subModule5351 + @"\53511-stud-rep-contr";
            if (!Directory.Exists(subModule53511))
            {
                Directory.CreateDirectory(subModule53511);
            }

            string subModule535111 = subModule53511 + @"\study-report-1";
            if (!Directory.Exists(subModule535111))
            {
                Directory.CreateDirectory(subModule535111);
            }

            string subModule535112 = subModule53511 + @"\study-report-2";
            if (!Directory.Exists(subModule535112))
            {
                Directory.CreateDirectory(subModule535112);
            }

            string subModule535113 = subModule53511 + @"\study-report-3";
            if (!Directory.Exists(subModule535113))
            {
                Directory.CreateDirectory(subModule535113);
            }

            string subModule53512 = subModule5351 + @"\53512-stud-rep-uncontr";
            if (!Directory.Exists(subModule53512))
            {
                Directory.CreateDirectory(subModule53512);
            }

            string subModule535121 = subModule53512 + @"\study-report-1";
            if (!Directory.Exists(subModule535121))
            {
                Directory.CreateDirectory(subModule535121);
            }

            string subModule535122 = subModule53512 + @"\study-report-2";
            if (!Directory.Exists(subModule535122))
            {
                Directory.CreateDirectory(subModule535122);
            }

            string subModule535123 = subModule53512 + @"\study-report-3";
            if (!Directory.Exists(subModule535123))
            {
                Directory.CreateDirectory(subModule535123);
            }

            string subModule53513 = subModule5351 + @"\53513-rep-analys-data-more-one-stud";
            if (!Directory.Exists(subModule53513))
            {
                Directory.CreateDirectory(subModule53513);
            }

            string subModule535131 = subModule53513 + @"\study-report-1";
            if (!Directory.Exists(subModule535131))
            {
                Directory.CreateDirectory(subModule535131);
            }

            string subModule535132 = subModule53513 + @"\study-report-2";
            if (!Directory.Exists(subModule535132))
            {
                Directory.CreateDirectory(subModule535132);
            }

            string subModule535133 = subModule53513 + @"\study-report-3";
            if (!Directory.Exists(subModule535133))
            {
                Directory.CreateDirectory(subModule535133);
            }

            string subModule53514 = subModule5351 + @"\53514-other-stud-rep";
            if (!Directory.Exists(subModule53514))
            {
                Directory.CreateDirectory(subModule53514);
            }

            string subModule535141 = subModule53514 + @"\study-report-1";
            if (!Directory.Exists(subModule535141))
            {
                Directory.CreateDirectory(subModule535141);
            }

            string subModule535142 = subModule53514 + @"\study-report-2";
            if (!Directory.Exists(subModule535142))
            {
                Directory.CreateDirectory(subModule535142);
            }

            string subModule535143 = subModule53514 + @"\study-report-3";
            if (!Directory.Exists(subModule535143))
            {
                Directory.CreateDirectory(subModule535143);
            }

            string subModule536 = subModule53 + @"\536-postmark-exp";
            if (!Directory.Exists(subModule536))
            {
                Directory.CreateDirectory(subModule536);
            }

            string subModule537 = subModule53 + @"\537-crf-ipl";
            if (!Directory.Exists(subModule537))
            {
                Directory.CreateDirectory(subModule537);
            }

            string subModule5371 = subModule537 + @"\study-report-1";
            if (!Directory.Exists(subModule5371))
            {
                Directory.CreateDirectory(subModule5371);
            }

            string subModule5372 = subModule537 + @"\study-report-2";
            if (!Directory.Exists(subModule5372))
            {
                Directory.CreateDirectory(subModule5372);
            }

            string subModule5373 = subModule537 + @"\study-report-3";
            if (!Directory.Exists(subModule5373))
            {
                Directory.CreateDirectory(subModule5373);
            }

            string subModule54 = Module5 + @"\54-lit-ref";
            if (!Directory.Exists(subModule54))
            {
                Directory.CreateDirectory(subModule54);
            }

        }


        public JsonResult GetDRF()
        {
            //List<DRFViewModel> DRFList = new List<DRFViewModel>();
            //ServiceResponseList<DRFDetails> allDRF = new ServiceResponseList<DRFDetails>();

            //allDRF = _DRF.GetAllDRF();

            //foreach (var item in allDRF.Results)
            //{
            //    DRFViewModel model = new DRFViewModel();
            //    model.Id = item.Id;
            //    model.ProductId = item.ProductId;
            //    model.ProductName = item.ProductName;
            //    model.APIName = item.APIName;
            //    model.APIVendor = item.APIVendor;
            //    model.APIVolume = item.APIVolume;
            //    model.RegistrationDate = item.RegistrationDate.ToString().Split(' ')[0];
            //    model.ReRegistrationDate = item.ReRegistrationDate.ToString().Split(' ')[0];
            //    model.RegisterPlant = item.RegisterPlant;
            //    DRFList.Add(model);
            //}

            var DRFList = (from drf in _db.DRFDetails
                           join c in _db.Tbl_DSR_NewProductContinentMapping on drf.Id equals c.NewProductID
                           join cc in _db.Master_Continent on c.ContinentID equals cc.Id
                           join ccc in _db.Master_Country on c.CountryID equals ccc.Id
                           join f in _db.Tbl_Master_Formulation on drf.FormulationID equals f.Id
                           join tc in _db.Tbl_Master_TherapeuticCategory on drf.TherapeuticCategoryID equals tc.Id
                           //join pm in _db.Tbl_Master_ProductManufacture on drf.ProductManufactureID equals pm.Id
                           from pm in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == drf.ProductManufactureID).DefaultIfEmpty()
                           join dt in _db.Tbl_Master_DossierTemplate on drf.DossierTemplateID equals dt.Id
                           join dc in _db.Tbl_Master_DrugCategory on drf.DrugCategoryID equals dc.Id
                           join pd in _db.PrescriberDetails on drf.ProjectManagerID equals pd.AspNetUserId
                           join ps in _db.Tbl_Master_PIDFStatus on drf.ProjectStatusID equals ps.PidfStatusID
                           select new
                           {
                               Id = drf.Id,
                               ProductId = drf.ProductId,
                               ProductName = drf.ProductName,
                               Strength = drf.Strength,
                               ModuleName = drf.ModuleName,
                               Formulation = f.Formulation,
                               Continent = cc.Continent,
                               Country = ccc.Country,
                               RegistrationDate = drf.RegistrationDate.Value.ToString("dd/MM/yyyy"),
                               ReRegistrationDate = drf.ReRegistrationDate.Value.ToString("dd/MM/yyyy"),
                               DossierFillingDate = drf.CreatedDate.Value.ToString("dd/MM/yyyy"),
                               RegisterPlant = drf.RegisterPlant,
                               ManufacturingPlant = pm.ProductManufacture,
                               ProjectManager = pd.FirstName + " " + pd.LastName,
                               ProjectStatus = ps.PidfStatus
                           }).ToList();


            return Json(new { data = DRFList }, new JsonSerializerSettings());
        }

        public JsonResult GetRegistrationFees()
        {
            var RFLists = _db.Tbl_Master_RegistrationFees
                .AsNoTracking()
                .Where(x => x.IsActive == true)
               .Select(x => new { x.Id, x.Name, Percentage = 0 });


            return Json(new { data = RFLists }, new JsonSerializerSettings());
        }

        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("AttachedPIDFDetails")]
        public ActionResult AttachedPIDFDetails(GetDRFAttachedPIDF getDRFAttachedPIDF)
        {
            IList<PIDFDetailsNew> AttachedPIDFList = new List<PIDFDetailsNew>();
            AttachedPIDFList = _DRF.GetAttachedPIDFList(getDRFAttachedPIDF.DRFID, getDRFAttachedPIDF.CountryID);
            return Json(new { data = AttachedPIDFList });
        }

        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpGet]
        [ActionName("GetDRFTaskSubTask")]
        public ActionResult GetDRFTaskSubTask(string DRFID)
        {
            //IList<DRFTaskSubTaskListOutput> result = new List<DRFTaskSubTaskListOutput>();
            var result = _DRF.GetDRFTAskSubTaskList(DRFID);
            return Json(result);
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("GetDRFTaskSubTaskDetailsForEdit")]
        public ActionResult GetDRFTaskSubTaskDetailsForEdit(DRFTaskSubTaskDetailsForEditDelete dRFTaskSubTaskDetailsForEditDelete)
        {
            var List = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                        join PD in _db.PrescriberDetails on TMPM.EmpID equals PD.AspNetUserId
                        where TMPM.ProjectTaskMappingID == dRFTaskSubTaskDetailsForEditDelete.ProjectMappingID
                        select new
                        {
                            TMPM.ProjectTaskMappingID,
                            TMPM.ParentID,
                            TMPM.TaskName,
                            TMPM.EmpID,
                            EmpName = PD.FirstName + " " + PD.LastName,
                            TMPM.StartDate,
                            TMPM.EndDate,
                            TMPM.TaskDuration,
                            TMPM.TotalPercentage,
                            TMPM.ModifiedDate,
                            TMPM.TaskStatusID,
                            TMPM.TaskStatus,
                            TMPM.PriorityID,
                            TMPM.Priority

                        }).FirstOrDefault();


            DRFTaskSubTaskEditModel dRFTaskSubTaskEditModel = new DRFTaskSubTaskEditModel();

            dRFTaskSubTaskEditModel.DRFEditProjectTaskMappingID = Convert.ToInt64(List.ProjectTaskMappingID);
            dRFTaskSubTaskEditModel.DRFEditParentID = Convert.ToInt32(List.ParentID);
            dRFTaskSubTaskEditModel.DRFEditTaskName = List.TaskName;
            dRFTaskSubTaskEditModel.DRFEditEmpID = Convert.ToInt32(List.EmpID);
            dRFTaskSubTaskEditModel.DRFEditStartDate = Convert.ToDateTime(List.StartDate.ToString().Split('T')[0]);
            dRFTaskSubTaskEditModel.DRFEditEndDate = Convert.ToDateTime(List.EndDate.ToString().Split('T')[0]);
            dRFTaskSubTaskEditModel.DRFEditTaskDuration = Convert.ToInt32(List.TaskDuration);
            dRFTaskSubTaskEditModel.DRFEditTotalPercentage = Convert.ToDecimal(List.TotalPercentage)*100;
            // dRFTaskSubTaskEditModel.DRFEditModifiedDate = Convert.ToDateTime(List.ModifiedDate);
            dRFTaskSubTaskEditModel.DRFEditTaskStatusID = Convert.ToInt32(List.TaskStatusID);
            dRFTaskSubTaskEditModel.DRFEditPriorityID = Convert.ToInt32(List.PriorityID);

            List<TaskOwner> taskOwner = new List<TaskOwner>();

            var taskOwnersList = (from PD in _db.PrescriberDetails
                                  join asp in _db.AspNetUsers on PD.AspNetUserId equals asp.UserId
                                  where asp.IsEnabled == true
                                  select new
                                  {
                                      EmpID = PD.AspNetUserId,
                                      EmpName = PD.FirstName + " " + PD.LastName
                                  }).ToList();


            foreach (var ddata in taskOwnersList)
            {
                TaskOwner temp = new TaskOwner();
                temp.EmpID = ddata.EmpID;
                temp.EmpName = ddata.EmpName;
                taskOwner.Add(temp);
            }

            dRFTaskSubTaskEditModel.TaskOwner = taskOwner;

            List<ProjectStatus> projectStatuses = new List<ProjectStatus>();
            // List<int> notIDS = new List<int> { 2, 3, 4, 5, 6, 7, 9, 10 };
            List<int> IDS = new List<int> { 1, 8, 11 };
            var projectStatusList = (from TMPS in _db.Tbl_Master_PIDFStatus
                                     where TMPS.IsActive == true && IDS.Contains(TMPS.PidfStatusID)//!notIDS.Contains(TMPS.PidfStatusID)
                                     select new
                                     {
                                         ProjectStatusID = TMPS.PidfStatusID,
                                         ProjectStatus = TMPS.PidfStatus
                                     }).ToList();

            foreach (var ddata in projectStatusList)
            {
                ProjectStatus temp = new ProjectStatus();
                temp.ProjectStatusID = ddata.ProjectStatusID;
                temp.Status = ddata.ProjectStatus;
                projectStatuses.Add(temp);
            }

            dRFTaskSubTaskEditModel.ProjectStatus = projectStatuses;

            List<Priority> priority = new List<Priority>();


            var priorityList = (from TMP in _db.Tbl_Master_Priority
                                select new
                                {
                                    PriorityID = TMP.PriorityID,
                                    PriorityName = TMP.PriorityName
                                }).ToList();

            foreach (var ddata in priorityList)
            {
                Priority temp = new Priority();
                temp.PriorityID = ddata.PriorityID;
                temp.PriorityName = ddata.PriorityName;
                priority.Add(temp);
            }

            dRFTaskSubTaskEditModel.Priority = priority;


            //ViewBag.DRFEditProjectTaskMappingID = List.ProjectTaskMappingID;
            //ViewBag.DRFEditParentID = List.ParentID;
            //ViewBag.DRFEditTaskName = List.TaskName;
            //ViewBag.DRFEditEmpID = List.EmpID;
            //ViewBag.DRFEditStartDate = List.StartDate;
            //ViewBag.DRFEditEndDate = List.EndDate;
            //ViewBag.DRFEditTaskDuration = List.TaskDuration;
            //ViewBag.DRFEditTotalPercentage = List.TotalPercentage;
            //ViewBag.DRFEditModifiedDate = List.ModifiedDate;
            //ViewBag.DRFEditTaskStatusID = List.TaskStatusID;
            //ViewBag.DRFEditPriorityID = List.PriorityID;

            return Json(new { success = true, message = _sharedLocalizer["Details for edit retrive successfully"].Value, data = dRFTaskSubTaskEditModel });
            //return View();
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("UpdateDRFTaskSubTaskDetails")]
        [Obsolete]
        public async Task<ActionResult> UpdateDRFTaskSubTaskDetails(DRFTaskSubTaskEditModel dRFTaskSubTaskEditModel)
        {
            string userName = HttpContext.Session.GetString("CurrentUserName") + " has updated the following task: ";
            var drfresult = (from t in _db.Tbl_Master_ProjectTask_Mapping
                             where t.ProjectTaskMappingID == dRFTaskSubTaskEditModel.DRFEditProjectTaskMappingID
                             select new { t.Drfid }).FirstOrDefault();
            //Add query for pidf using session
            string strAction = HttpContext.Session.GetString("Action");
            string strProjectName = "";
            if(strAction == "PIDF")
            {
                var result1 = (from TDI in _db.Tbl_PIDF_HeaderNew
                               //join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               //join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.PidfID == drfresult.Drfid
                               select new { TDI.ProjectorProductName }).FirstOrDefault();
                strProjectName = result1.ProjectorProductName;
            }
            else
            {
                var result1 = (from TDI in _db.Tbl_DRF_Initialization
                               join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                               join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                               where TDI.InitializationID == drfresult.Drfid
                               select new { TDR.ProjectName }).FirstOrDefault();
                strProjectName = result1.ProjectName;
            }

            //string userMessage = "Project Name : " + result1.ProjectName + "</br>" +   "Task Name : " + dRFTaskSubTaskEditModel.DRFEditTaskName;
            string userMessage = "Project Name : " + strProjectName + "</br>" + "Task Name : " + dRFTaskSubTaskEditModel.DRFEditTaskName;
            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";

            //string strEmailMessage = userName  + "</br>" + "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + dRFTaskSubTaskEditModel.DRFEditTaskName;
            string strEmailMessage = userName + "</br>" + "Project Name : " + strProjectName + "</br>" + "Task Name : " + dRFTaskSubTaskEditModel.DRFEditTaskName;
            if (ModelState.IsValid)
            {
                Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = new Tbl_Master_ProjectTask_Mapping();
                tbl_Master_ProjectTask_Mapping.ProjectTaskMappingID = dRFTaskSubTaskEditModel.DRFEditProjectTaskMappingID;
                tbl_Master_ProjectTask_Mapping.TaskName = dRFTaskSubTaskEditModel.DRFEditTaskName;
                tbl_Master_ProjectTask_Mapping.EmpID = dRFTaskSubTaskEditModel.DRFEditEmpID;
                tbl_Master_ProjectTask_Mapping.StartDate = dRFTaskSubTaskEditModel.DRFEditStartDate;
                tbl_Master_ProjectTask_Mapping.EndDate = dRFTaskSubTaskEditModel.DRFEditEndDate;
                tbl_Master_ProjectTask_Mapping.TaskDuration = dRFTaskSubTaskEditModel.DRFEditTaskDuration;
                tbl_Master_ProjectTask_Mapping.TotalPercentage = dRFTaskSubTaskEditModel.DRFEditTotalPercentage;
                tbl_Master_ProjectTask_Mapping.PriorityID = dRFTaskSubTaskEditModel.DRFEditPriorityID;
                tbl_Master_ProjectTask_Mapping.TaskStatusID = dRFTaskSubTaskEditModel.DRFEditTaskStatusID;
                tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Today;

                int data = _DRF.UpdateDRFTaskSubTaskDetails(tbl_Master_ProjectTask_Mapping);

                ModelState.Clear();
                await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);

                //send email notification code added by yogesh balapure on date 08/02/2020
                //get smtp details 
                
                SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                string tempNotificationName="";
                if(strAction == "PIDF")
                {
                    tempNotificationName = "Pidf Task Update";
                }
                else
                {
                    tempNotificationName = "Task Updated";
                }
                //EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Task Updated");
                EmailDetailsModel emailDetailsModel = _emailService.EmailDetails(tempNotificationName);
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
                            if (!string.IsNullOrEmpty(emailDetailsModel.BCCList))
                            {
                                testBCC.Add(emailDetailsModel.BCCList);
                            }
                        }
                    }
                        

                    if(testCC.Count>0)
                    {
                        emailDetailsVM.CCMail = testCC.ToList();
                    }
                    if(testBCC.Count>0)
                    {
                        emailDetailsVM.BCCMail = testBCC.ToList();
                    }
                    
                    emailDetailsVM.Subject = emailDetailsModel.MailSubject;
                    clsTemplate _clsTemplate = new clsTemplate(_config, _env);

                    //emailDetailsVM.Body = emailDetailsModel.MailBody;
                    string tempurl = _config.GetSection("ApplicationURL:DrfUrlLink").Value + drfresult.Drfid;
                    emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                    emailDetailsVM.DispalyName = "";
                }

                if (sMTPDetailsModel != null && emailDetailsModel != null)
                {
                    EmailHelper emailHelper = new EmailHelper();
                    if (Convert.ToBoolean(_config.GetSection("MailSend:IsTaskUpdate").Value) == true)
                    {
                        var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                    }                        
                }
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("DeleteDRFTaskSubTaskDetails")]
        public ActionResult DeleteDRFTaskSubTaskDetails(DRFTaskSubTaskDetailsForEditDelete dRFTaskSubTaskDetailsForEditDelete)
        {
            Tbl_Master_ProjectTask_Mapping tbl_Master_ProjectTask_Mapping = new Tbl_Master_ProjectTask_Mapping();

            tbl_Master_ProjectTask_Mapping.ProjectTaskMappingID = dRFTaskSubTaskDetailsForEditDelete.ProjectMappingID;
            tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Today;
            int temp = _DRF.DeleteDRFTaskSubTaskDetails(tbl_Master_ProjectTask_Mapping);


            return Json(new { success = true, message = _sharedLocalizer["Step deleted successfully"].Value, data = "success" });
            //return View();
        }

       // [Authorize(Roles = "Prescriber")]
       [Authorize]
        [HttpPost]
        [ActionName("GetDropdownsForAddDRFTask")]
        public ActionResult GetDropdownsForAddDRFTask()
        {



            DRFTaskAddModel dRFTaskAddModel = new DRFTaskAddModel();


            List<TaskOwner> taskOwner = new List<TaskOwner>();

            var taskOwnersList = (from PD in _db.PrescriberDetails
                                  join asp in _db.AspNetUsers on PD.AspNetUserId equals asp.UserId
                                  where asp.IsEnabled == true
                                  select new
                                  {
                                      EmpID = PD.AspNetUserId,
                                      EmpName = PD.FirstName + " " + PD.LastName
                                  }).ToList();


            foreach (var ddata in taskOwnersList)
            {
                TaskOwner temp = new TaskOwner();
                temp.EmpID = ddata.EmpID;
                temp.EmpName = ddata.EmpName;
                taskOwner.Add(temp);
            }

            dRFTaskAddModel.TaskOwner = taskOwner;

            List<ProjectStatus> projectStatuses = new List<ProjectStatus>();
            //List<int> notIDS = new List<int> { 2, 3, 4, 5, 6, 7, 9, 10,12};//status ID array 
            List<int> IDS =new List<int> { 1, 8, 11 };
            var projectStatusList = (from TMPS in _db.Tbl_Master_PIDFStatus
                                     where TMPS.IsActive == true && IDS.Contains(TMPS.PidfStatusID)//!notIDS.Contains(TMPS.PidfStatusID)
                                     select new
                                     {
                                         ProjectStatusID = TMPS.PidfStatusID,
                                         ProjectStatus = TMPS.PidfStatus
                                     }).ToList();

            foreach (var ddata in projectStatusList)
            {
                ProjectStatus temp = new ProjectStatus();
                temp.ProjectStatusID = ddata.ProjectStatusID;
                temp.Status = ddata.ProjectStatus;
                projectStatuses.Add(temp);
            }

            dRFTaskAddModel.ProjectStatus = projectStatuses;

            List<Priority> priority = new List<Priority>();


            var priorityList = (from TMP in _db.Tbl_Master_Priority
                                select new
                                {
                                    PriorityID = TMP.PriorityID,
                                    PriorityName = TMP.PriorityName
                                }).ToList();

            foreach (var ddata in priorityList)
            {
                Priority temp = new Priority();
                temp.PriorityID = ddata.PriorityID;
                temp.PriorityName = ddata.PriorityName;
                priority.Add(temp);
            }

            dRFTaskAddModel.Priority = priority;


            return Json(new { success = true, message = _sharedLocalizer["Dropdown retrive successfully"].Value, data = dRFTaskAddModel });
            //return View();
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("AddDRFTaskDetails")]
        [Obsolete]
        public async Task<ActionResult> AddDRFTaskDetails(DRFTaskAddModel dRFTaskAddModel)
        {
            string userName = HttpContext.Session.GetString("CurrentUserName") + " has created the following task: ";
            
            var taskName = dRFTaskAddModel.DRFAddTaskName.Trim().ToUpper();
            var chkduplicate = (from x in _db.Tbl_Master_ProjectTask_Mapping where x.TaskName.ToUpper() == taskName && x.Drfid== dRFTaskAddModel.DRFAddTaskDRFID select x).ToList();
            var duplicateStep="";
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
                    tbl_Master_ProjectTask_Mapping.CreatedBy= Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.CreatedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProjectTask_Mapping.ModifiedDate = DateTime.Today;
                    tbl_Master_ProjectTask_Mapping.Action = "DRF";

                    int data = _DRF.insertDRFTaskDetails(tbl_Master_ProjectTask_Mapping);

                    //get last identity and fetch project name

                    var result1 = (from TDI in _db.Tbl_DRF_Initialization
                                   join TDM in _db.Tbl_DRFDataMaster on TDI.InitializationID equals TDM.InitializationId
                                   join TDR in _db.Tbl_DRF_IP_Details on TDM.IPDetailsId equals TDR.Id
                                   where TDI.InitializationID == dRFTaskAddModel.DRFAddTaskDRFID
                                   select new { TDR.ProjectName }).FirstOrDefault();

                    string userMessage = "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + dRFTaskAddModel.DRFAddTaskName;
                    string messageTime = Convert.ToString(DateTime.Now.Second) + "seconds ago.";

                    string strEmailMessage = userName  + "</br>" + "Project Name : " + result1.ProjectName + "</br>" + "Task Name : " + dRFTaskAddModel.DRFAddTaskName;

                    await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                    ModelState.Clear();

                    //send email notification code added by yogesh balapure on date 08/02/2020
                    //get smtp details 
                    SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                    EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Task Create");
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
                        string tempurl = _config.GetSection("ApplicationURL:ApprovedPidfUrlLink").Value + dRFTaskAddModel.DRFAddTaskDRFID;
                        emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage,tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
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


        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("GetDropdownsForAddSubTask")]
        public ActionResult GetDropdownsForAddSubTask(int Id)
        {

            DRFSubTaskAddModel dRFSubTaskAddModel = new DRFSubTaskAddModel();


            List<TaskOwner> taskOwner = new List<TaskOwner>();

            var taskOwnersList = (from PD in _db.PrescriberDetails
                                  join asp in _db.AspNetUsers on PD.AspNetUserId equals asp.UserId
                                  where asp.IsEnabled == true
                                  select new
                                  {
                                      EmpID = PD.AspNetUserId,
                                      EmpName = PD.FirstName + " " + PD.LastName
                                  }).ToList();


            foreach (var ddata in taskOwnersList)
            {
                TaskOwner temp = new TaskOwner();
                temp.EmpID = ddata.EmpID;
                temp.EmpName = ddata.EmpName;
                taskOwner.Add(temp);
            }

            dRFSubTaskAddModel.TaskOwner = taskOwner;

            List<ProjectStatus> projectStatuses = new List<ProjectStatus>();
            //List<int> notIDS = new List<int> { 2, 3, 4, 5, 6, 7, 9, 10 };
            List<int> IDS = new List<int> { 1, 8, 11 };
            var projectStatusList = (from TMPS in _db.Tbl_Master_PIDFStatus
                                     where TMPS.IsActive == true && IDS.Contains(TMPS.PidfStatusID)//!notIDS.Contains(TMPS.PidfStatusID)
                                     select new
                                     {
                                         ProjectStatusID = TMPS.PidfStatusID,
                                         ProjectStatus = TMPS.PidfStatus
                                     }).ToList();

            foreach (var ddata in projectStatusList)
            {
                ProjectStatus temp = new ProjectStatus();
                temp.ProjectStatusID = ddata.ProjectStatusID;
                temp.Status = ddata.ProjectStatus;
                projectStatuses.Add(temp);
            }

            dRFSubTaskAddModel.ProjectStatus = projectStatuses;

            List<Priority> priority = new List<Priority>();


            var priorityList = (from TMP in _db.Tbl_Master_Priority
                                select new
                                {
                                    PriorityID = TMP.PriorityID,
                                    PriorityName = TMP.PriorityName
                                }).ToList();

            foreach (var ddata in priorityList)
            {
                Priority temp = new Priority();
                temp.PriorityID = ddata.PriorityID;
                temp.PriorityName = ddata.PriorityName;
                priority.Add(temp);
            }

            dRFSubTaskAddModel.Priority = priority;

            List<MainTaskList> mainTaskLists = new List<MainTaskList>();


            var list = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                        where TMPM.ParentID==0 && TMPM.Drfid==Id
                                select new
                                {
                                    MainTaskID = TMPM.ProjectTaskMappingID,
                                    MainTaskName = TMPM.TaskName
                                }).ToList();

            foreach (var ddata in list)
            {
                MainTaskList temp = new MainTaskList();
                temp.MainTaskID = Convert.ToInt32(ddata.MainTaskID);
                temp.MainTaskName = ddata.MainTaskName;
                mainTaskLists.Add(temp);
            }

            dRFSubTaskAddModel.MainTaskList = mainTaskLists;



            return Json(new { success = true, message = _sharedLocalizer["Dropdown retrive successfully"].Value, data = dRFSubTaskAddModel });
            //return View();
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("AddDRFSubTaskDetails")]
        public ActionResult AddDRFSubTaskDetails(DRFSubTaskAddModel dRFSubTaskAddModel)
        {
            var subTaskName = dRFSubTaskAddModel.DRFAddSubTaskName.Trim().ToUpper();
            var chkduplicate = (from x in _db.Tbl_Master_ProjectTask_Mapping where x.TaskName.ToUpper() == subTaskName && x.Drfid == dRFSubTaskAddModel.DRFAddSubTaskDRFID && x.ParentID!= dRFSubTaskAddModel.DRFAddSubTaskTaskID select x).ToList();
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
                    tbl_Master_ProjectTask_Mapping.Action = "DRF";

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

        public void GetDropdownList()
        {
            List<SelectListItem> TherapeuticCategoryLists = new List<SelectListItem>();
            TherapeuticCategoryLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var TCLists = _db.Tbl_Master_TherapeuticCategory
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                 .OrderBy(x=>x.TherapeuticCategory)
                .Select(x => new { x.Id, x.TherapeuticCategory });
            foreach (var item in TCLists)
            {
                TherapeuticCategoryLists.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.TherapeuticCategory });
            }
            ViewBag.TherapeuticCategoryList = TherapeuticCategoryLists;

            List<SelectListItem> ProductManufactureLists = new List<SelectListItem>();
            ProductManufactureLists.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var PMLists = _db.Tbl_Master_ProductManufacture
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                 .OrderBy(x => x.ProductManufacture)
                .Select(x => new { x.Id, x.ProductManufacture });
            foreach (var item in PMLists)
            {
                ProductManufactureLists.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.ProductManufacture });
            }
            ViewBag.ProductManufactureList = ProductManufactureLists;

            List<SelectListItem> FormulationList = new List<SelectListItem>();
            FormulationList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var FLists = _db.Tbl_Master_Formulation
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                 .OrderBy(x => x.Formulation)
                .Select(x => new { x.Id, x.Formulation });
            foreach (var item in FLists)
            {
                FormulationList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Formulation });
            }
            ViewBag.FormulationList = FormulationList;

            List<SelectListItem> DossierTemplateList = new List<SelectListItem>();
            DossierTemplateList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var DTLists = _db.Tbl_Master_DossierTemplate
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                  .OrderBy(x => x.DossierTemplate)
                .Select(x => new { x.Id, x.DossierTemplate });
            foreach (var item in DTLists)
            {
                DossierTemplateList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.DossierTemplate });
            }
            ViewBag.DossierTemplateList = DossierTemplateList;

            List<SelectListItem> DrugCategoryList = new List<SelectListItem>();
            DrugCategoryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var DCLists = _db.Tbl_Master_DrugCategory
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                  .OrderBy(x => x.DrugCategory)
                .Select(x => new { x.Id, x.DrugCategory });
            foreach (var item in DCLists)
            {
                DrugCategoryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.DrugCategory });
            }
            ViewBag.DrugCategoryList = DrugCategoryList;

            List<SelectListItem> ContinentList = new List<SelectListItem>();
            ContinentList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            var COLists = _db.Master_Continent
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                  .OrderBy(x => x.Continent)
                .Select(x => new { x.Id, x.Continent });
            foreach (var item in COLists)
            {
                ContinentList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Continent });
            }
            ViewBag.ContinentList = ContinentList;

            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select option"].Value, Selected = true });
            ViewBag.CountryList = CountryList;

        }


        public IActionResult DRFFileManager()
        {
            var ProductID = HttpContext.Session.GetString("ProductID");
            ViewBag.filemid = ProductID.Replace(" ","");
            ViewBag.DRFProductID = HttpContext.Session.GetString("ProductID");
            return View();
        }

    }
}