using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Models;
using EmcureCERI.Web.Models.FileUpload;
using EmcureCERI.Web.Models.PrescriberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PrescriberController : Controller
    {
        private readonly IFileProvider fileProvider; 
        private readonly IPatientService _patient;
        private readonly IPrescriberService _prescriber;
        private readonly EmcureCERIDBContext _db;
        private readonly IBaselineDataMaster _baselinedata;
        private readonly IFollowUpFormMaster _followupform;
        private readonly IQuestionnaireService _questionnaireService;
        private readonly IFufquestionnaireService _fufquestionnaireService;
        private readonly ICountryService _country;
        private readonly IGlobalService _global;
        private readonly IAdverseEventService _adverseEvent;
        private readonly IMessageService _messageService;
        private readonly IHostingEnvironment _environment;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMemoryCache _cache;
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;

        public PrescriberController(IGlobalService global, IConfiguration configuration, IAdminService adminService, IMemoryCache memoryCache, IStringLocalizer<SharedResource> sharedLocalizer, IFufquestionnaireService fufquestionnaireService, IAdverseEventService adverseEvent, IMessageService messageService, IPatientService patient, IFileProvider fileProvider, IPrescriberService prescriber, IBaselineDataMaster baselinedata, IQuestionnaireService questionnaireService, ICountryService country, IHostingEnvironment environment, IFollowUpFormMaster followupform)
        {
            this._configuration = configuration;
            this._adminService = adminService;
            this._patient = patient;
            this.fileProvider = fileProvider;
            this._prescriber = prescriber;
            _db = new EmcureCERIDBContext();
            this._baselinedata = baselinedata;
            this._messageService = messageService;
            this._questionnaireService = questionnaireService;
            this._country = country;
            this._environment = environment;
            this._followupform = followupform;
            this._adverseEvent = adverseEvent;
            this._fufquestionnaireService = fufquestionnaireService;
            this._sharedLocalizer = sharedLocalizer;
            this._cache = memoryCache;
            this._global = global;
        }

        public IActionResult Index()
        {
            //return RedirectToAction("RegisteredPatients", "Prescriber");
            ViewBag.ContactTo = _configuration["ContactTo"].ToString();
            return View();
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


        protected virtual string GetSingleStatus(bool? status)
        {
            return status == null ? "Pending" : status == true ? "Approved" : "Rejected";
        }

        /// <summary>
        /// All Patient
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult RegisteredPatients(int id = 0)
        {
            return View();
        }

        public JsonResult GetPatient()
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            ServiceResponseList<PatientDetails> allPatient = new ServiceResponseList<PatientDetails>();
            allPatient = _patient.GetAllPatientsByPrescriber(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")));
            foreach (var item in allPatient.Results)
            {
                PatientViewModel model = new PatientViewModel();
                model.Id = item.Id;
                model.UniqueId = item.UniqueId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.IsConsentFcheckByAdmin = item.IsConsentFcheckByAdmin;
                model.CStatus = GetSingleStatus(item.IsStatus);
                var baseline = _baselinedata.GetBaselineDataByPatientId(item.Id);
                if (baseline != null)
                {
                    model.BStatus = GetSingleStatus(baseline.IsStatus);
                    model.IsBaselineDataByAdmin = baseline.IsConfirmedByAdmin;
                }
                else
                {
                    model.BStatus = "Pending";
                    model.IsBaselineDataByAdmin = false;
                }
                model.FStatus = GetStatus(model.CStatus, model.BStatus);
                patientList.Add(model);
            }
            return Json(new { data = patientList }, new JsonSerializerSettings());
        }


        protected virtual string GetStatus(string consent, string baseline)
        {
            if (consent == "Approved" && baseline == "Approved")
                return "Approved";
            if (consent == "Approved" && baseline == "Rejected" || consent == "Rejected" && baseline == "Approved" || consent == "Rejected" && baseline == "Pending")
                return "Partially Rejected";
            if (consent == "Rejected" && baseline == "Rejected")
                return "Rejected";
            if (consent == "Approved" && baseline == "Pending" || consent == "Pending" && baseline == "Approved")
                return "Partially Approved";
            if (consent == "Pending" && baseline == "Pending")
                return "Pending Approval";
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult PatientAddOrEdit()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult DeleteFollowDetails(NewModelVM model)
        {
            int patientId = Convert.ToInt32(model.PatientID);
            int followUpFormId = Convert.ToInt32(model.FollowUpFormID);
            var user = _db.PatientFollowUpForm.Where(o => o.PatientId == patientId && o.FollowUpFormId == followUpFormId).FirstOrDefault();
            if (user != null)
            {
                user.IsDeleted = true;
                _db.SaveChanges();
                return Json(new { success = true, message = _sharedLocalizer["Deleted Successfully"].Value });
            }
            else
            {
                return Json(new { success = true, message = _sharedLocalizer["Error"].Value });
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Prescriber")]
        [HttpGet]
        public ActionResult FollowDetails(NewModelVM model)
        {
            if (String.IsNullOrEmpty(model.PatientID) || String.IsNullOrEmpty(model.FollowUpFormID))
            {
                if (TempData["PatientID"] != null && TempData["FollowUpFormID"] != null)
                {
                    model.PatientID = TempData["PatientID"].ToString();
                    model.FollowUpFormID = TempData["FollowUpFormID"].ToString();
                    return RedirectToAction("FollowDetails", "Prescriber", model);
                }
                else
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            else
            {
                TempData["PatientID"] = model.PatientID;
                TempData["FollowUpFormID"] = model.FollowUpFormID;
            }

            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                return RedirectToAction("FollowDetailsView", "Prescriber", model);
            }


            List<SelectListItem> sdaList = new List<SelectListItem>();
            List<SelectListItem> outList = new List<SelectListItem>();
            List<SelectListItem> rsdList = new List<SelectListItem>();


            List<StudyDrug> allSda = CacheTryGetAllStudyDrug();
            List<Outcome> allOut = CacheTryGetAllOutcome();
            List<RelaStudyDrug> allRsd = CacheTryGetAllRelaStudyDrug();



            sdaList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"].Value });
            outList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Outcome"].Value });
            rsdList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Relationship to Study Drug"].Value });


            foreach (var item in allSda.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }
            foreach (var item in allOut.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }
            foreach (var item in allRsd.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }

            ViewBag.SdaList = sdaList;
            ViewBag.OutList = outList;
            ViewBag.RsdList = rsdList;

            ViewBag.patientid = Convert.ToInt32(model.PatientID);

            FollowUpFormMaster model2 = _followupform.FindFufquestionnairesResult(Convert.ToInt32(model.FollowUpFormID));
            if (model2.Id != 0)
            {
                model2.Fufquest1Navigation.Pexperienced = model2.Fufquest1Navigation.Pexperienced == null ? true : model2.Fufquest1Navigation.Pexperienced;
                model2.Fufquest5Navigation.MedicalHistory = model2.Fufquest5Navigation.MedicalHistory == null ? true : model2.Fufquest5Navigation.MedicalHistory;
                model2.Fufquest6Navigation.Medications = model2.Fufquest6Navigation.Medications == null ? true : model2.Fufquest6Navigation.Medications;
                //model2.Fufquest7Navigation.ConfirmFuf = true;
            }

            //Patient Details
            PatientDetails patDetails = GetPatient(Convert.ToInt32(model.PatientID));
            ViewBag.PatientName = "" + patDetails.FirstName + " " + patDetails.LastName + "";


            //Prescriber Details
            PrescriberDetails preDetails = GetPrescriber(patDetails.Id);
            ViewBag.Name = "" + preDetails.FirstName + " " + preDetails.LastName + "";
            ViewBag.Email = _db.AspNetUsers.Where(o => o.UserId == preDetails.AspNetUserId).Select(o => o.Email).FirstOrDefault();
            ViewBag.RegistrationNumber = preDetails.UniqueId;
            ViewBag.TelephoneNumber = preDetails.TelephoneNumber;
            ViewBag.HospitalAddress = preDetails.HospitalAddress;
            ViewBag.ContactAddress = preDetails.ContactAddress;

            return View(model2);
        }

        [HttpGet]
        public ActionResult FollowDetailsView(NewModelVM model)
        {

            if (String.IsNullOrEmpty(model.PatientID) || String.IsNullOrEmpty(model.FollowUpFormID))
            {
                if (TempData["PatientID"] != null && TempData["FollowUpFormID"] != null)
                {
                    model.PatientID = TempData["PatientID"].ToString();
                    model.FollowUpFormID = TempData["FollowUpFormID"].ToString();
                    return RedirectToAction("FollowDetailsView", "Prescriber", model);
                }
                else
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            else
            {
                TempData["PatientID"] = model.PatientID;
                TempData["FollowUpFormID"] = model.FollowUpFormID;
            }

            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            List<SelectListItem> sdaList = new List<SelectListItem>();
            List<SelectListItem> outList = new List<SelectListItem>();
            List<SelectListItem> rsdList = new List<SelectListItem>();

            List<StudyDrug> allSda = CacheTryGetAllStudyDrug();
            List<Outcome> allOut = CacheTryGetAllOutcome();
            List<RelaStudyDrug> allRsd = CacheTryGetAllRelaStudyDrug();

            sdaList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Study Drug Action"].Value });
            outList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Outcome"].Value });
            rsdList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Relationship to Study Drug"].Value });

            foreach (var item in allSda.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        sdaList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }
            foreach (var item in allOut.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        outList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }
            foreach (var item in allRsd.Where(o => o.IsEnabled == true))
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameBe });
                        break;
                    case "de-DE":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameDe });
                        break;
                    case "en-GB":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameGb });
                        break;
                    case "es-ES":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameEs });
                        break;
                    case "fr-FR":
                        rsdList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.NameFr });
                        break;
                }
            }

            ViewBag.SdaList = sdaList;
            ViewBag.OutList = outList;
            ViewBag.RsdList = rsdList;



            ViewBag.patientid = Convert.ToInt32(model.PatientID);

            FollowUpFormMaster model2 = _followupform.FindFufquestionnairesResult(Convert.ToInt32(model.FollowUpFormID));

            if (model2.Id != 0)
            {
                model2.Fufquest1Navigation.Pexperienced = model2.Fufquest1Navigation.Pexperienced == null ? true : model2.Fufquest1Navigation.Pexperienced;
                model2.Fufquest5Navigation.MedicalHistory = model2.Fufquest5Navigation.MedicalHistory == null ? true : model2.Fufquest5Navigation.MedicalHistory;
                model2.Fufquest6Navigation.Medications = model2.Fufquest6Navigation.Medications == null ? true : model2.Fufquest6Navigation.Medications;

                if (model2.Fufquest1Navigation.StudyDaid1 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid1).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug1 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug1 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug1 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug1 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug1 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId1 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId1).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome1 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome1 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome1 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome1 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome1 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId1 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId1).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug1 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug1 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug1 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug1 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug1 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }


                if (model2.Fufquest1Navigation.StudyDaid2 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid2).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug2 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug2 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug2 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug2 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug2 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId2 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId2).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome2 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome2 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome2 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome2 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome2 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId2 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId2).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug2 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug2 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug2 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug2 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug2 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }



                if (model2.Fufquest1Navigation.StudyDaid3 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid3).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug3 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug3 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug3 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug3 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug3 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId3 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId3).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome3 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome3 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome3 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome3 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome3 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId3 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId3).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug3 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug3 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug3 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug3 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug3 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }



                if (model2.Fufquest1Navigation.StudyDaid4 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid4).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug4 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug4 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug4 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug4 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug4 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId4 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId4).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome4 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome4 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome4 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome4 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome4 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId4 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId4).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug4 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug4 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug4 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug4 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug4 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }


                if (model2.Fufquest1Navigation.StudyDaid5 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid5).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug5 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug5 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug5 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug5 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug5 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId5 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId5).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome5 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome5 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome5 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome5 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome5 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId5 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId5).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug5 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug5 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug5 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug5 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug5 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }


                if (model2.Fufquest1Navigation.StudyDaid6 != 0)
                {
                    StudyDrug studyDrug = CacheTryGetAllStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.StudyDaid6).FirstOrDefault();
                    if (studyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedstudyDrug6 = studyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedstudyDrug6 = studyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedstudyDrug6 = studyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedstudyDrug6 = studyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedstudyDrug6 = studyDrug.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.OutcomeId6 != 0)
                {
                    Outcome outcome = CacheTryGetAllOutcome().Where(o => o.Id == model2.Fufquest1Navigation.OutcomeId6).FirstOrDefault();
                    if (outcome != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedOutcome6 = outcome.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedOutcome6 = outcome.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedOutcome6 = outcome.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedOutcome6 = outcome.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedOutcome6 = outcome.NameFr;
                                break;
                        }
                    }
                }
                if (model2.Fufquest1Navigation.RelaStudyId6 != 0)
                {
                    RelaStudyDrug relaStudyDrug = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == model2.Fufquest1Navigation.RelaStudyId6).FirstOrDefault();
                    if (relaStudyDrug != null)
                    {
                        switch (cultureName)
                        {
                            case "nl-BE":
                                ViewBag.SelectedRelaStudyDrug6 = relaStudyDrug.NameBe;
                                break;
                            case "de-DE":
                                ViewBag.SelectedRelaStudyDrug6 = relaStudyDrug.NameDe;
                                break;
                            case "en-GB":
                                ViewBag.SelectedRelaStudyDrug6 = relaStudyDrug.NameGb;
                                break;
                            case "es-ES":
                                ViewBag.SelectedRelaStudyDrug6 = relaStudyDrug.NameEs;
                                break;
                            case "fr-FR":
                                ViewBag.SelectedRelaStudyDrug6 = relaStudyDrug.NameFr;
                                break;
                        }
                    }
                }

            }

            //Patient Details
            PatientDetails patDetails = GetPatient(Convert.ToInt32(model.PatientID));
            ViewBag.PatientName = "" + patDetails.FirstName + " " + patDetails.LastName + "";


            //Prescriber Details
            PrescriberDetails preDetails = GetPrescriber(patDetails.Id);
            ViewBag.Name = "" + preDetails.FirstName + " " + preDetails.LastName + "";
            ViewBag.Email = _db.AspNetUsers.Where(o => o.UserId == preDetails.AspNetUserId).Select(o => o.Email).FirstOrDefault();
            ViewBag.RegistrationNumber = preDetails.UniqueId;
            ViewBag.TelephoneNumber = preDetails.TelephoneNumber;
            ViewBag.HospitalAddress = preDetails.HospitalAddress;
            ViewBag.ContactAddress = preDetails.ContactAddress;

            return View(model2);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddFollowUpForm(int id = 0)
        {
            AddFollowUpFormVM pvm = new AddFollowUpFormVM();
            pvm.Id = id;
            return View(pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFollowUpForm(AddFollowUpFormVM model)
        {
            if (ModelState.IsValid)
            {
                Fufquestionnaire1 model1 = new Fufquestionnaire1();
                model1.Heading = "Adverse Events";
                model1.IsFulFill = false;
                _db.Fufquestionnaire1.Add(model1);
                _db.SaveChanges();

                Fufquestionnaire2 model2 = new Fufquestionnaire2();
                model2.Heading = "Relevant Laboratory Investigations";
                model2.IsFulFill = false;
                _db.Fufquestionnaire2.Add(model2);
                _db.SaveChanges();

                Fufquestionnaire3 model3 = new Fufquestionnaire3();
                model3.Heading = "Patient Outcome";
                model3.IsFulFill = false;
                _db.Fufquestionnaire3.Add(model3);
                _db.SaveChanges();

                Fufquestionnaire4 model4 = new Fufquestionnaire4();
                model4.Heading = "Cidofovir Administration";
                model4.IsFulFill = false;
                _db.Fufquestionnaire4.Add(model4);
                _db.SaveChanges();


                Fufquestionnaire5 model5 = new Fufquestionnaire5();
                model5.Heading = "Medical History";
                model5.IsFulFill = false;
                _db.Fufquestionnaire5.Add(model5);
                _db.SaveChanges();

                Fufquestionnaire6 model6 = new Fufquestionnaire6();
                model6.Heading = "Concurrent Medical Conditions And Medications";
                model6.IsFulFill = false;
                _db.Fufquestionnaire6.Add(model6);
                _db.SaveChanges();

                Fufquestionnaire7 model7 = new Fufquestionnaire7();
                model7.Heading = "Prescribers Sign Off";
                model7.ConfirmFuf = false;
                model7.IsFulFill = false;
                _db.Fufquestionnaire7.Add(model7);
                _db.SaveChanges();

                FollowUpFormMaster model8 = new FollowUpFormMaster();
                model8.Fufquest1 = model1.Id;
                model8.Fufquest2 = model2.Id;
                model8.Fufquest3 = model3.Id;
                model8.Fufquest4 = model4.Id;
                model8.Fufquest5 = model5.Id;
                model8.Fufquest6 = model6.Id;
                model8.Fufquest7 = model7.Id;
                _db.FollowUpFormMaster.Add(model8);
                _db.SaveChanges();


                PatientFollowUpForm newModel = new PatientFollowUpForm();
                newModel.PatientId = model.Id;
                newModel.FollowUpFormId = model8.Id;
                newModel.Date = DateTime.Now;
                var srno = _db.PatientFollowUpForm.Where(o => o.PatientId == model.Id).Select(o => o.SrNo).LastOrDefault();
                newModel.SrNo = srno != null ? Convert.ToInt32(srno) + 1 : 1;
                newModel.FollowUpFormName = model.FollowUpFormName;
                newModel.IsDeleted = false;
                _db.PatientFollowUpForm.Add(newModel);
                _db.SaveChanges();

                //Send a mail to admin 
                SendMailToAdmin(model.Id, "FUF");

                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            return View(model);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFollowUpForm(int id = 0)
        {
            bool IsAdmin = false;
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                IsAdmin = true;
            }

            var followupformList = _db.PatientFollowUpForm.Where(o => o.PatientId == id).Where(o => o.IsDeleted == false).ToList();
            List<FollowUpFormVM> followUpFormVMList = new List<FollowUpFormVM>();

            foreach (var item in followupformList)
            {
                FollowUpFormVM com = new FollowUpFormVM();
                com.SrNo = item.SrNo;
                com.Id = item.FollowUpFormId;
                com.FollowUpFormName = item.FollowUpFormName;
                com.Date = item.Date;
                followUpFormVMList.Add(com);
            }
            return Json(new { data = followUpFormVMList, result = IsAdmin }, new JsonSerializerSettings());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        public ActionResult PatientAddOrEdit(PatientViewModel model)
        {

            ModelState.Remove("Reason");
            ModelState.Remove("IsConsentFcheckByAdmin");
            if (ModelState.IsValid)
            {
                PatientDetails perDetail = new PatientDetails();
                perDetail.AspNetUserId = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                perDetail.Point1Date = model.Point1Date;
                perDetail.Point1 = model.Point1;
                perDetail.Point2 = model.Point2;
                perDetail.Point3 = model.Point3;
                perDetail.Point4 = model.Point4;
                perDetail.Point5 = model.Point5;
                perDetail.Point6 = model.Point6;
                perDetail.Point7 = model.Point7;
                perDetail.FirstName = model.FirstName;
                perDetail.LastName = model.LastName;
                perDetail.RfirstName = model.RFirstName;
                perDetail.RlastName = model.RLastName;
                perDetail.IsStatus = null;
                perDetail.IsConsentFcheckByHcp = false;
                perDetail.IsConsentFcheckByAdmin = false;
                var uniqueID = _db.PatientDetails.Select(o => o.UniqueId).LastOrDefault();
                if (uniqueID != null)
                {
                    perDetail.UniqueId = IncreamentUnique("CERIPAT", _db.PatientDetails.Select(o => o.UniqueId).LastOrDefault());
                }
                else
                {
                    perDetail.UniqueId = "CERIPAT0001";
                }
                perDetail.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                perDetail.CreatedOnUtc = DateTime.Now;
                perDetail.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                perDetail.UpdatedOnUtc = DateTime.Now;
                _patient.AddPatientDetails(perDetail);


                Questionnaire1 model1 = new Questionnaire1();
                model1.Heading = "Informed Consent and Eligibility";
                model1.IsFulFill = false;
                _db.Questionnaire1.Add(model1);
                _db.SaveChanges();

                Questionnaire2 model2 = new Questionnaire2();
                model2.Heading = "Demographic Data";
                model2.IsFulFill = false;
                model2.Dob = model.DateOfBirth;
                if (model.DateOfBirth != null)
                {
                    DateTime dob = Convert.ToDateTime(model.DateOfBirth);
                    perDetail.RejectionReason = dob.ToShortDateString(); // this is use only for showing dob on download ICF
                    AgeVM text = CalculateYourAge(dob);
                    if (text.Years >= 18)
                    {
                        model2.IsAdult = true;
                    }
                    else
                    {
                        model2.IsAdult = false;
                    }
                }
                else {
                    model2.IsAdult = false;
                }
                _db.Questionnaire2.Add(model2);
                _db.SaveChanges();

                Questionnaire3 model3 = new Questionnaire3();
                model3.Heading = "Treatment Init Details";
                model3.IsFulFill = false;

                _db.Questionnaire3.Add(model3);
                _db.SaveChanges();

                Questionnaire4 model4 = new Questionnaire4();
                model4.Heading = "Prescribers Sign Off";
                model4.IsFulFill = false;
                model4.ConfirmBdf = false;
                _db.Questionnaire4.Add(model4);
                _db.SaveChanges();

                BaselineDataMaster model5 = new BaselineDataMaster();
                model5.PatientId = perDetail.Id;
                model5.Quest1 = model1.Id;
                model5.Quest2 = model2.Id;
                model5.Quest3 = model3.Id;
                model5.Quest4 = model4.Id;
                model5.IsStatus = null;
                model5.IsConfirmedByHcp = false;
                model5.IsConfirmedByAdmin = false;
                _db.BaselineDataMaster.Add(model5);
                _db.SaveChanges();

               // return RedirectToAction("InformedConsentForm", "Prescriber", new { id = model5.PatientId });

                //  RedirectToAction("PatientUploadDocument", "Prescriber");

                return Json(new { success = "success", data = perDetail }, new JsonSerializerSettings());
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Getconsent_form()
        {
            //   var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            var cultureName = "nl-BE";
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "Form");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "FINAL_informed_consent_form.docx");
                    string fileName1 = "FINAL_informed_consent_form.docx";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName2 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName3 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName4 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName5 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }

        [HttpGet]
        public IActionResult Getpaintemnt_infosheet()
        {
            //   var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            var cultureName = "nl-BE";
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "Form");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "FINAL_informed_consent_form.docx");
                    string fileName1 = "FINAL_informed_consent_form.docx";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName2 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName3 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName4 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName5 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }



        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        public int CalculateAgeNew(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }


        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public AgeVM CalculateYourAge(DateTime Dob)
        {

            AgeVM ageVM = new AgeVM();

            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            ageVM.Years = Years;
            ageVM.Months = Months;
            ageVM.Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            ageVM.Hours = Now.Subtract(PastYearDate).Hours;
            ageVM.Minutes = Now.Subtract(PastYearDate).Minutes;
            ageVM.Seconds = Now.Subtract(PastYearDate).Seconds;

            return ageVM;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
        }


        public void SendMailToAdmin(int patientId, string currentForm)
        {
            var adminSubject = "";
            var preSubject = "";
            string preEmailBody = "";
            string adminEmailbody = "";
            string url = _global.GetBaseUrl() + "/Account/Login";
            AdminObject admin = _adminService.GetAdmin();
            PatientPrescriverObject obj = _adminService.GetPatientPrescriberByPatientId(patientId);
            EmailHelper email = new EmailHelper();

            switch (currentForm)
            {
                case "ICF":
                    preSubject = _sharedLocalizer["Thank you for Submission of Informed Consent Form"].Value;
                    preEmailBody = _messageService.GetPrescriberAcknowledgementICF(obj.PrescriberFullName, url, admin.Email);

                    adminSubject = "Prescriber " + obj.PrescriberFullName + " Uploaded Informed Consent Form for Patient's " + obj.PatientFullName + " for Approval.";
                    adminEmailbody = _messageService.SendMessageToAdmin(admin.FullName, obj.PrescriberFullName, obj.PatientFullName, "Informed Consent Form");
                    break;
                case "BDF":
                    preSubject = _sharedLocalizer["Thank you for Submission of Baseline Data Form"].Value;
                    preEmailBody = _messageService.GetPrescriberAcknowledgementBDF(obj.PrescriberFullName, url, admin.Email);

                    adminSubject = "Prescriber " + obj.PrescriberFullName + " Uploaded Baseline Data Form for Patient's " + obj.PatientFullName + " for Approval.";
                    adminEmailbody = _messageService.SendMessageToAdmin(admin.FullName, obj.PrescriberFullName, obj.PatientFullName, "Baseline Data Form");
                    break;
                case "FUF":
                    preSubject = _sharedLocalizer["Thank you for Adding of Follow Up From"].Value;
                    preEmailBody = _messageService.GetPrescriberAcknowledgementFUF(obj.PrescriberFullName, url, admin.Email);

                    adminSubject = "Prescriber " + obj.PrescriberFullName + " added Follow Up Form for Patient's " + obj.PatientFullName + ".";
                    adminEmailbody = _messageService.SendMessageToAdmin(admin.FullName, obj.PrescriberFullName, obj.PatientFullName, "Follow Up From");
                    break;
            }

            //Mail to Admin
            email.SendMail(admin.Email, "", adminSubject, adminEmailbody);
            //Mail to Prescriber
            email.SendMail(obj.PrescriberEmail, "", preSubject, preEmailBody);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult PatientUploadDocument(int id = 0)
        {
            PatientViewModel pvm = new PatientViewModel();
            pvm.Id = id;
            return View(pvm);
        }


        [HttpPost]
        public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        {
            if (model == null || model.FileToUpload == null || model.FileToUpload.Length == 0)
                return Json(new { result = "error", message = _sharedLocalizer["File Not Selected"].Value }, new JsonSerializerSettings());
            //return Content("file not selected");

            var filename = model.FileToUpload.GetFilename();

            string extension = System.IO.Path.GetExtension(filename);

            if (extension == ".pdf" || extension == ".PDF")
            {
                string result = filename.Substring(0, filename.Length - extension.Length);

                var finalName = result + DateTime.Now.ToString("yyyyMMddTHHmmss") + extension;

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/document",
                            finalName);

                if (model.PatientId != 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.FileToUpload.CopyToAsync(stream);
                    }

                    PatientDetails uer = _patient.GetPatient(model.PatientId);
                    if (uer != null)
                    {
                        uer.PdfUploadDate = DateTime.Now;
                        uer.PdfName = finalName;
                        uer.IsConsentFcheckByHcp = true;
                        uer.PdfUploadBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        _patient.UpdatePatientDetails(uer);

                        //Send a mail to admin 
                        SendMailToAdmin(uer.Id, "ICF");
                    }

                    return RedirectToAction("InformedConsentForm", "Prescriber", new { id = model.PatientId });

                }
                else
                {
                    return Json(new { result = "error", message = _sharedLocalizer["Patient Not Selected"].Value }, new JsonSerializerSettings());
                }

            }
            else
            {
                return Json(new { result = "error", message = _sharedLocalizer["File Extension Is InValid - Only Upload PDF File"].Value }, new JsonSerializerSettings());
            }

        }


        [HttpPost]
        public ActionResult UploadFilesCity()
        {
            var newFileName = string.Empty;
            var dict = HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var patientID = dict["patientID"];
            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count != 0)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);
                        if (FileExtension == ".docx" || FileExtension == ".DOCX")
                        {
                            // concating  FileName + FileExtension
                            newFileName = myUniqueFileName + FileExtension;

                            // Combines two strings into a path.
                            fileName = Path.Combine(_environment.WebRootPath, "document") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            PathDB = "document/" + newFileName;

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();

                                PatientDetails uer = _patient.GetPatient(Convert.ToInt32(patientID));
                                if (uer != null)
                                {
                                    uer.PdfUploadDate = DateTime.Now;
                                    uer.PdfName = newFileName;
                                    uer.IsConsentFcheckByHcp = true;
                                    uer.PdfUploadBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                    _patient.UpdatePatientDetails(uer);

                                    BaselineDataMaster baseEntity = _baselinedata.FindQuestionnairesResult(uer.Id);
                                    DateTime dob = baseEntity.Quest2Navigation.Dob.HasValue ? baseEntity.Quest2Navigation.Dob.Value : DateTime.Now;
                                    AgeVM text = CalculateYourAge(dob);
                                    if (text.Years >= 18)
                                    {
                                        baseEntity.Quest2Navigation.IsAdult = true;
                                        _baselinedata.UpdateQuestionnaire(baseEntity);
                                    }

                                    SendMailToAdmin(uer.Id, "ICF");
                                }
                                else
                                {
                                    return Json(new { data = "error", message = _sharedLocalizer["Some error occure please contact to admin."].Value }, new JsonSerializerSettings());
                                }
                            }
                        }
                        else
                        {
                            return Json(new { data = "error", message = _sharedLocalizer["File Extension Is InValid - Only Upload PDF File"].Value }, new JsonSerializerSettings());
                        }
                    }
                }

                return Json(new { data = "success", message = _sharedLocalizer["Informed Consent Form has been uploaded successfully"].Value }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "error", message = _sharedLocalizer["Please select the file to upload"].Value }, new JsonSerializerSettings());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AcceptICForm(int id = 0)
        {
            if (id != 0)
            {
                var patient = _db.PatientDetails.Where(o => o.Id == id).FirstOrDefault();
                patient.IsConsentFcheckByHcp = true;
                patient.IsConsentFcheckByAdmin = true;
                patient.IsStatus = true;
                patient.RejectionReason = null;
                _db.SaveChanges();

                if (patient != null)
                {
                    PatientPrescriverObject obj = _adminService.GetPatientPrescriberByPatientId(patient.Id);
                    EmailHelper email = new EmailHelper();
                    string emailbody = _messageService.GetPatientApprovedICF(obj.PrescriberFullName, obj.PatientFullName);
                    email.SendMail(obj.PrescriberEmail, "", ""+_sharedLocalizer["Your Patient's"].Value+" " + obj.PatientFullName + " " + _sharedLocalizer["Approved"].Value + "", emailbody);
                }

                return Json(new { success = true, message = _sharedLocalizer["Informed Consent Form is Approved Please go ahead and Approve the Baseline Data Form to Successfully Register a Patient for Cidofovir Exposure Study."].Value });
            }
            else
            {
                return Json(new { });
            }
        }

        protected virtual void SendEmailToPrescriber(int patientId, string formName, string reason)
        {
            if (patientId != 0)
            {
                PatientPrescriverObject obj = _adminService.GetPatientPrescriberByPatientId(patientId);
                EmailHelper email = new EmailHelper();
                string emailbody = _messageService.GetPatientReject(obj.PrescriberFullName, obj.PatientFullName, reason, formName);
                email.SendMail(obj.PrescriberEmail, "", "" + _sharedLocalizer["Your Patient's"].Value + " " + formName + " " + _sharedLocalizer["Rejected"].Value + "", emailbody);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RejectBLForm(int id = 0)
        {
            ReasonViewModel pvm = new ReasonViewModel();
            pvm.Id = id;
            return View(pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RejectBLForm(ReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var baseline = _baselinedata.GetBaselineDataByPatientId(model.Id);
                baseline.RejectionReason = model.Reason;
                baseline.IsConfirmedByHcp = false;
                baseline.IsConfirmedByAdmin = false;
                baseline.IsStatus = false;
                _baselinedata.UpdateQuestionnaire(baseline);
                SendEmailToPrescriber(model.Id, "Baseline Data Form", model.Reason);
                return Json(new { success = "success" }, new JsonSerializerSettings());
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RejectICForm(int id = 0)
        {
            ReasonViewModel pvm = new ReasonViewModel();
            pvm.Id = id;
            return View(pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RejectICForm(ReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = _patient.GetPatient(model.Id);
                patient.RejectionReason = model.Reason;
                patient.IsConsentFcheckByHcp = false;
                patient.IsConsentFcheckByAdmin = false;
                patient.IsStatus = false;
                _patient.UpdatePatientDetails(patient);
                SendEmailToPrescriber(model.Id, "Informed Consent Form", model.Reason);
                return Json(new { success = "success", data = patient }, new JsonSerializerSettings());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Eligibility(BaselineDataMaster model)
        {
            _questionnaireService.UpdateQuestionnaire1(model.Quest1Navigation);
            return Json(new { result = "success", message = "tab1Id" }, new JsonSerializerSettings());

            //ModelState.Remove("IsConfirmedByAdmin");
            //if (ModelState.IsValid)
            //{
            //    _questionnaireService.UpdateQuestionnaire1(model.Quest1Navigation);
            //    return Json(new { result = "success", message = "tab1Id" }, new JsonSerializerSettings());
            //}
            //else
            //{
            //    return Json(new { result = "error", message = "An error has occurred. Please contact your system administrator." }, new JsonSerializerSettings());
            //}


        }

        [HttpPost]
        public IActionResult Demographic(BaselineDataMaster model)
        {
            if (model.Quest2Navigation.MedicalHistory == false || model.Quest2Navigation.MedicalHistory == null)
            {
                model.Quest2Navigation.Mh1anySignificant = null;
                model.Quest2Navigation.Mh1startDate = null;
                model.Quest2Navigation.Mh1stopDate = null;
                model.Quest2Navigation.Mh1ongoing = null;

                model.Quest2Navigation.Mh2anySignificant = null;
                model.Quest2Navigation.Mh2startDate = null;
                model.Quest2Navigation.Mh2stopDate = null;
                model.Quest2Navigation.Mh2ongoing = null;

                model.Quest2Navigation.Mh2anySignificant = null;
                model.Quest2Navigation.Mh2startDate = null;
                model.Quest2Navigation.Mh2stopDate = null;
                model.Quest2Navigation.Mh2ongoing = null;

                model.Quest2Navigation.Mh2anySignificant = null;
                model.Quest2Navigation.Mh2startDate = null;
                model.Quest2Navigation.Mh2stopDate = null;
                model.Quest2Navigation.Mh2ongoing = null;
            }

            if (model.Quest2Navigation.Medications == false || model.Quest2Navigation.Medications == null)
            {

                model.Quest2Navigation.M1medication = null;
                model.Quest2Navigation.M1indication = null;
                model.Quest2Navigation.M1dosageUnits = null;
                model.Quest2Navigation.M1frequency = null;
                model.Quest2Navigation.M1startDate = null;
                model.Quest2Navigation.M1stopDate = null;
                model.Quest2Navigation.M1ongoing = null;


                model.Quest2Navigation.M2medication = null;
                model.Quest2Navigation.M2indication = null;
                model.Quest2Navigation.M2dosageUnits = null;
                model.Quest2Navigation.M2frequency = null;
                model.Quest2Navigation.M2startDate = null;
                model.Quest2Navigation.M2stopDate = null;
                model.Quest2Navigation.M2ongoing = null;

                model.Quest2Navigation.M3medication = null;
                model.Quest2Navigation.M3indication = null;
                model.Quest2Navigation.M3dosageUnits = null;
                model.Quest2Navigation.M3frequency = null;
                model.Quest2Navigation.M3startDate = null;
                model.Quest2Navigation.M3stopDate = null;
                model.Quest2Navigation.M3ongoing = null;

                model.Quest2Navigation.M4medication = null;
                model.Quest2Navigation.M4indication = null;
                model.Quest2Navigation.M4dosageUnits = null;
                model.Quest2Navigation.M4frequency = null;
                model.Quest2Navigation.M4startDate = null;
                model.Quest2Navigation.M4stopDate = null;
                model.Quest2Navigation.M4ongoing = null;


                model.Quest2Navigation.M5medication = null;
                model.Quest2Navigation.M5indication = null;
                model.Quest2Navigation.M5dosageUnits = null;
                model.Quest2Navigation.M5frequency = null;
                model.Quest2Navigation.M5startDate = null;
                model.Quest2Navigation.M5stopDate = null;
                model.Quest2Navigation.M5ongoing = null;


                model.Quest2Navigation.M6medication = null;
                model.Quest2Navigation.M6indication = null;
                model.Quest2Navigation.M6dosageUnits = null;
                model.Quest2Navigation.M6frequency = null;
                model.Quest2Navigation.M6startDate = null;
                model.Quest2Navigation.M6stopDate = null;
                model.Quest2Navigation.M6ongoing = null;

                model.Quest2Navigation.M7medication = null;
                model.Quest2Navigation.M7indication = null;
                model.Quest2Navigation.M7dosageUnits = null;
                model.Quest2Navigation.M7frequency = null;
                model.Quest2Navigation.M7startDate = null;
                model.Quest2Navigation.M7stopDate = null;
                model.Quest2Navigation.M7ongoing = null;

                model.Quest2Navigation.M8medication = null;
                model.Quest2Navigation.M8indication = null;
                model.Quest2Navigation.M8dosageUnits = null;
                model.Quest2Navigation.M8frequency = null;
                model.Quest2Navigation.M8startDate = null;
                model.Quest2Navigation.M8stopDate = null;
                model.Quest2Navigation.M8ongoing = null;


                model.Quest2Navigation.M9medication = null;
                model.Quest2Navigation.M9indication = null;
                model.Quest2Navigation.M9dosageUnits = null;
                model.Quest2Navigation.M9frequency = null;
                model.Quest2Navigation.M9startDate = null;
                model.Quest2Navigation.M9stopDate = null;
                model.Quest2Navigation.M9ongoing = null;


                model.Quest2Navigation.M10medication = null;
                model.Quest2Navigation.M10indication = null;
                model.Quest2Navigation.M10dosageUnits = null;
                model.Quest2Navigation.M10frequency = null;
                model.Quest2Navigation.M10startDate = null;
                model.Quest2Navigation.M10stopDate = null;
                model.Quest2Navigation.M10ongoing = null;

                model.Quest2Navigation.M11medication = null;
                model.Quest2Navigation.M11indication = null;
                model.Quest2Navigation.M11dosageUnits = null;
                model.Quest2Navigation.M11frequency = null;
                model.Quest2Navigation.M11startDate = null;
                model.Quest2Navigation.M11stopDate = null;
                model.Quest2Navigation.M11ongoing = null;

                model.Quest2Navigation.M12medication = null;
                model.Quest2Navigation.M12indication = null;
                model.Quest2Navigation.M12dosageUnits = null;
                model.Quest2Navigation.M12frequency = null;
                model.Quest2Navigation.M12startDate = null;
                model.Quest2Navigation.M12stopDate = null;
                model.Quest2Navigation.M12ongoing = null;
            }
          
            _questionnaireService.UpdateQuestionnaire2(model.Quest2Navigation);
            return Json(new { result = "success", message = "tab2Id" }, new JsonSerializerSettings());

            //ModelState.Remove("IsConfirmedByAdmin");
            //if (ModelState.IsValid)
            //{
            //    _questionnaireService.UpdateQuestionnaire2(model.Quest2Navigation);
            //    return Json(new { result = "success", message = "tab2Id" }, new JsonSerializerSettings());
            //}
            //else
            //{
            //    return Json(new { result = "error", message = "An error has occurred. Please contact your system administrator." }, new JsonSerializerSettings());
            //}

        }

        [HttpPost]
        public IActionResult Treatment(BaselineDataMaster model)
        {
            _questionnaireService.UpdateQuestionnaire3(model.Quest3Navigation);
            return Json(new { result = "success", message = "tab3Id" }, new JsonSerializerSettings());

            //ModelState.Remove("IsConfirmedByAdmin");
            //if (ModelState.IsValid)
            //{
            //    _questionnaireService.UpdateQuestionnaire3(model.Quest3Navigation);
            //    return Json(new { result = "success", message = "tab3Id" }, new JsonSerializerSettings());
            //}
            //else
            //{
            //    return Json(new { result = "error", message = "An error has occurred. Please contact your system administrator." }, new JsonSerializerSettings());
            //}
        }

        [HttpPost]
        public IActionResult PrescriberSignOff(BaselineDataMaster model)
        {
            _questionnaireService.UpdateQuestionnaire4(model.Quest4Navigation);
            return Json(new { result = "success", message = "tab4Id" }, new JsonSerializerSettings());

            //ModelState.Remove("IsConfirmedByAdmin");
            //if (ModelState.IsValid)
            //{
            //    _questionnaireService.UpdateQuestionnaire4(model.Quest4Navigation);
            //    return Json(new { result = "success", message = "tab4Id" }, new JsonSerializerSettings());
            //}
            //else
            //{
            //    return Json(new { result = "error", message = "An error has occurred. Please contact your system administrator." }, new JsonSerializerSettings());
            //}
        }

        [HttpPost]
        public IActionResult Modification(BaselineDataMaster model)
        {

            ModelState.Remove("Patient.IsConsentFcheckByAdmin");
            ModelState.Remove("IsConfirmedByAdmin");
            if (ModelState.IsValid)
            {
                var baseline = _baselinedata.GetBaselineDataByPatientId(model.Patient.Id);
                baseline.IsConfirmedByHcp = false;
                baseline.IsConfirmedByAdmin = false;
                baseline.IsStatus = null;
                baseline.RejectionReason = "";
                _baselinedata.UpdateQuestionnaire(baseline);

                int patientId = baseline.PatientId.HasValue ? baseline.PatientId.Value : 0;
                if (patientId != 0)
                {
                    AdminObject adminObj = _adminService.GetAdmin();
                    PatientPrescriverObject obj = _adminService.GetPatientPrescriberByPatientId(patientId);
                    EmailHelper email = new EmailHelper();
                    string emailbody = _messageService.GetAdminAcknowledgementBDFModification(adminObj.FullName, obj.PrescriberFullName, obj.PatientFullName);
                    email.SendMail(adminObj.Email, "", "The Prescriber " + obj.PrescriberFullName + " has modified Baseline Data Form of " + obj.PatientFullName + " ", emailbody);
                }
                return Json(new { result = "success", message = _sharedLocalizer["Baseline Data Form are Modified"].Value }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { result = "error", message = _sharedLocalizer["Server Side Validation Fail"].Value }, new JsonSerializerSettings());
            }

        }

        public void PatientIsIsAdult(int? Id) {
            var questionnaire2 = _questionnaireService.FindQuestionnaires2Result(Id);
            if (questionnaire2.Dob != null)
            {
                DateTime dob = questionnaire2.Dob.HasValue ? questionnaire2.Dob.Value : DateTime.Now;
                AgeVM text = CalculateYourAge(dob);
                if (text.Years >= 18)
                {
                    questionnaire2.IsAdult = true;
                    _questionnaireService.UpdateQuestionnaire2(questionnaire2);
                }
            }
        }


        [HttpPost]
        public IActionResult PatientConfirm(BaselineDataMaster model)
        {

            if (ModelState.IsValid)
            {
                var baseline = _baselinedata.GetBaselineDataByPatientId(model.Patient.Id);
                baseline.IsConfirmedByHcp = true; 
                baseline.IsConfirmedByAdmin = true;
                baseline.IsStatus = true;
                baseline.RejectionReason = "";
                _baselinedata.UpdateQuestionnaire(baseline);
                PatientIsIsAdult(baseline.Quest2); //If Age >= 18 then make IsAdult true else nothing do anything;

                int patientId = baseline.PatientId.HasValue ? baseline.PatientId.Value : 0;
                if (patientId != 0)
                {
                    PatientPrescriverObject obj = _adminService.GetPatientPrescriberByPatientId(patientId);
                    EmailHelper email = new EmailHelper();
                    string emailbody = _messageService.GetPatientApprovedBDF(obj.PrescriberFullName, obj.PatientFullName);
                    email.SendMail(obj.PrescriberEmail, "", "" + _sharedLocalizer["Your Patient's"].Value + " " + obj.PatientFullName + " " + _sharedLocalizer["Approved"].Value + "", emailbody);
                }
                return Json(new { result = "success", message = _sharedLocalizer["Informed Consent Form and Baseline Data Form are verified"].Value }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { result = "error", message = _sharedLocalizer["Server Side Validation Fail"].Value  }, new JsonSerializerSettings());
            }
        }

        [HttpPost]
        public IActionResult BaselineDataSubmit(BaselineDataMaster model)
        {
            if (model.Id != 0)
            {
                BaselineDataMaster baseline = _baselinedata.FindQuestionnairesResult(model.Id);
                baseline.IsConfirmedByHcp = true;
                _baselinedata.UpdateQuestionnaire(baseline);

                //Send Email to Admin
                SendMailToAdmin(baseline.PatientId.HasValue ? baseline.PatientId.Value : 0, "BDF");
                return Json(new { result = "success", message = "reloadPage" }, new JsonSerializerSettings());
                //return RedirectToAction("BaselineDataFormView", "Prescriber", new { @id = quest.PatientId });
            }
            else
            {
                return Json(new { result = "error", message = "some error occure" }, new JsonSerializerSettings());
            }

        }



        [HttpPost]
        public IActionResult AdverseEvent(FollowUpFormMaster model)
        {
            if (model.Fufquest1Navigation.Pexperienced == false || model.Fufquest1Navigation.Pexperienced == null)
            {
                model.Fufquest1Navigation.EventTerm1 = null;
                model.Fufquest1Navigation.StartDate1 = null;
                model.Fufquest1Navigation.StopDate1 = null;
                model.Fufquest1Navigation.SaeId1 = null;
                model.Fufquest1Navigation.ComMedId1 = null;
                model.Fufquest1Navigation.StudyDaid1 = null;
                model.Fufquest1Navigation.OutcomeId1 = null;
                model.Fufquest1Navigation.RelaStudyId1 = null;

                model.Fufquest1Navigation.EventTerm2 = null;
                model.Fufquest1Navigation.StartDate2 = null;
                model.Fufquest1Navigation.StopDate2 = null;
                model.Fufquest1Navigation.SaeId2 = null;
                model.Fufquest1Navigation.ComMedId2 = null;
                model.Fufquest1Navigation.StudyDaid2 = null;
                model.Fufquest1Navigation.OutcomeId2 = null;
                model.Fufquest1Navigation.RelaStudyId2 = null;

                model.Fufquest1Navigation.EventTerm3 = null;
                model.Fufquest1Navigation.StartDate3 = null;
                model.Fufquest1Navigation.StopDate3 = null;
                model.Fufquest1Navigation.SaeId3 = null;
                model.Fufquest1Navigation.ComMedId3 = null;
                model.Fufquest1Navigation.StudyDaid3 = null;
                model.Fufquest1Navigation.OutcomeId3 = null;
                model.Fufquest1Navigation.RelaStudyId3 = null;

                model.Fufquest1Navigation.EventTerm4 = null;
                model.Fufquest1Navigation.StartDate4 = null;
                model.Fufquest1Navigation.StopDate4 = null;
                model.Fufquest1Navigation.SaeId4 = null;
                model.Fufquest1Navigation.ComMedId4 = null;
                model.Fufquest1Navigation.StudyDaid4 = null;
                model.Fufquest1Navigation.OutcomeId4 = null;
                model.Fufquest1Navigation.RelaStudyId4 = null;

                model.Fufquest1Navigation.EventTerm5 = null;
                model.Fufquest1Navigation.StartDate5 = null;
                model.Fufquest1Navigation.StopDate5 = null;
                model.Fufquest1Navigation.SaeId5 = null;
                model.Fufquest1Navigation.ComMedId5 = null;
                model.Fufquest1Navigation.StudyDaid5 = null;
                model.Fufquest1Navigation.OutcomeId5 = null;
                model.Fufquest1Navigation.RelaStudyId5 = null;

                model.Fufquest1Navigation.EventTerm6 = null;
                model.Fufquest1Navigation.StartDate6 = null;
                model.Fufquest1Navigation.StopDate6 = null;
                model.Fufquest1Navigation.SaeId6 = null;
                model.Fufquest1Navigation.ComMedId6 = null;
                model.Fufquest1Navigation.StudyDaid6 = null;
                model.Fufquest1Navigation.OutcomeId6 = null;
                model.Fufquest1Navigation.RelaStudyId6 = null;



            }

            _fufquestionnaireService.UpdateFufquestionnaire1(model.Fufquest1Navigation);
            return Json(new { result = "success", message = "tab1Id" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult ReleventLaboratory(FollowUpFormMaster model)
        {
            _fufquestionnaireService.UpdateFufquestionnaire2(model.Fufquest2Navigation);
            return Json(new { result = "success", message = "tab2Id" }, new JsonSerializerSettings());
        }
        [HttpPost]
        public IActionResult PatientOutcome(FollowUpFormMaster model)
        {
            _fufquestionnaireService.UpdateFufquestionnaire3(model.Fufquest3Navigation);
            return Json(new { result = "success", message = "tab3Id" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult CidofovirAdministration(FollowUpFormMaster model)
        {
            _fufquestionnaireService.UpdateFufquestionnaire4(model.Fufquest4Navigation);
            return Json(new { result = "success", message = "tab4Id" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult MedicalHistory(FollowUpFormMaster model)
        {
            if (model.Fufquest5Navigation.MedicalHistory == false || model.Fufquest5Navigation.MedicalHistory == null)
            {

                model.Fufquest5Navigation.C1condition = null;
                model.Fufquest5Navigation.C1startDate = null;
                model.Fufquest5Navigation.C1stopDate = null;
                model.Fufquest5Navigation.C1ongoing = null;

                model.Fufquest5Navigation.C2condition = null;
                model.Fufquest5Navigation.C2startDate = null;
                model.Fufquest5Navigation.C2stopDate = null;
                model.Fufquest5Navigation.C2ongoing = null;

                model.Fufquest5Navigation.C3condition = null;
                model.Fufquest5Navigation.C3startDate = null;
                model.Fufquest5Navigation.C3stopDate = null;
                model.Fufquest5Navigation.C3ongoing = null;

                model.Fufquest5Navigation.C4condition = null;
                model.Fufquest5Navigation.C4startDate = null;
                model.Fufquest5Navigation.C4stopDate = null;
                model.Fufquest5Navigation.C4ongoing = null;

                model.Fufquest5Navigation.C5condition = null;
                model.Fufquest5Navigation.C5startDate = null;
                model.Fufquest5Navigation.C5stopDate = null;
                model.Fufquest5Navigation.C5ongoing = null;

                model.Fufquest5Navigation.C6condition = null;
                model.Fufquest5Navigation.C6startDate = null;
                model.Fufquest5Navigation.C6stopDate = null;
                model.Fufquest5Navigation.C6ongoing = null;

                model.Fufquest5Navigation.C7condition = null;
                model.Fufquest5Navigation.C7startDate = null;
                model.Fufquest5Navigation.C7stopDate = null;
                model.Fufquest5Navigation.C7ongoing = null;

                model.Fufquest5Navigation.C8condition = null;
                model.Fufquest5Navigation.C8startDate = null;
                model.Fufquest5Navigation.C8stopDate = null;
                model.Fufquest5Navigation.C8ongoing = null;

                model.Fufquest5Navigation.C9condition = null;
                model.Fufquest5Navigation.C9startDate = null;
                model.Fufquest5Navigation.C9stopDate = null;
                model.Fufquest5Navigation.C9ongoing = null;

                model.Fufquest5Navigation.C10condition = null;
                model.Fufquest5Navigation.C10startDate = null;
                model.Fufquest5Navigation.C10stopDate = null;
                model.Fufquest5Navigation.C10ongoing = null;
            }

            _fufquestionnaireService.UpdateFufquestionnaire5(model.Fufquest5Navigation);
            return Json(new { result = "success", message = "tab5Id" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult MedicalCondition(FollowUpFormMaster model)
        {

            if (model.Fufquest6Navigation.Medications == false || model.Fufquest6Navigation.Medications == null)
            {

                model.Fufquest6Navigation.M1medication = null;
                model.Fufquest6Navigation.M1indication = null;
                model.Fufquest6Navigation.M1dosageUnits = null;
                model.Fufquest6Navigation.M1frequency = null;
                model.Fufquest6Navigation.M1startDate = null;
                model.Fufquest6Navigation.M1stopDate = null;
                model.Fufquest6Navigation.M1ongoing = null;


                model.Fufquest6Navigation.M2medication = null;
                model.Fufquest6Navigation.M2indication = null;
                model.Fufquest6Navigation.M2dosageUnits = null;
                model.Fufquest6Navigation.M2frequency = null;
                model.Fufquest6Navigation.M2startDate = null;
                model.Fufquest6Navigation.M2stopDate = null;
                model.Fufquest6Navigation.M2ongoing = null;

                model.Fufquest6Navigation.M3medication = null;
                model.Fufquest6Navigation.M3indication = null;
                model.Fufquest6Navigation.M3dosageUnits = null;
                model.Fufquest6Navigation.M3frequency = null;
                model.Fufquest6Navigation.M3startDate = null;
                model.Fufquest6Navigation.M3stopDate = null;
                model.Fufquest6Navigation.M3ongoing = null;

                model.Fufquest6Navigation.M4medication = null;
                model.Fufquest6Navigation.M4indication = null;
                model.Fufquest6Navigation.M4dosageUnits = null;
                model.Fufquest6Navigation.M4frequency = null;
                model.Fufquest6Navigation.M4startDate = null;
                model.Fufquest6Navigation.M4stopDate = null;
                model.Fufquest6Navigation.M4ongoing = null;


                model.Fufquest6Navigation.M5medication = null;
                model.Fufquest6Navigation.M5indication = null;
                model.Fufquest6Navigation.M5dosageUnits = null;
                model.Fufquest6Navigation.M5frequency = null;
                model.Fufquest6Navigation.M5startDate = null;
                model.Fufquest6Navigation.M5stopDate = null;
                model.Fufquest6Navigation.M5ongoing = null;


                model.Fufquest6Navigation.M6medication = null;
                model.Fufquest6Navigation.M6indication = null;
                model.Fufquest6Navigation.M6dosageUnits = null;
                model.Fufquest6Navigation.M6frequency = null;
                model.Fufquest6Navigation.M6startDate = null;
                model.Fufquest6Navigation.M6stopDate = null;
                model.Fufquest6Navigation.M6ongoing = null;

                model.Fufquest6Navigation.M7medication = null;
                model.Fufquest6Navigation.M7indication = null;
                model.Fufquest6Navigation.M7dosageUnits = null;
                model.Fufquest6Navigation.M7frequency = null;
                model.Fufquest6Navigation.M7startDate = null;
                model.Fufquest6Navigation.M7stopDate = null;
                model.Fufquest6Navigation.M7ongoing = null;

                model.Fufquest6Navigation.M8medication = null;
                model.Fufquest6Navigation.M8indication = null;
                model.Fufquest6Navigation.M8dosageUnits = null;
                model.Fufquest6Navigation.M8frequency = null;
                model.Fufquest6Navigation.M8startDate = null;
                model.Fufquest6Navigation.M8stopDate = null;
                model.Fufquest6Navigation.M8ongoing = null;


                model.Fufquest6Navigation.M9medication = null;
                model.Fufquest6Navigation.M9indication = null;
                model.Fufquest6Navigation.M9dosageUnits = null;
                model.Fufquest6Navigation.M9frequency = null;
                model.Fufquest6Navigation.M9startDate = null;
                model.Fufquest6Navigation.M9stopDate = null;
                model.Fufquest6Navigation.M9ongoing = null;


                model.Fufquest6Navigation.M10medication = null;
                model.Fufquest6Navigation.M10indication = null;
                model.Fufquest6Navigation.M10dosageUnits = null;
                model.Fufquest6Navigation.M10frequency = null;
                model.Fufquest6Navigation.M10startDate = null;
                model.Fufquest6Navigation.M10stopDate = null;
                model.Fufquest6Navigation.M10ongoing = null;


            }

            _fufquestionnaireService.UpdateFufquestionnaire6(model.Fufquest6Navigation);
            return Json(new { result = "success", message = "tab6Id" }, new JsonSerializerSettings());
        }

        [HttpPost]
        public IActionResult PrescriberSignOffFD(FollowUpFormMaster model)
        {
            _fufquestionnaireService.UpdateFufquestionnaire7(model.Fufquest7Navigation);
            return Json(new { result = "success", message = "tab7Id" }, new JsonSerializerSettings());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Prescriber")]
        [HttpGet]
        public ActionResult PatientDetails(int id = 0)
        {
            PatientViewModel pvm = new PatientViewModel();
            pvm.Id = id;
            return View(pvm);
        }


        //Is Prescriber Authority to Access this Patient Details 
        protected virtual bool IsPrescriberAuthority(int patientId, int prescriberId)
        {
            if (patientId != 0 && prescriberId != 0)
            {
                var patient = _patient.GetPatient(patientId);
                if (patient != null)
                {
                    int id = patient.AspNetUserId ?? 0;
                    return id == prescriberId ? true : false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Is Patient PDF Uploaded by Admin
        protected virtual bool IsPDFAvailable(int patientId, int adminId)
        {
            if (patientId != 0 && adminId != 0)
            {
                var patient = _patient.GetPatient(patientId);
                if (patient != null)
                {
                    if (patient.PdfName != null && patient.PdfName != "")
                    {
                        return true;
                    }
                    else { return false; }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Prescriber")]
        [HttpGet]
        public ActionResult InformedConsentForm(int id = 0)
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                if (!IsPrescriberAuthority(id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                if (!IsPDFAvailable(id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }

            ViewBag.patientid = id;
            PatientViewModel pvm = new PatientViewModel();
            if (id != 0)
            {
                var user = _patient.GetPatient(id);
                if (user != null)
                {
                    pvm.Id = user.Id;
                    pvm.DateOfBirth = _baselinedata.FindQuestionnairesResult(user.Id).Quest2Navigation.Dob.Value;
                    pvm.FirstName = user.FirstName;
                    pvm.LastName = user.LastName;
                    pvm.IsConsentFcheckByHcp = user.IsConsentFcheckByHcp;
                    pvm.IsConsentFcheckByAdmin = user.IsConsentFcheckByAdmin;
                    if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
                    {
                        pvm.PdfName = "~/document/" + user.PdfName;
                    }
                    else
                    {
                        if (user.IsConsentFcheckByHcp)
                        {
                            pvm.PdfName = "~/document/" + user.PdfName;
                        }
                        else
                        {
                            if (user.RfirstName == null || user.RfirstName == "") { pvm.RFirstName = "NA"; } else { pvm.RFirstName = user.RfirstName; };
                            if (user.RlastName == null || user.RlastName == "") { pvm.RLastName = "NA"; } else { pvm.RLastName = user.RlastName; };
                            pvm.Point1Date = user.Point1Date ?? DateTime.Now;
                            if (user.Point1 == null || user.Point1 == false) { pvm.Point1 = false; } else { pvm.Point1 = true; }
                            if (user.Point2 == null || user.Point2 == false) { pvm.Point2 = false; } else { pvm.Point2 = true; }
                            if (user.Point3 == null || user.Point3 == false) { pvm.Point3 = false; } else { pvm.Point3 = true; }
                            if (user.Point4 == null || user.Point4 == false) { pvm.Point4 = false; } else { pvm.Point4 = true; }
                            if (user.Point5 == null || user.Point5 == false) { pvm.Point5 = false; } else { pvm.Point5 = true; }
                            if (user.Point6 == null || user.Point6 == false) { pvm.Point6 = false; } else { pvm.Point6 = true; }
                            if (user.Point7 == null || user.Point7 == false) { pvm.Point7 = false; } else { pvm.Point7 = true; }
                        }
                    }
                }
                else
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            return View(pvm);
        }

        /// <summary>  
        /// For calculating only age  
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public static class CacheKeys
        {
            public static string AllSTD { get { return "_AllSTD"; } }
            public static string AllOUT { get { return "_AllOUT"; } }
            public static string AllRSD { get { return "_AllRSD"; } }

        }

        public List<StudyDrug> CacheTryGetAllStudyDrug()
        {
            List<StudyDrug> cacheEntry = new List<StudyDrug>();
            // Look for cache key.
            if (!_cache.TryGetValue(CacheKeys.AllSTD, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = _adverseEvent.GetAllStudyDrug().Results;
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(300));
                // Save data in cache.
                _cache.Set(CacheKeys.AllSTD, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
        public List<Outcome> CacheTryGetAllOutcome()
        {
            List<Outcome> cacheEntry = new List<Outcome>();
            // Look for cache key.
            if (!_cache.TryGetValue(CacheKeys.AllOUT, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = _adverseEvent.GetAllOutcome().Results;
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(300));
                // Save data in cache.
                _cache.Set(CacheKeys.AllOUT, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
        public List<RelaStudyDrug> CacheTryGetAllRelaStudyDrug()
        {
            List<RelaStudyDrug> cacheEntry = new List<RelaStudyDrug>();
            // Look for cache key.
            if (!_cache.TryGetValue(CacheKeys.AllRSD, out cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = _adverseEvent.GetAllRelaStudyDrug().Results;
                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(300));
                // Save data in cache.
                _cache.Set(CacheKeys.AllRSD, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }


        public string GetValue(int? id, string expression)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            if (id != 0)
            {
                string value = "";
                switch (expression)
                {
                    case "SDA":
                        switch (cultureName)
                        {
                            case "nl-BE":
                                value = CacheTryGetAllStudyDrug().Where(o => o.Id == id).Select(o => o.NameBe).FirstOrDefault();
                                break;
                            case "de-DE":
                                value = CacheTryGetAllStudyDrug().Where(o => o.Id == id).Select(o => o.NameDe).FirstOrDefault();
                                break;
                            case "en-GB":
                                value = CacheTryGetAllStudyDrug().Where(o => o.Id == id).Select(o => o.NameGb).FirstOrDefault();
                                break;
                            case "es-ES":
                                value = CacheTryGetAllStudyDrug().Where(o => o.Id == id).Select(o => o.NameEs).FirstOrDefault();
                                break;
                            case "fr-FR":
                                value = CacheTryGetAllStudyDrug().Where(o => o.Id == id).Select(o => o.NameFr).FirstOrDefault();
                                break;
                        }
                        break;
                    case "OUT":
                        switch (cultureName)
                        {
                            case "nl-BE":
                                value = CacheTryGetAllOutcome().Where(o => o.Id == id).Select(o => o.NameBe).FirstOrDefault();
                                break;
                            case "de-DE":
                                value = CacheTryGetAllOutcome().Where(o => o.Id == id).Select(o => o.NameDe).FirstOrDefault();
                                break;
                            case "en-GB":
                                value = CacheTryGetAllOutcome().Where(o => o.Id == id).Select(o => o.NameGb).FirstOrDefault();
                                break;
                            case "es-ES":
                                value = CacheTryGetAllOutcome().Where(o => o.Id == id).Select(o => o.NameEs).FirstOrDefault();
                                break;
                            case "fr-FR":
                                value = CacheTryGetAllOutcome().Where(o => o.Id == id).Select(o => o.NameFr).FirstOrDefault();
                                break;
                        }
                        break;
                    case "RSD":
                        switch (cultureName)
                        {
                            case "nl-BE":
                                value = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == id).Select(o => o.NameBe).FirstOrDefault();
                                break;
                            case "de-DE":
                                value = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == id).Select(o => o.NameDe).FirstOrDefault();
                                break;
                            case "en-GB":
                                value = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == id).Select(o => o.NameGb).FirstOrDefault();
                                break;
                            case "es-ES":
                                value = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == id).Select(o => o.NameEs).FirstOrDefault();
                                break;
                            case "fr-FR":
                                value = CacheTryGetAllRelaStudyDrug().Where(o => o.Id == id).Select(o => o.NameFr).FirstOrDefault();
                                break;
                        }
                        break;
                }
                return value;
            }
            else { return ""; }
        }

        public int BaselineSheet(ExcelWorksheet worksheet, int bsrn, BaselineDataMaster model)
        {

            int patientId = Convert.ToInt32(model.PatientId);
            PatientDetails patDetails = GetPatient(patientId);
            PrescriberDetails preDetails = GetPrescriber(patientId);

            var patientName = patDetails.FirstName + " " + patDetails.LastName;
            var hcpName = preDetails.FirstName + " " + preDetails.LastName;

            //add the headers
            worksheet.Cells[bsrn, 1].Value = _sharedLocalizer["Prescribers Reference ID"].Value; worksheet.Cells[bsrn, 1].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 1].Value = _sharedLocalizer["HCP Id"].Value; worksheet.Cells[bsrn + 1, 1].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 2].Value = _sharedLocalizer["HCP Name"].Value; worksheet.Cells[bsrn + 1, 2].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 3].Value = _sharedLocalizer["Patient Id"].Value; worksheet.Cells[bsrn + 1, 3].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 4].Value = _sharedLocalizer["First Name"].Value; worksheet.Cells[bsrn + 1, 4].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 5].Value = _sharedLocalizer["Last Name"].Value; worksheet.Cells[bsrn + 1, 5].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 6].Value = _sharedLocalizer["Gender"].Value; worksheet.Cells[bsrn + 1, 6].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 7].Value = _sharedLocalizer["Age"].Value; worksheet.Cells[bsrn + 1, 7].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 8].Value = _sharedLocalizer["DOB"].Value; worksheet.Cells[bsrn + 1, 8].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 9].Value = _sharedLocalizer["Country"].Value; worksheet.Cells[bsrn + 1, 9].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 10].Value = _sharedLocalizer["Informed Consent"].Value; worksheet.Cells[bsrn + 1, 10].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 1, 11].Value = _sharedLocalizer["Baseline Data"].Value; worksheet.Cells[bsrn + 1, 11].Style.Font.Bold = true;

            ////worksheet.Cells[bsrn + 1, 12].Value = "Medical History (Illnesses, Conditions, Surgery)"; worksheet.Cells[bsrn + 1, 12].Style.Font.Bold = true;
            worksheet.Select("L2:P2");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Medical History (Illnesses, Conditions, Surgery)"].Value;

            //worksheet.Cells[bsrn + 1, 17].Value = "Concomitant Medications"; worksheet.Cells[bsrn + 1, 17].Style.Font.Bold = true;
            worksheet.Select("Q2:Y2");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Concomitant Medications"].Value;

            //worksheet.Cells[bsrn + 1, 26].Value = "Laboratory Investigations (If Any)"; worksheet.Cells[bsrn + 1, 26].Style.Font.Bold = true;
            worksheet.Select("Z2:AB2");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Laboratory Investigations (If Any)"].Value;

            //worksheet.Cells[bsrn + 1, 29].Value = "Cidofovir Therapy Dates"; worksheet.Cells[bsrn + 1, 29].Style.Font.Bold = true;
            worksheet.Select("AC2:AG2");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Cidofovir Therapy Dates"].Value;

            worksheet.Cells[bsrn + 2, 12].Value = _sharedLocalizer["Medical History"].Value; worksheet.Cells[bsrn + 2, 12].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 13].Value = _sharedLocalizer["Please specify any significant/major illness, medical conditions or surgery in the past or at present."].Value; worksheet.Cells[bsrn + 2, 13].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 14].Value = _sharedLocalizer["Start Date"].Value; worksheet.Cells[bsrn + 2, 14].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 15].Value = _sharedLocalizer["Stop Date"].Value; worksheet.Cells[bsrn + 2, 15].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 16].Value = _sharedLocalizer["Or tick if Ongoing?"].Value; worksheet.Cells[bsrn + 2, 16].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 17].Value = _sharedLocalizer["Medications"].Value; worksheet.Cells[bsrn + 2, 17].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 18].Value = _sharedLocalizer["Medication"].Value; worksheet.Cells[bsrn + 2, 18].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 19].Value = _sharedLocalizer["Indication"].Value; worksheet.Cells[bsrn + 2, 19].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 20].Value = _sharedLocalizer["Dosage and units"].Value; worksheet.Cells[bsrn + 2, 20].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 21].Value = _sharedLocalizer["Frequency"].Value; worksheet.Cells[bsrn + 2, 21].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 22].Value = _sharedLocalizer["Route"].Value; worksheet.Cells[bsrn + 2, 22].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 23].Value = _sharedLocalizer["Start Date"].Value; worksheet.Cells[bsrn + 2, 23].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 24].Value = _sharedLocalizer["Stop Date"].Value; worksheet.Cells[bsrn + 2, 24].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 25].Value = _sharedLocalizer["Or tick if on going at Screening Visit"].Value; worksheet.Cells[bsrn + 2, 25].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 26].Value = _sharedLocalizer["Specification"].Value; worksheet.Cells[bsrn + 2, 26].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 27].Value = _sharedLocalizer["Abnormal parameters, if any"].Value; worksheet.Cells[bsrn + 2, 27].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 28].Value = _sharedLocalizer["Comments"].Value; worksheet.Cells[bsrn + 2, 28].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 29].Value = _sharedLocalizer["Indication"].Value; worksheet.Cells[bsrn + 2, 29].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 30].Value = _sharedLocalizer["Dose (including units)"].Value; worksheet.Cells[bsrn + 2, 30].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 31].Value = _sharedLocalizer["Frequency of administration"].Value; worksheet.Cells[bsrn + 2, 31].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 32].Value = _sharedLocalizer["Route of administration (IV, Intraocular, Topical, etc.)"].Value; worksheet.Cells[bsrn + 2, 32].Style.Font.Bold = true;
            worksheet.Cells[bsrn + 2, 33].Value = _sharedLocalizer["Remarks"].Value; worksheet.Cells[bsrn + 2, 33].Style.Font.Bold = true;

            worksheet.Cells[bsrn + 3, 1].Value = preDetails.UniqueId;
            worksheet.Cells[bsrn + 3, 2].Value = hcpName;
            worksheet.Cells[bsrn + 3, 3].Value = patDetails.UniqueId;
            worksheet.Cells[bsrn + 3, 4].Value = patDetails.FirstName;
            worksheet.Cells[bsrn + 3, 5].Value = patDetails.LastName;
            worksheet.Cells[bsrn + 3, 6].Value = model.Quest2Navigation.Sex != null ? model.Quest2Navigation.Sex == true ? _sharedLocalizer["Male"].Value : _sharedLocalizer["Female"].Value : "";

            var dateofbirth = model.Quest2Navigation.Dob.HasValue ? model.Quest2Navigation.Dob.Value.ToShortDateString() : string.Empty;
            if (dateofbirth != null && dateofbirth != string.Empty) { worksheet.Cells[bsrn + 3, 7].Value = CalculateAge(Convert.ToDateTime(dateofbirth)); }
            worksheet.Cells[bsrn + 3, 8].Value = dateofbirth;
            worksheet.Cells[bsrn + 3, 9].Value = _country.GetAllCounties().Results.Where(o => o.Id == model.Quest1Navigation.CountryId).Select(o => o.CountryName).FirstOrDefault();
            worksheet.Cells[bsrn + 3, 10].Value = patDetails.IsConsentFcheckByAdmin == true ? _sharedLocalizer["Approved"].Value : _sharedLocalizer["Pending Approval"].Value;
            worksheet.Cells[bsrn + 3, 11].Value = model.IsConfirmedByAdmin == true ? _sharedLocalizer["Approved"].Value : _sharedLocalizer["Pending Approval"].Value;

            worksheet.Cells[bsrn + 3, 12].Value = _sharedLocalizer["BST31"].Value + " 1";
            worksheet.Cells[bsrn + 3, 13].Value = model.Quest2Navigation.Mh1anySignificant;
            worksheet.Cells[bsrn + 3, 14].Value = model.Quest2Navigation.Mh1startDate.HasValue ? model.Quest2Navigation.Mh1startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 3, 15].Value = model.Quest2Navigation.Mh1stopDate.HasValue ? model.Quest2Navigation.Mh1stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 3, 16].Value = model.Quest2Navigation.Mh1ongoing != null ? model.Quest2Navigation.Mh1ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 4, 12].Value = _sharedLocalizer["BST31"].Value + " 2";
            worksheet.Cells[bsrn + 4, 13].Value = model.Quest2Navigation.Mh2anySignificant;
            worksheet.Cells[bsrn + 4, 14].Value = model.Quest2Navigation.Mh2startDate.HasValue ? model.Quest2Navigation.Mh2startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 4, 15].Value = model.Quest2Navigation.Mh2stopDate.HasValue ? model.Quest2Navigation.Mh2stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 4, 16].Value = model.Quest2Navigation.Mh2ongoing != null ? model.Quest2Navigation.Mh2ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 5, 12].Value = _sharedLocalizer["BST31"].Value + " 3";
            worksheet.Cells[bsrn + 5, 13].Value = model.Quest2Navigation.Mh3anySignificant;
            worksheet.Cells[bsrn + 5, 14].Value = model.Quest2Navigation.Mh3startDate.HasValue ? model.Quest2Navigation.Mh3startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 5, 15].Value = model.Quest2Navigation.Mh3stopDate.HasValue ? model.Quest2Navigation.Mh3stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 5, 16].Value = model.Quest2Navigation.Mh3ongoing != null ? model.Quest2Navigation.Mh3ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 6, 12].Value = _sharedLocalizer["BST31"].Value + " 4";
            worksheet.Cells[bsrn + 6, 13].Value = model.Quest2Navigation.Mh4anySignificant;
            worksheet.Cells[bsrn + 6, 14].Value = model.Quest2Navigation.Mh4startDate.HasValue ? model.Quest2Navigation.Mh4startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 6, 15].Value = model.Quest2Navigation.Mh4stopDate.HasValue ? model.Quest2Navigation.Mh4stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 6, 16].Value = model.Quest2Navigation.Mh4ongoing != null ? model.Quest2Navigation.Mh4ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 3, 17].Value = _sharedLocalizer["BST38"].Value + " 1";
            worksheet.Cells[bsrn + 3, 18].Value = model.Quest2Navigation.M1medication;
            worksheet.Cells[bsrn + 3, 19].Value = model.Quest2Navigation.M1indication;
            worksheet.Cells[bsrn + 3, 20].Value = model.Quest2Navigation.M1dosageUnits;
            worksheet.Cells[bsrn + 3, 21].Value = model.Quest2Navigation.M1frequency;
            worksheet.Cells[bsrn + 3, 22].Value = model.Quest2Navigation.M1route;
            worksheet.Cells[bsrn + 3, 23].Value = model.Quest2Navigation.M1startDate.HasValue ? model.Quest2Navigation.M1startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 3, 24].Value = model.Quest2Navigation.M1stopDate.HasValue ? model.Quest2Navigation.M1stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 3, 25].Value = model.Quest2Navigation.M1ongoing != null ? model.Quest2Navigation.M1ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 4, 17].Value = _sharedLocalizer["BST38"].Value + " 2";
            worksheet.Cells[bsrn + 4, 18].Value = model.Quest2Navigation.M2medication;
            worksheet.Cells[bsrn + 4, 19].Value = model.Quest2Navigation.M2indication;
            worksheet.Cells[bsrn + 4, 20].Value = model.Quest2Navigation.M2dosageUnits;
            worksheet.Cells[bsrn + 4, 21].Value = model.Quest2Navigation.M2frequency;
            worksheet.Cells[bsrn + 4, 22].Value = model.Quest2Navigation.M2route;
            worksheet.Cells[bsrn + 4, 23].Value = model.Quest2Navigation.M2startDate.HasValue ? model.Quest2Navigation.M2startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 4, 24].Value = model.Quest2Navigation.M2stopDate.HasValue ? model.Quest2Navigation.M2stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 4, 25].Value = model.Quest2Navigation.M2ongoing != null ? model.Quest2Navigation.M2ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 5, 17].Value = _sharedLocalizer["BST38"].Value + " 3";
            worksheet.Cells[bsrn + 5, 18].Value = model.Quest2Navigation.M3medication;
            worksheet.Cells[bsrn + 5, 19].Value = model.Quest2Navigation.M3indication;
            worksheet.Cells[bsrn + 5, 20].Value = model.Quest2Navigation.M3dosageUnits;
            worksheet.Cells[bsrn + 5, 21].Value = model.Quest2Navigation.M3frequency;
            worksheet.Cells[bsrn + 5, 22].Value = model.Quest2Navigation.M3route;
            worksheet.Cells[bsrn + 5, 23].Value = model.Quest2Navigation.M3startDate.HasValue ? model.Quest2Navigation.M3startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 5, 24].Value = model.Quest2Navigation.M3stopDate.HasValue ? model.Quest2Navigation.M3stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 5, 25].Value = model.Quest2Navigation.M3ongoing != null ? model.Quest2Navigation.M3ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 6, 17].Value = _sharedLocalizer["BST38"].Value + " 4";
            worksheet.Cells[bsrn + 6, 18].Value = model.Quest2Navigation.M4medication;
            worksheet.Cells[bsrn + 6, 19].Value = model.Quest2Navigation.M4indication;
            worksheet.Cells[bsrn + 6, 20].Value = model.Quest2Navigation.M4dosageUnits;
            worksheet.Cells[bsrn + 6, 21].Value = model.Quest2Navigation.M4frequency;
            worksheet.Cells[bsrn + 6, 22].Value = model.Quest2Navigation.M4route;
            worksheet.Cells[bsrn + 6, 23].Value = model.Quest2Navigation.M4startDate.HasValue ? model.Quest2Navigation.M4startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 6, 24].Value = model.Quest2Navigation.M4stopDate.HasValue ? model.Quest2Navigation.M4stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 6, 25].Value = model.Quest2Navigation.M4ongoing != null ? model.Quest2Navigation.M4ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 7, 17].Value = _sharedLocalizer["BST38"].Value + " 5";
            worksheet.Cells[bsrn + 7, 18].Value = model.Quest2Navigation.M5medication;
            worksheet.Cells[bsrn + 7, 19].Value = model.Quest2Navigation.M5indication;
            worksheet.Cells[bsrn + 7, 20].Value = model.Quest2Navigation.M5dosageUnits;
            worksheet.Cells[bsrn + 7, 21].Value = model.Quest2Navigation.M5frequency;
            worksheet.Cells[bsrn + 7, 22].Value = model.Quest2Navigation.M5route;
            worksheet.Cells[bsrn + 7, 23].Value = model.Quest2Navigation.M5startDate.HasValue ? model.Quest2Navigation.M5startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 7, 24].Value = model.Quest2Navigation.M5stopDate.HasValue ? model.Quest2Navigation.M5stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 7, 25].Value = model.Quest2Navigation.M5ongoing != null ? model.Quest2Navigation.M5ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 8, 17].Value = _sharedLocalizer["BST38"].Value + " 6";
            worksheet.Cells[bsrn + 8, 18].Value = model.Quest2Navigation.M6medication;
            worksheet.Cells[bsrn + 8, 19].Value = model.Quest2Navigation.M6indication;
            worksheet.Cells[bsrn + 8, 20].Value = model.Quest2Navigation.M6dosageUnits;
            worksheet.Cells[bsrn + 8, 21].Value = model.Quest2Navigation.M6frequency;
            worksheet.Cells[bsrn + 8, 22].Value = model.Quest2Navigation.M6route;
            worksheet.Cells[bsrn + 8, 23].Value = model.Quest2Navigation.M6startDate.HasValue ? model.Quest2Navigation.M6startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 8, 24].Value = model.Quest2Navigation.M6stopDate.HasValue ? model.Quest2Navigation.M6stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 8, 25].Value = model.Quest2Navigation.M6ongoing != null ? model.Quest2Navigation.M6ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 9, 17].Value = _sharedLocalizer["BST38"].Value + " 7";
            worksheet.Cells[bsrn + 9, 18].Value = model.Quest2Navigation.M7medication;
            worksheet.Cells[bsrn + 9, 19].Value = model.Quest2Navigation.M7indication;
            worksheet.Cells[bsrn + 9, 20].Value = model.Quest2Navigation.M7dosageUnits;
            worksheet.Cells[bsrn + 9, 21].Value = model.Quest2Navigation.M7frequency;
            worksheet.Cells[bsrn + 9, 22].Value = model.Quest2Navigation.M7route;
            worksheet.Cells[bsrn + 9, 23].Value = model.Quest2Navigation.M7startDate.HasValue ? model.Quest2Navigation.M7startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 9, 24].Value = model.Quest2Navigation.M7stopDate.HasValue ? model.Quest2Navigation.M7stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 9, 25].Value = model.Quest2Navigation.M7ongoing != null ? model.Quest2Navigation.M7ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 10, 17].Value = _sharedLocalizer["BST38"].Value + " 8";
            worksheet.Cells[bsrn + 10, 18].Value = model.Quest2Navigation.M8medication;
            worksheet.Cells[bsrn + 10, 19].Value = model.Quest2Navigation.M8indication;
            worksheet.Cells[bsrn + 10, 20].Value = model.Quest2Navigation.M8dosageUnits;
            worksheet.Cells[bsrn + 10, 21].Value = model.Quest2Navigation.M8frequency;
            worksheet.Cells[bsrn + 10, 22].Value = model.Quest2Navigation.M8route;
            worksheet.Cells[bsrn + 10, 23].Value = model.Quest2Navigation.M8startDate.HasValue ? model.Quest2Navigation.M8startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 10, 24].Value = model.Quest2Navigation.M8stopDate.HasValue ? model.Quest2Navigation.M8stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 10, 25].Value = model.Quest2Navigation.M8ongoing != null ? model.Quest2Navigation.M8ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 11, 17].Value = _sharedLocalizer["BST38"].Value + " 9";
            worksheet.Cells[bsrn + 11, 18].Value = model.Quest2Navigation.M9medication;
            worksheet.Cells[bsrn + 11, 19].Value = model.Quest2Navigation.M9indication;
            worksheet.Cells[bsrn + 11, 20].Value = model.Quest2Navigation.M9dosageUnits;
            worksheet.Cells[bsrn + 11, 21].Value = model.Quest2Navigation.M9frequency;
            worksheet.Cells[bsrn + 11, 22].Value = model.Quest2Navigation.M9route;
            worksheet.Cells[bsrn + 11, 23].Value = model.Quest2Navigation.M9startDate.HasValue ? model.Quest2Navigation.M9startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 11, 24].Value = model.Quest2Navigation.M9stopDate.HasValue ? model.Quest2Navigation.M9stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 11, 25].Value = model.Quest2Navigation.M9ongoing != null ? model.Quest2Navigation.M9ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 12, 17].Value = _sharedLocalizer["BST38"].Value + " 10";
            worksheet.Cells[bsrn + 12, 18].Value = model.Quest2Navigation.M10medication;
            worksheet.Cells[bsrn + 12, 19].Value = model.Quest2Navigation.M10indication;
            worksheet.Cells[bsrn + 12, 20].Value = model.Quest2Navigation.M10dosageUnits;
            worksheet.Cells[bsrn + 12, 21].Value = model.Quest2Navigation.M10frequency;
            worksheet.Cells[bsrn + 12, 22].Value = model.Quest2Navigation.M10route;
            worksheet.Cells[bsrn + 12, 23].Value = model.Quest2Navigation.M10startDate.HasValue ? model.Quest2Navigation.M10startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 12, 24].Value = model.Quest2Navigation.M10stopDate.HasValue ? model.Quest2Navigation.M10stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 12, 25].Value = model.Quest2Navigation.M10ongoing != null ? model.Quest2Navigation.M10ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 13, 17].Value = _sharedLocalizer["BST38"].Value + " 11";
            worksheet.Cells[bsrn + 13, 18].Value = model.Quest2Navigation.M11medication;
            worksheet.Cells[bsrn + 13, 19].Value = model.Quest2Navigation.M11indication;
            worksheet.Cells[bsrn + 13, 20].Value = model.Quest2Navigation.M11dosageUnits;
            worksheet.Cells[bsrn + 13, 21].Value = model.Quest2Navigation.M11frequency;
            worksheet.Cells[bsrn + 13, 22].Value = model.Quest2Navigation.M11route;
            worksheet.Cells[bsrn + 13, 23].Value = model.Quest2Navigation.M11startDate.HasValue ? model.Quest2Navigation.M11startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 13, 24].Value = model.Quest2Navigation.M11stopDate.HasValue ? model.Quest2Navigation.M11stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 13, 25].Value = model.Quest2Navigation.M11ongoing != null ? model.Quest2Navigation.M11ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 14, 17].Value = _sharedLocalizer["BST38"].Value + " 12";
            worksheet.Cells[bsrn + 14, 18].Value = model.Quest2Navigation.M12medication;
            worksheet.Cells[bsrn + 14, 19].Value = model.Quest2Navigation.M12indication;
            worksheet.Cells[bsrn + 14, 20].Value = model.Quest2Navigation.M12dosageUnits;
            worksheet.Cells[bsrn + 14, 21].Value = model.Quest2Navigation.M12frequency;
            worksheet.Cells[bsrn + 14, 22].Value = model.Quest2Navigation.M12route;
            worksheet.Cells[bsrn + 14, 23].Value = model.Quest2Navigation.M12startDate.HasValue ? model.Quest2Navigation.M12startDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 14, 24].Value = model.Quest2Navigation.M12stopDate.HasValue ? model.Quest2Navigation.M12stopDate.Value.ToShortDateString() : string.Empty;
            worksheet.Cells[bsrn + 14, 25].Value = model.Quest2Navigation.M12ongoing != null ? model.Quest2Navigation.M12ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

            worksheet.Cells[bsrn + 3, 26].Value = _sharedLocalizer["BST49"].Value;
            worksheet.Cells[bsrn + 3, 27].Value = model.Quest2Navigation.HaematologyA;
            worksheet.Cells[bsrn + 3, 28].Value = model.Quest2Navigation.HaematologyC;

            worksheet.Cells[bsrn + 4, 26].Value = _sharedLocalizer["BST50"].Value;
            worksheet.Cells[bsrn + 4, 27].Value = model.Quest2Navigation.BiochemistryA;
            worksheet.Cells[bsrn + 4, 28].Value = model.Quest2Navigation.BiochemistryC;

            worksheet.Cells[bsrn + 5, 26].Value = _sharedLocalizer["BST51"].Value;
            worksheet.Cells[bsrn + 5, 27].Value = model.Quest2Navigation.UreaA;
            worksheet.Cells[bsrn + 5, 28].Value = model.Quest2Navigation.UreaC;

            worksheet.Cells[bsrn + 6, 26].Value = _sharedLocalizer["BST52"].Value;
            worksheet.Cells[bsrn + 6, 27].Value = model.Quest2Navigation.CreatinineA;
            worksheet.Cells[bsrn + 6, 28].Value = model.Quest2Navigation.CreatinineC;

            worksheet.Cells[bsrn + 7, 26].Value = _sharedLocalizer["BST53"].Value;
            worksheet.Cells[bsrn + 7, 27].Value = model.Quest2Navigation.PhosphateA;
            worksheet.Cells[bsrn + 7, 28].Value = model.Quest2Navigation.PhosphateC;

            worksheet.Cells[bsrn + 8, 26].Value = _sharedLocalizer["BST54"].Value;
            worksheet.Cells[bsrn + 8, 27].Value = model.Quest2Navigation.UricAcidA;
            worksheet.Cells[bsrn + 8, 28].Value = model.Quest2Navigation.UricAcidC;

            worksheet.Cells[bsrn + 9, 26].Value = _sharedLocalizer["BST55"].Value;
            worksheet.Cells[bsrn + 9, 27].Value = model.Quest2Navigation.BicarbonateA;
            worksheet.Cells[bsrn + 9, 28].Value = model.Quest2Navigation.BicarbonateC;

            worksheet.Cells[bsrn + 10, 26].Value = _sharedLocalizer["BST56"].Value;
            worksheet.Cells[bsrn + 10, 27].Value = model.Quest2Navigation.UrineAnalysisA;
            worksheet.Cells[bsrn + 10, 28].Value = model.Quest2Navigation.UrineAnalysisC;

            worksheet.Cells[bsrn + 11, 26].Value = _sharedLocalizer["BST57"].Value;
            worksheet.Cells[bsrn + 11, 27].Value = model.Quest2Navigation.SerologyA;
            worksheet.Cells[bsrn + 11, 28].Value = model.Quest2Navigation.SerologyC;

            worksheet.Cells[bsrn + 12, 26].Value = _sharedLocalizer["BST58"].Value + " : " + model.Quest2Navigation.OthersSpecify;
            worksheet.Cells[bsrn + 12, 27].Value = model.Quest2Navigation.OthersSpecifyA;
            worksheet.Cells[bsrn + 12, 28].Value = model.Quest2Navigation.OthersSpecifyC;

            worksheet.Cells[bsrn + 13, 26].Value = _sharedLocalizer["BST59"].Value;
            worksheet.Cells[bsrn + 13, 27].Value = model.Quest2Navigation.Comments;


            worksheet.Cells[bsrn + 3, 29].Value = model.Quest3Navigation.Indication;
            worksheet.Cells[bsrn + 3, 30].Value = model.Quest3Navigation.Dose;
            worksheet.Cells[bsrn + 3, 31].Value = model.Quest3Navigation.Frequency;
            worksheet.Cells[bsrn + 3, 32].Value = model.Quest3Navigation.Route;
            worksheet.Cells[bsrn + 3, 33].Value = model.Quest3Navigation.Remarks;

            return bsrn + 14;
        }

        public int FollowUpFormSheet(ExcelWorksheet worksheet, int fsp, BaselineDataMaster model)
        {
            //FollowUp
            //int fsp = bsrn + 16;
            var fufModel = _db.PatientFollowUpForm.Where(o => o.PatientId == model.PatientId && o.IsDeleted == false).ToList();

            int startPoint = fsp + 4;
            int increment = 12;
            int value = 0;

            worksheet.Cells[fsp, 10].Value = _sharedLocalizer["Follow Up Date"].Value; worksheet.Cells[fsp, 10].Style.Font.Bold = true;
            worksheet.Cells[fsp, 11].Value = _sharedLocalizer["Adverse Events"].Value; worksheet.Cells[fsp, 11].Style.Font.Bold = true;
            worksheet.Select("K17:R17");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Adverse Events"].Value;

            //worksheet.Cells[fsp, 19].Value = "Medical History (Illnesses, Conditions, Surgery)"; worksheet.Cells[fsp, 19].Style.Font.Bold = true;
            worksheet.Select("S17:X17");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Medical History (Illnesses, Conditions, Surgery)"].Value;

            //worksheet.Cells[fsp, 25].Value = "Concomitant Medications"; worksheet.Cells[fsp, 25].Style.Font.Bold = true;
            worksheet.Select("Y17:AG17");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Concomitant Medications"].Value;

            //worksheet.Cells[fsp, 34].Value = "Laboratory Investigations (If Any)"; worksheet.Cells[fsp, 34].Style.Font.Bold = true;
            worksheet.Select("AH17:AJ17");
            worksheet.SelectedRange.Merge = true; worksheet.SelectedRange.Style.Font.Bold = true;
            worksheet.SelectedRange.Value = _sharedLocalizer["Laboratory Investigations (If Any)"].Value;

            worksheet.Cells[fsp + 1, 11].Value = _sharedLocalizer["Adverse Events"].Value; worksheet.Cells[fsp + 1, 11].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 12].Value = _sharedLocalizer["Event Term (Diagnosis if known)"].Value; worksheet.Cells[fsp + 1, 12].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 13].Value = _sharedLocalizer["Start Date"].Value; worksheet.Cells[fsp + 1, 13].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 14].Value = _sharedLocalizer["Stop Date"].Value; worksheet.Cells[fsp + 1, 14].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 15].Value = _sharedLocalizer["SAE"].Value; worksheet.Cells[fsp + 1, 15].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 16].Value = _sharedLocalizer["Con-comitant Medication given"].Value; worksheet.Cells[fsp + 1, 16].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 17].Value = _sharedLocalizer["Study Drug Action"].Value; worksheet.Cells[fsp + 1, 17].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 18].Value = _sharedLocalizer["Outcome"].Value; worksheet.Cells[fsp + 1, 18].Style.Font.Bold = true;
            worksheet.Cells[fsp + 1, 19].Value = _sharedLocalizer["Relationship to Study Drug"].Value; worksheet.Cells[fsp + 1, 19].Style.Font.Bold = true;

            worksheet.Cells[fsp + 2, 20].Value = _sharedLocalizer["Medical History"].Value; worksheet.Cells[fsp + 2, 20].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 21].Value = _sharedLocalizer["Please specify any significant/major illness, medical conditions or surgery in the past or at present."].Value; worksheet.Cells[fsp + 2, 21].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 22].Value = _sharedLocalizer["Start Date"].Value; worksheet.Cells[fsp + 2, 22].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 23].Value = _sharedLocalizer["Stop Date"].Value; worksheet.Cells[fsp + 2, 23].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 24].Value = _sharedLocalizer["Or tick if Ongoing?"].Value; worksheet.Cells[fsp + 2, 24].Style.Font.Bold = true;

            worksheet.Cells[fsp + 2, 25].Value = _sharedLocalizer["Medications"].Value; worksheet.Cells[fsp + 2, 25].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 26].Value = _sharedLocalizer["Medication (Record Generic or trade name)"].Value; worksheet.Cells[fsp + 2, 26].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 27].Value = _sharedLocalizer["Reason for use (Medical History diagnosis or other reason, e.g. Prophylaxis)"].Value; worksheet.Cells[fsp + 2, 27].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 28].Value = _sharedLocalizer["Dosage and units"].Value; worksheet.Cells[fsp + 2, 28].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 29].Value = _sharedLocalizer["Frequency"].Value; worksheet.Cells[fsp + 2, 29].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 30].Value = _sharedLocalizer["Route"].Value; worksheet.Cells[fsp + 2, 30].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 31].Value = _sharedLocalizer["Start Date"].Value; worksheet.Cells[fsp + 2, 31].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 32].Value = _sharedLocalizer["Stop Date"].Value; worksheet.Cells[fsp + 2, 32].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 33].Value = _sharedLocalizer["Or tick if on going at Screening Visit"].Value; worksheet.Cells[fsp + 2, 33].Style.Font.Bold = true;

            worksheet.Cells[fsp + 2, 34].Value = _sharedLocalizer["Specification"].Value; worksheet.Cells[fsp + 2, 34].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 35].Value = _sharedLocalizer["Abnormal parameters, if any"].Value; worksheet.Cells[fsp + 2, 35].Style.Font.Bold = true;
            worksheet.Cells[fsp + 2, 36].Value = _sharedLocalizer["Comments"].Value; worksheet.Cells[fsp + 2, 36].Style.Font.Bold = true;

            if (fufModel.Count() != 0)
            {
                for (int i = 0; i < fufModel.Count(); i++)
                {

                    value = startPoint + (increment * i);
                    worksheet.Cells[value, 10].Value = fufModel[i].Date.HasValue ? fufModel[i].Date.Value.ToShortDateString() : string.Empty;
                    var fufForm = _followupform.FindFufquestionnairesResult(fufModel[i].FollowUpFormId);

                    worksheet.Cells[value, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 1";
                    worksheet.Cells[value, 12].Value = fufForm.Fufquest1Navigation.EventTerm1;
                    worksheet.Cells[value, 13].Value = fufForm.Fufquest1Navigation.StartDate1.HasValue ? fufForm.Fufquest1Navigation.StartDate1.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 14].Value = fufForm.Fufquest1Navigation.StopDate1.HasValue ? fufForm.Fufquest1Navigation.StopDate1.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 15].Value = fufForm.Fufquest1Navigation.SaeId1;
                    worksheet.Cells[value, 16].Value = fufForm.Fufquest1Navigation.ComMedId1;
                    worksheet.Cells[value, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid1, "SDA");
                    worksheet.Cells[value, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId1, "OUT");
                    worksheet.Cells[value, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId1, "RSD");

                    worksheet.Cells[value + 1, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 2";
                    worksheet.Cells[value + 1, 12].Value = fufForm.Fufquest1Navigation.EventTerm2;
                    worksheet.Cells[value + 1, 13].Value = fufForm.Fufquest1Navigation.StartDate2.HasValue ? fufForm.Fufquest1Navigation.StartDate2.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 14].Value = fufForm.Fufquest1Navigation.StopDate2.HasValue ? fufForm.Fufquest1Navigation.StopDate2.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 15].Value = fufForm.Fufquest1Navigation.SaeId2;
                    worksheet.Cells[value + 1, 16].Value = fufForm.Fufquest1Navigation.ComMedId2;
                    worksheet.Cells[value + 1, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid2, "SDA");
                    worksheet.Cells[value + 1, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId2, "OUT");
                    worksheet.Cells[value + 1, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId2, "RSD");

                    worksheet.Cells[value + 2, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 3";
                    worksheet.Cells[value + 2, 12].Value = fufForm.Fufquest1Navigation.EventTerm3;
                    worksheet.Cells[value + 2, 13].Value = fufForm.Fufquest1Navigation.StartDate3.HasValue ? fufForm.Fufquest1Navigation.StartDate3.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 14].Value = fufForm.Fufquest1Navigation.StopDate3.HasValue ? fufForm.Fufquest1Navigation.StopDate3.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 15].Value = fufForm.Fufquest1Navigation.SaeId3;
                    worksheet.Cells[value + 2, 16].Value = fufForm.Fufquest1Navigation.ComMedId3;
                    worksheet.Cells[value + 2, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid3, "SDA");
                    worksheet.Cells[value + 2, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId3, "OUT");
                    worksheet.Cells[value + 2, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId3, "RSD");

                    worksheet.Cells[value + 3, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 4";
                    worksheet.Cells[value + 3, 12].Value = fufForm.Fufquest1Navigation.EventTerm4;
                    worksheet.Cells[value + 3, 13].Value = fufForm.Fufquest1Navigation.StartDate4.HasValue ? fufForm.Fufquest1Navigation.StartDate4.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 14].Value = fufForm.Fufquest1Navigation.StopDate4.HasValue ? fufForm.Fufquest1Navigation.StopDate4.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 15].Value = fufForm.Fufquest1Navigation.SaeId4;
                    worksheet.Cells[value + 3, 16].Value = fufForm.Fufquest1Navigation.ComMedId4;
                    worksheet.Cells[value + 3, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid4, "SDA");
                    worksheet.Cells[value + 3, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId4, "OUT");
                    worksheet.Cells[value + 3, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId4, "RSD");

                    worksheet.Cells[value + 4, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 5";
                    worksheet.Cells[value + 4, 12].Value = fufForm.Fufquest1Navigation.EventTerm5;
                    worksheet.Cells[value + 4, 13].Value = fufForm.Fufquest1Navigation.StartDate5.HasValue ? fufForm.Fufquest1Navigation.StartDate5.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 14].Value = fufForm.Fufquest1Navigation.StopDate5.HasValue ? fufForm.Fufquest1Navigation.StopDate5.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 15].Value = fufForm.Fufquest1Navigation.SaeId5;
                    worksheet.Cells[value + 4, 16].Value = fufForm.Fufquest1Navigation.ComMedId5;
                    worksheet.Cells[value + 4, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid5, "SDA");
                    worksheet.Cells[value + 4, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId5, "OUT");
                    worksheet.Cells[value + 4, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId5, "RSD");

                    worksheet.Cells[value + 5, 11].Value = _sharedLocalizer["Adverse Events"].Value + " 6";
                    worksheet.Cells[value + 5, 12].Value = fufForm.Fufquest1Navigation.EventTerm6;
                    worksheet.Cells[value + 5, 13].Value = fufForm.Fufquest1Navigation.StartDate6.HasValue ? fufForm.Fufquest1Navigation.StartDate6.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 14].Value = fufForm.Fufquest1Navigation.StopDate6.HasValue ? fufForm.Fufquest1Navigation.StopDate6.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 15].Value = fufForm.Fufquest1Navigation.SaeId6;
                    worksheet.Cells[value + 5, 16].Value = fufForm.Fufquest1Navigation.ComMedId6;
                    worksheet.Cells[value + 5, 17].Value = GetValue(fufForm.Fufquest1Navigation.StudyDaid6, "SDA");
                    worksheet.Cells[value + 5, 18].Value = GetValue(fufForm.Fufquest1Navigation.OutcomeId6, "OUT");
                    worksheet.Cells[value + 5, 19].Value = GetValue(fufForm.Fufquest1Navigation.RelaStudyId6, "RSD");

                    worksheet.Cells[value, 20].Value = _sharedLocalizer["Medical History"].Value + " 1";
                    worksheet.Cells[value, 21].Value = fufForm.Fufquest5Navigation.C1condition;
                    worksheet.Cells[value, 22].Value = fufForm.Fufquest5Navigation.C1startDate.HasValue ? fufForm.Fufquest5Navigation.C1startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 23].Value = fufForm.Fufquest5Navigation.C1stopDate.HasValue ? fufForm.Fufquest5Navigation.C1stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 24].Value = fufForm.Fufquest5Navigation.C1ongoing != null ? fufForm.Fufquest5Navigation.C1ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 1, 20].Value = _sharedLocalizer["Medical History"].Value + " 2";
                    worksheet.Cells[value + 1, 21].Value = fufForm.Fufquest5Navigation.C2condition;
                    worksheet.Cells[value + 1, 22].Value = fufForm.Fufquest5Navigation.C2startDate.HasValue ? fufForm.Fufquest5Navigation.C2startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 23].Value = fufForm.Fufquest5Navigation.C2stopDate.HasValue ? fufForm.Fufquest5Navigation.C2stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 24].Value = fufForm.Fufquest5Navigation.C2ongoing != null ? fufForm.Fufquest5Navigation.C2ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 2, 20].Value = _sharedLocalizer["Medical History"].Value + " 3";
                    worksheet.Cells[value + 2, 21].Value = fufForm.Fufquest5Navigation.C3condition;
                    worksheet.Cells[value + 2, 22].Value = fufForm.Fufquest5Navigation.C3startDate.HasValue ? fufForm.Fufquest5Navigation.C3startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 23].Value = fufForm.Fufquest5Navigation.C3stopDate.HasValue ? fufForm.Fufquest5Navigation.C3stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 24].Value = fufForm.Fufquest5Navigation.C3ongoing != null ? fufForm.Fufquest5Navigation.C3ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 3, 20].Value = _sharedLocalizer["Medical History"].Value + " 4";
                    worksheet.Cells[value + 3, 21].Value = fufForm.Fufquest5Navigation.C4condition;
                    worksheet.Cells[value + 3, 22].Value = fufForm.Fufquest5Navigation.C4startDate.HasValue ? fufForm.Fufquest5Navigation.C4startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 23].Value = fufForm.Fufquest5Navigation.C4stopDate.HasValue ? fufForm.Fufquest5Navigation.C4stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 24].Value = fufForm.Fufquest5Navigation.C4ongoing != null ? fufForm.Fufquest5Navigation.C4ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 4, 20].Value = _sharedLocalizer["Medical History"].Value + " 5";
                    worksheet.Cells[value + 4, 21].Value = fufForm.Fufquest5Navigation.C5condition;
                    worksheet.Cells[value + 4, 22].Value = fufForm.Fufquest5Navigation.C5startDate.HasValue ? fufForm.Fufquest5Navigation.C5startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 23].Value = fufForm.Fufquest5Navigation.C5stopDate.HasValue ? fufForm.Fufquest5Navigation.C5stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 24].Value = fufForm.Fufquest5Navigation.C5ongoing != null ? fufForm.Fufquest5Navigation.C5ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 5, 20].Value = _sharedLocalizer["Medical History"].Value + " 6";
                    worksheet.Cells[value + 5, 21].Value = fufForm.Fufquest5Navigation.C6condition;
                    worksheet.Cells[value + 5, 22].Value = fufForm.Fufquest5Navigation.C6startDate.HasValue ? fufForm.Fufquest5Navigation.C6startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 23].Value = fufForm.Fufquest5Navigation.C6stopDate.HasValue ? fufForm.Fufquest5Navigation.C6stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 24].Value = fufForm.Fufquest5Navigation.C6ongoing != null ? fufForm.Fufquest5Navigation.C6ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 6, 20].Value = _sharedLocalizer["Medical History"].Value + " 7";
                    worksheet.Cells[value + 6, 21].Value = fufForm.Fufquest5Navigation.C7condition;
                    worksheet.Cells[value + 6, 22].Value = fufForm.Fufquest5Navigation.C7startDate.HasValue ? fufForm.Fufquest5Navigation.C7startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 6, 23].Value = fufForm.Fufquest5Navigation.C7stopDate.HasValue ? fufForm.Fufquest5Navigation.C7stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 6, 24].Value = fufForm.Fufquest5Navigation.C7ongoing != null ? fufForm.Fufquest5Navigation.C7ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 7, 20].Value = _sharedLocalizer["Medical History"].Value + " 8";
                    worksheet.Cells[value + 7, 21].Value = fufForm.Fufquest5Navigation.C8condition;
                    worksheet.Cells[value + 7, 22].Value = fufForm.Fufquest5Navigation.C8startDate.HasValue ? fufForm.Fufquest5Navigation.C8startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 7, 23].Value = fufForm.Fufquest5Navigation.C8stopDate.HasValue ? fufForm.Fufquest5Navigation.C8stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 7, 24].Value = fufForm.Fufquest5Navigation.C8ongoing != null ? fufForm.Fufquest5Navigation.C8ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 8, 20].Value = _sharedLocalizer["Medical History"].Value + " 9";
                    worksheet.Cells[value + 8, 21].Value = fufForm.Fufquest5Navigation.C9condition;
                    worksheet.Cells[value + 8, 22].Value = fufForm.Fufquest5Navigation.C9startDate.HasValue ? fufForm.Fufquest5Navigation.C9startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 8, 23].Value = fufForm.Fufquest5Navigation.C9stopDate.HasValue ? fufForm.Fufquest5Navigation.C9stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 8, 24].Value = fufForm.Fufquest5Navigation.C9ongoing != null ? fufForm.Fufquest5Navigation.C9ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value + 9, 20].Value = _sharedLocalizer["Medical History"].Value + " 10";
                    worksheet.Cells[value + 9, 21].Value = fufForm.Fufquest5Navigation.C10condition;
                    worksheet.Cells[value + 9, 22].Value = fufForm.Fufquest5Navigation.C10startDate.HasValue ? fufForm.Fufquest5Navigation.C10startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 9, 23].Value = fufForm.Fufquest5Navigation.C10stopDate.HasValue ? fufForm.Fufquest5Navigation.C10stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 9, 24].Value = fufForm.Fufquest5Navigation.C10ongoing != null ? fufForm.Fufquest5Navigation.C10ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value : "";

                    worksheet.Cells[value, 25].Value = _sharedLocalizer["Medications"].Value + " 1";
                    worksheet.Cells[value, 26].Value = fufForm.Fufquest6Navigation.M1medication;
                    worksheet.Cells[value, 27].Value = fufForm.Fufquest6Navigation.M1indication;
                    worksheet.Cells[value, 28].Value = fufForm.Fufquest6Navigation.M1dosageUnits;
                    worksheet.Cells[value, 29].Value = fufForm.Fufquest6Navigation.M1frequency;
                    worksheet.Cells[value, 30].Value = fufForm.Fufquest6Navigation.M1route;
                    worksheet.Cells[value, 31].Value = fufForm.Fufquest6Navigation.M1startDate.HasValue ? fufForm.Fufquest6Navigation.M1startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 32].Value = fufForm.Fufquest6Navigation.M1stopDate.HasValue ? fufForm.Fufquest6Navigation.M1stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value, 33].Value = fufForm.Fufquest6Navigation.M1ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 1, 25].Value = _sharedLocalizer["Medications"].Value + " 2";
                    worksheet.Cells[value + 1, 26].Value = fufForm.Fufquest6Navigation.M2medication;
                    worksheet.Cells[value + 1, 27].Value = fufForm.Fufquest6Navigation.M2indication;
                    worksheet.Cells[value + 1, 28].Value = fufForm.Fufquest6Navigation.M2dosageUnits;
                    worksheet.Cells[value + 1, 29].Value = fufForm.Fufquest6Navigation.M2frequency;
                    worksheet.Cells[value + 1, 30].Value = fufForm.Fufquest6Navigation.M2route;
                    worksheet.Cells[value + 1, 31].Value = fufForm.Fufquest6Navigation.M2startDate.HasValue ? fufForm.Fufquest6Navigation.M2startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 32].Value = fufForm.Fufquest6Navigation.M2stopDate.HasValue ? fufForm.Fufquest6Navigation.M2stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 1, 33].Value = fufForm.Fufquest6Navigation.M2ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 2, 25].Value = _sharedLocalizer["Medications"].Value + " 3";
                    worksheet.Cells[value + 2, 26].Value = fufForm.Fufquest6Navigation.M3medication;
                    worksheet.Cells[value + 2, 27].Value = fufForm.Fufquest6Navigation.M3indication;
                    worksheet.Cells[value + 2, 28].Value = fufForm.Fufquest6Navigation.M3dosageUnits;
                    worksheet.Cells[value + 2, 29].Value = fufForm.Fufquest6Navigation.M3frequency;
                    worksheet.Cells[value + 2, 30].Value = fufForm.Fufquest6Navigation.M3route;
                    worksheet.Cells[value + 2, 31].Value = fufForm.Fufquest6Navigation.M3startDate.HasValue ? fufForm.Fufquest6Navigation.M3startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 32].Value = fufForm.Fufquest6Navigation.M3stopDate.HasValue ? fufForm.Fufquest6Navigation.M3stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 2, 33].Value = fufForm.Fufquest6Navigation.M3ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 3, 25].Value = _sharedLocalizer["Medications"].Value + " 4";
                    worksheet.Cells[value + 3, 26].Value = fufForm.Fufquest6Navigation.M4medication;
                    worksheet.Cells[value + 3, 27].Value = fufForm.Fufquest6Navigation.M4indication;
                    worksheet.Cells[value + 3, 28].Value = fufForm.Fufquest6Navigation.M4dosageUnits;
                    worksheet.Cells[value + 3, 29].Value = fufForm.Fufquest6Navigation.M4frequency;
                    worksheet.Cells[value + 3, 30].Value = fufForm.Fufquest6Navigation.M4route;
                    worksheet.Cells[value + 3, 31].Value = fufForm.Fufquest6Navigation.M4startDate.HasValue ? fufForm.Fufquest6Navigation.M4startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 32].Value = fufForm.Fufquest6Navigation.M4stopDate.HasValue ? fufForm.Fufquest6Navigation.M4stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 3, 33].Value = fufForm.Fufquest6Navigation.M4ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 4, 25].Value = _sharedLocalizer["Medications"].Value + " 5";
                    worksheet.Cells[value + 4, 26].Value = fufForm.Fufquest6Navigation.M5medication;
                    worksheet.Cells[value + 4, 27].Value = fufForm.Fufquest6Navigation.M5indication;
                    worksheet.Cells[value + 4, 28].Value = fufForm.Fufquest6Navigation.M5dosageUnits;
                    worksheet.Cells[value + 4, 29].Value = fufForm.Fufquest6Navigation.M5frequency;
                    worksheet.Cells[value + 4, 30].Value = fufForm.Fufquest6Navigation.M5route;
                    worksheet.Cells[value + 4, 31].Value = fufForm.Fufquest6Navigation.M5startDate.HasValue ? fufForm.Fufquest6Navigation.M5startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 32].Value = fufForm.Fufquest6Navigation.M5stopDate.HasValue ? fufForm.Fufquest6Navigation.M5stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 4, 33].Value = fufForm.Fufquest6Navigation.M5ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 5, 25].Value = _sharedLocalizer["Medications"].Value + " 6";
                    worksheet.Cells[value + 5, 26].Value = fufForm.Fufquest6Navigation.M6medication;
                    worksheet.Cells[value + 5, 27].Value = fufForm.Fufquest6Navigation.M6indication;
                    worksheet.Cells[value + 5, 28].Value = fufForm.Fufquest6Navigation.M6dosageUnits;
                    worksheet.Cells[value + 5, 29].Value = fufForm.Fufquest6Navigation.M6frequency;
                    worksheet.Cells[value + 5, 30].Value = fufForm.Fufquest6Navigation.M6route;
                    worksheet.Cells[value + 5, 31].Value = fufForm.Fufquest6Navigation.M6startDate.HasValue ? fufForm.Fufquest6Navigation.M6startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 32].Value = fufForm.Fufquest6Navigation.M6stopDate.HasValue ? fufForm.Fufquest6Navigation.M6stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 5, 33].Value = fufForm.Fufquest6Navigation.M6ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 6, 25].Value = _sharedLocalizer["Medications"].Value + " 7";
                    worksheet.Cells[value + 6, 26].Value = fufForm.Fufquest6Navigation.M7medication;
                    worksheet.Cells[value + 6, 27].Value = fufForm.Fufquest6Navigation.M7indication;
                    worksheet.Cells[value + 6, 28].Value = fufForm.Fufquest6Navigation.M7dosageUnits;
                    worksheet.Cells[value + 6, 29].Value = fufForm.Fufquest6Navigation.M7frequency;
                    worksheet.Cells[value + 6, 30].Value = fufForm.Fufquest6Navigation.M7route;
                    worksheet.Cells[value + 6, 31].Value = fufForm.Fufquest6Navigation.M7startDate.HasValue ? fufForm.Fufquest6Navigation.M7startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 6, 32].Value = fufForm.Fufquest6Navigation.M7stopDate.HasValue ? fufForm.Fufquest6Navigation.M7stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 6, 33].Value = fufForm.Fufquest6Navigation.M7ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 7, 25].Value = _sharedLocalizer["Medications"].Value + " 8";
                    worksheet.Cells[value + 7, 26].Value = fufForm.Fufquest6Navigation.M8medication;
                    worksheet.Cells[value + 7, 27].Value = fufForm.Fufquest6Navigation.M8indication;
                    worksheet.Cells[value + 7, 28].Value = fufForm.Fufquest6Navigation.M8dosageUnits;
                    worksheet.Cells[value + 7, 29].Value = fufForm.Fufquest6Navigation.M8frequency;
                    worksheet.Cells[value + 7, 30].Value = fufForm.Fufquest6Navigation.M8route;
                    worksheet.Cells[value + 7, 31].Value = fufForm.Fufquest6Navigation.M8startDate.HasValue ? fufForm.Fufquest6Navigation.M8startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 7, 32].Value = fufForm.Fufquest6Navigation.M8stopDate.HasValue ? fufForm.Fufquest6Navigation.M8stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 7, 33].Value = fufForm.Fufquest6Navigation.M8ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 8, 25].Value = _sharedLocalizer["Medications"].Value + " 9";
                    worksheet.Cells[value + 8, 26].Value = fufForm.Fufquest6Navigation.M9medication;
                    worksheet.Cells[value + 8, 27].Value = fufForm.Fufquest6Navigation.M9indication;
                    worksheet.Cells[value + 8, 28].Value = fufForm.Fufquest6Navigation.M9dosageUnits;
                    worksheet.Cells[value + 8, 29].Value = fufForm.Fufquest6Navigation.M9frequency;
                    worksheet.Cells[value + 8, 30].Value = fufForm.Fufquest6Navigation.M9route;
                    worksheet.Cells[value + 8, 31].Value = fufForm.Fufquest6Navigation.M9startDate.HasValue ? fufForm.Fufquest6Navigation.M9startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 8, 32].Value = fufForm.Fufquest6Navigation.M9stopDate.HasValue ? fufForm.Fufquest6Navigation.M9stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 8, 33].Value = fufForm.Fufquest6Navigation.M9ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value + 9, 25].Value = _sharedLocalizer["Medications"].Value + " 10";
                    worksheet.Cells[value + 9, 26].Value = fufForm.Fufquest6Navigation.M10medication;
                    worksheet.Cells[value + 9, 27].Value = fufForm.Fufquest6Navigation.M10indication;
                    worksheet.Cells[value + 9, 28].Value = fufForm.Fufquest6Navigation.M10dosageUnits;
                    worksheet.Cells[value + 9, 29].Value = fufForm.Fufquest6Navigation.M10frequency;
                    worksheet.Cells[value + 9, 30].Value = fufForm.Fufquest6Navigation.M10route;
                    worksheet.Cells[value + 9, 31].Value = fufForm.Fufquest6Navigation.M10startDate.HasValue ? fufForm.Fufquest6Navigation.M10startDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 9, 32].Value = fufForm.Fufquest6Navigation.M10stopDate.HasValue ? fufForm.Fufquest6Navigation.M10stopDate.Value.ToShortDateString() : string.Empty;
                    worksheet.Cells[value + 9, 33].Value = fufForm.Fufquest6Navigation.M10ongoing == true ? _sharedLocalizer["Yes"].Value : _sharedLocalizer["No"].Value;

                    worksheet.Cells[value, 34].Value = _sharedLocalizer["FST34"].Value;
                    worksheet.Cells[value, 35].Value = fufForm.Fufquest2Navigation.FhaematologyA;
                    worksheet.Cells[value, 36].Value = fufForm.Fufquest2Navigation.FhaematologyC;

                    worksheet.Cells[value + 1, 34].Value = _sharedLocalizer["FST35"].Value;
                    worksheet.Cells[value + 1, 35].Value = fufForm.Fufquest2Navigation.FbiochemistryA;
                    worksheet.Cells[value + 1, 36].Value = fufForm.Fufquest2Navigation.FbiochemistryC;

                    worksheet.Cells[value + 2, 34].Value = _sharedLocalizer["FST36"].Value;
                    worksheet.Cells[value + 2, 35].Value = fufForm.Fufquest2Navigation.FureaA;
                    worksheet.Cells[value + 2, 36].Value = fufForm.Fufquest2Navigation.FureaC;

                    worksheet.Cells[value + 3, 34].Value = _sharedLocalizer["FST37"].Value;
                    worksheet.Cells[value + 3, 35].Value = fufForm.Fufquest2Navigation.FcreatinineA;
                    worksheet.Cells[value + 3, 36].Value = fufForm.Fufquest2Navigation.FcreatinineC;

                    worksheet.Cells[value + 4, 34].Value = _sharedLocalizer["FST38"].Value;
                    worksheet.Cells[value + 4, 35].Value = fufForm.Fufquest2Navigation.FphosphateA;
                    worksheet.Cells[value + 4, 36].Value = fufForm.Fufquest2Navigation.FphosphateC;

                    worksheet.Cells[value + 5, 34].Value = _sharedLocalizer["FST39"].Value;
                    worksheet.Cells[value + 5, 35].Value = fufForm.Fufquest2Navigation.FuricAcidA;
                    worksheet.Cells[value + 5, 36].Value = fufForm.Fufquest2Navigation.FuricAcidC;

                    worksheet.Cells[value + 6, 34].Value = _sharedLocalizer["FST40"].Value;
                    worksheet.Cells[value + 6, 35].Value = fufForm.Fufquest2Navigation.FbicarbonateA;
                    worksheet.Cells[value + 6, 36].Value = fufForm.Fufquest2Navigation.FbicarbonateC;

                    worksheet.Cells[value + 7, 34].Value = _sharedLocalizer["FST41"].Value;
                    worksheet.Cells[value + 7, 35].Value = fufForm.Fufquest2Navigation.FurineAnalysisA;
                    worksheet.Cells[value + 7, 36].Value = fufForm.Fufquest2Navigation.FurineAnalysisC;

                    worksheet.Cells[value + 8, 34].Value = _sharedLocalizer["FST42"].Value;
                    worksheet.Cells[value + 8, 35].Value = fufForm.Fufquest2Navigation.FserologyA;
                    worksheet.Cells[value + 8, 36].Value = fufForm.Fufquest2Navigation.FserologyC;

                    worksheet.Cells[value + 9, 34].Value = _sharedLocalizer["FST43"].Value + " : " + fufForm.Fufquest2Navigation.FothersSpecify;
                    worksheet.Cells[value + 9, 35].Value = fufForm.Fufquest2Navigation.FothersSpecifyA;
                    worksheet.Cells[value + 9, 36].Value = fufForm.Fufquest2Navigation.FothersSpecifyC;

                    worksheet.Cells[value + 10, 34].Value = _sharedLocalizer["FST44"].Value;
                    worksheet.Cells[value + 10, 35].Value = fufForm.Fufquest2Navigation.Fcomments;
                }
                return value + 10;
            }
            else
            {
                return fsp + 2;
            }
        }


        public void CreateExport(BaselineDataMaster model, FileInfo file)
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Patient Information");
                int bsrn = 1; //baseline start row number
                int epb = BaselineSheet(worksheet, bsrn, model);
                int epf = FollowUpFormSheet(worksheet, epb + 2, model);
                package.Save(); //Save the workbook.
            }
        }


        [HttpGet]
        public ActionResult PatientDetailsExport(int id = 0)
        {
            string sWebRootFolder = _environment.WebRootPath;
            string sFileName = @"PatientDetails.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }

            if (id != 0)
            {
                BaselineDataMaster model = _baselinedata.FindQuestionnairesResult(id);
                if (model.Id != 0)
                {
                    CreateExport(model, file);
                }
                else
                {
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        // add a new worksheet to the empty workbook
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("No Record");
                        package.Save(); //Save the workbook.
                    }
                }
            }
            else
            {

                //Get All Patient
                var patientDetails = _patient.GetAllPatients();
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Patient Information");
                    int bsrn = 1; //baseline start row number
                    foreach (var x in patientDetails.Results.Where(o => o.PdfName != "" && o.PdfName != null))
                    {
                        BaselineDataMaster model = _baselinedata.FindQuestionnairesResult(x.Id);
                        if (model.Id != 0)
                        {
                            int epb = BaselineSheet(worksheet, bsrn, model);
                            int epf = FollowUpFormSheet(worksheet, epb + 2, model);
                            bsrn = epf + 1;
                        }
                    }
                    package.Save(); //Save the workbook.
                }
            }

            return Json(new { data = file.Name }, new JsonSerializerSettings());

        }

        [HttpGet]
        public ActionResult ExcelDownload(string fileName)
        {
            string sWebRootFolder = _environment.WebRootPath;
            string sFileName = @"PatientDetails.xlsx";
            var result = PhysicalFile(Path.Combine(sWebRootFolder, sFileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            }.ToString();

            return result;
        }



        [HttpGet]
        public ActionResult BaselineDataFormView(int Id = 0)
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                if (!IsPrescriberAuthority(Id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                if (!IsPDFAvailable(Id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            ViewBag.patientid = Id;

            List<SelectListItem> countryList = new List<SelectListItem>();
            ServiceResponseList<Country> allCountry = _country.GetAllCounties();
            countryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Country"].Value });

            foreach (var item in allCountry.Results.Where(o => o.IsEnabled == true))
            {
                countryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.CountryName });
            }

            ViewBag.CountryList = countryList;

            BaselineDataMaster model = _baselinedata.FindQuestionnairesResult(Id);
            if (model.Id != 0)
            {
                model.Quest2Navigation.MedicalHistory = model.Quest2Navigation.MedicalHistory == null ? true : model.Quest2Navigation.MedicalHistory;
                model.Quest2Navigation.Medications = model.Quest2Navigation.Medications == null ? true : model.Quest2Navigation.Medications;
                ViewBag.SelectedCountry = model.Quest1Navigation.CountryId;

                //Prescriber Details
                PrescriberDetails preDetails = GetPrescriber(Id);
                ViewBag.Name = "" + preDetails.FirstName + " " + preDetails.LastName + "";
                ViewBag.Email = _db.AspNetUsers.Where(o => o.UserId == preDetails.AspNetUserId).Select(o => o.Email).FirstOrDefault();
                ViewBag.RegistrationNumber = preDetails.UniqueId;
                ViewBag.TelephoneNumber = preDetails.TelephoneNumber;
                ViewBag.HospitalAddress = preDetails.HospitalAddress;
                ViewBag.ContactAddress = preDetails.ContactAddress;



                if (model.Quest1Navigation.CountryId != 0)
                {
                    Country country = _country.GetAllCounties().Results.Where(o => o.Id == model.Quest1Navigation.CountryId).FirstOrDefault();
                    if (country != null)
                    {
                        ViewBag.SelectedCountryName = country.CountryName;
                    }
                }
            }
            if ((!model.IsConfirmedByAdmin && !model.IsConfirmedByHcp) && HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                return RedirectToAction("BaselineDataForm", "Prescriber", new { @id = model.PatientId });
            }

            return View(model);
            //if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            //{
            //    return RedirectToAction("BaselineDataFormView", "Prescriber", new { @id = model.PatientId });
            //}
            //else
            //{
            //    return View(model);
            //}
        }



        protected virtual PrescriberDetails GetPrescriber(int patientId)
        {
            PrescriberDetails model = new PrescriberDetails();
            if (patientId != 0)
            {
                int AspNetUserId = _patient.GetPatient(patientId).AspNetUserId ?? 0;
                model = _prescriber.GetPrescriberByAspNetUserId(AspNetUserId);
            }
            return model;
        }

        protected virtual PatientDetails GetPatient(int patientId)
        {
            PatientDetails model = new PatientDetails();
            if (patientId != 0)
            {
                model = _patient.GetPatient(patientId);
            }
            return model;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Prescriber")]
        [HttpGet]
        public ActionResult BaselineDataForm(int Id = 0)
        {
            ViewBag.IsDisplayBDF = true;
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                if (!IsPrescriberAuthority(Id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                //if (!IsBaselineDataFilled(Id)) {
                //    ViewBag.IsDisplayBDF = false;
                //}

                if (!IsPDFAvailable(Id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            ViewBag.patientid = Id;

            List<SelectListItem> countryList = new List<SelectListItem>();
            ServiceResponseList<Country> allCountry = _country.GetAllCounties();
            countryList.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Country"].Value });

            foreach (var item in allCountry.Results.Where(o => o.IsEnabled == true))
            {
                countryList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.CountryName });
            }

            ViewBag.CountryList = countryList;

            BaselineDataMaster model = _baselinedata.FindQuestionnairesResult(Id);
            if (model.Id != 0)
            {
                model.Quest2Navigation.MedicalHistory = model.Quest2Navigation.MedicalHistory == null ? true : model.Quest2Navigation.MedicalHistory;
                model.Quest2Navigation.Medications = model.Quest2Navigation.Medications == null ? true : model.Quest2Navigation.Medications;
                ViewBag.SelectedCountry = model.Quest1Navigation.CountryId;

                //Prescriber Details
                PrescriberDetails preDetails = GetPrescriber(Id);
                ViewBag.Name = "" + preDetails.FirstName + " " + preDetails.LastName + "";
                ViewBag.Email = _db.AspNetUsers.Where(o => o.UserId == preDetails.AspNetUserId).Select(o => o.Email).FirstOrDefault();
                ViewBag.RegistrationNumber = preDetails.UniqueId;
                ViewBag.TelephoneNumber = preDetails.TelephoneNumber;
                ViewBag.HospitalAddress = preDetails.HospitalAddress;
                ViewBag.ContactAddress = preDetails.ContactAddress;


            }
            if ((model.IsConfirmedByAdmin || model.IsConfirmedByHcp) && HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                return RedirectToAction("BaselineDataFormView", "Prescriber", new { @id = model.PatientId });
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                return RedirectToAction("BaselineDataFormView", "Prescriber", new { @id = model.PatientId });
            }
            else
            {
                return View(model);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Prescriber")]
        [HttpGet]
        public ActionResult FollowUpForm(int id = 0)
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                if (!IsPrescriberAuthority(id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Admin")
            {
                if (!IsPDFAvailable(id, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))))
                {
                    return RedirectToAction("NotFound404", "Error");
                }
            }
            ViewBag.patientid = id;


            //Prescriber Details
            PatientDetails preDetails = GetPatient(id);
            ViewBag.Name = "" + preDetails.FirstName + " " + preDetails.LastName + "";


            PatientViewModel pvm = new PatientViewModel();
            pvm.Id = id;
            return View(pvm);
        }

        public IActionResult Export()
        {
            return View();
        }


        public class NewModelVM
        {
            public string PatientID { get; set; }

            public string FollowUpFormID { get; set; }
        }

        public class FollowUpFormVM
        {

            public int Id { get; set; }

            public int? SrNo { get; set; }

            public string FollowUpFormName { get; set; }

            public DateTime? Date { get; set; }
        }

        public class NewModel
        {
            public int PatientId { get; set; }
            public bool isicf { get; set; }

        }
    }
}