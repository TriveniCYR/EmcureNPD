using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Principal;

namespace EmcureNPD.Web.Controllers
{
    public class PBFController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IHelper _helper;
        #endregion
        public PBFController(IConfiguration configuration,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _helper = helper;
        }


       


        public IActionResult PIDFList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PBFRnDForm(string pidfid, string bui)
        {
            return View();
        }
        [HttpGet]
        public IActionResult PBFAnaLyticalForm(string pidfid, string bui)
        {
            //MasterProductStrengthEntity p = new MasterProductStrengthEntity();
            //p.ProductStrengthId = 1;
            //p.ProductStrengthName = "PCTMOL 250";
            //MasterProductStrengthEntity p2 = new MasterProductStrengthEntity();
            //p2.ProductStrengthId = 1;
            //p2.ProductStrengthName = "PCTMOL 250";
            //List<MasterProductStrengthEntity> Pn = new List<MasterProductStrengthEntity>();
            //Pn.Add(p);
            //Pn.Add(p2);
            //PidfPbfAnalyticalEntity PnEntity= new ();
            //PnEntity.ProjectName = "Prashant";
            //PnEntity.ProductStrength = Pn;
            //PnEntity.SAPProjectProjectCode = "Fill From DB";
            //PnEntity.ImprintingEmbossingCodes = "Fill From DB";


            return View();
        }
        public IActionResult PBFClinicalForm(string pidfid, string bui)
        {
            return View();
        }

        [HttpGet]
        public IActionResult PBFForm(string pidfid, string bui)
        {
            ModelState.Clear();
            PidfPbfFormEntity oPIDForm = new();
            try
            {
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;
                oPIDForm = GetPIDFPbfModel(pidfid, bui);
                return View(oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }
        [HttpGet]
        public IActionResult PBFFormAnalytical(string pidfid, string bui, string? strengthId)
        {
            ModelState.Clear();

            PidfPbfFormEntity oPIDForm = new PidfPbfFormEntity();
            try
            {
                strengthId = (strengthId == null) ? "0" : strengthId;
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;

                oPIDForm = GetPIDFPbfModelAnalytical(pidfid, bui, strengthId);
                if (oPIDForm.MasterStrengthEntities.Count > 0 && oPIDForm.MasterStrengthEntities != null)
                    ViewBag.DefaultStrengthId = Convert.ToString(oPIDForm.MasterStrengthEntities[0].PidfproductStrengthId);


                return View("PBFFormAnalytical", oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

        [NonAction]
        private PidfPbfFormEntity GetPIDFPbfModelAnalytical(string pidfid, string bui, string StrengthId)
        {
            PidfPbfFormEntity oPIDForm = new();
            long n;
            if (!long.TryParse(bui, out n))
            {
                bui = UtilityHelper.Decreypt(bui);
                StrengthId = (StrengthId == "0") ? "0" : UtilityHelper.Decreypt(StrengthId);
            }
            if (!long.TryParse(pidfid, out n))
            {
                pidfid = UtilityHelper.Decreypt(pidfid);
            }
            if (!long.TryParse(StrengthId, out n))
            {
                StrengthId = UtilityHelper.Decreypt(StrengthId);
            }

            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            // var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);

            //pidfid = int.Parse(pidfid) ? UtilityHelper.Decreypt(pidfid) : pidfid;
            string bussnessId = bui;
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetPbfFormDetailsAnalytical + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PidfPbfFormEntity>>(jsonResponse);
                oPIDForm = data._object;
                oPIDForm.Pidfid = Convert.ToInt64(pidfid);
                oPIDForm.CreatedBy = Convert.ToInt32(logUserId);
                TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                }
            }
            //oPIDForm.pidfEntity = _pidfEntity;

            //oPIDForm.BusinessUnitId = oPIDForm.pidfEntity.BusinessUnitId;
            return oPIDForm;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PBFAnalytical(int PIDFId, PidfPbfFormEntity pbfEntity)
        {
            try
            {
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }

                if (pbfEntity.SaveSubmitType == "Save")
                    pbfEntity.StatusId = (Int32)Master_PIDFStatus.PIDFInProgress;
                else
                    pbfEntity.StatusId = (Int32)Master_PIDFStatus.PIDFSubmitted;
                pbfEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SavePBFAnalytical, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pbfEntity))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    ModelState.Clear();
                    return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
                }


            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                ModelState.Clear();
                return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
            }
            return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
        }

        [NonAction]
        private PidfPbfFormEntity GetPIDFPbfModel(string pidfid, string bui)
        {
            PidfPbfFormEntity oPIDForm = new();
            pidfid = UtilityHelper.Decreypt(pidfid);
            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);
            string bussnessId = _pidfEntity.BusinessUnitId.ToString();
            var StrengthId = 0;
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetPbfFormDetails + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PidfPbfFormEntity>>(jsonResponse);
                oPIDForm = data._object;
                oPIDForm.Pidfid = Convert.ToInt64(pidfid);
                oPIDForm.CreatedBy = Convert.ToInt32(logUserId);
                TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                }
            }
            //oPIDForm.pidfEntity = _pidfEntity;

            oPIDForm.BusinessUnitId = _pidfEntity.BusinessUnitId;
            return oPIDForm;
        }

        [NonAction]
        private PIDFEntity GetPidfFormModel(int? PIDFId, out HttpResponseMessage responseMessage)
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + PIDFId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsonResponse);
                    return data._object;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PBFClinical(int PIDFId, PIDFPBFClinicalFormEntity pbfEntity)
        {
            try
            {
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }

                if (pbfEntity.SaveSubmitType == "Save")
                    pbfEntity.StatusId = (Int32)Master_PIDFStatus.PIDFInProgress;
                else
                    pbfEntity.StatusId = (Int32)Master_PIDFStatus.PIDFSubmitted;
                pbfEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SavePBFClinical, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pbfEntity))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    ModelState.Clear();
                    return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
                }


            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                ModelState.Clear();
               return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
            }
            return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
        }

        //-------------------------------------Start-----KUldip NEw PBF Modules---------------------      

        [HttpGet]
        public IActionResult PBFRnDDetailsForm(string pidfid, string bui)
        {
            ModelState.Clear();
            PidfPbfFormEntity oPIDForm = new();
            try
            {
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;
                oPIDForm = GetPIDFPbfModel(pidfid, bui);
                return View("PBFRnDForm", oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }
        [HttpGet]
        public IActionResult PBFClinicalDetailsForm(string pidfid, string bui, string strength)
        {
            ModelState.Clear();
            //ModelState.Remove("PidfPbfClinicals.TotalExpense");
            //ModelState.Remove("ProjectName");
            
            PIDFPBFClinicalFormEntity oPIDForm = null;
            try
            {
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;
                oPIDForm = GetPIDFPbfClinicalModel(pidfid, bui, strength);
                return View(oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

       
        [NonAction]
        private PIDFPBFClinicalFormEntity GetPIDFPbfClinicalModel(string pidfid, string bui, string strength)
        {
           var oPIDForm = new PIDFPBFClinicalFormEntity();
            pidfid = UtilityHelper.Decreypt(pidfid);
            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);          
            string bussnessId = UtilityHelper.Decreypt(bui);
            string StrengthId = "0";
            if (strength !=null)
            {
                StrengthId = UtilityHelper.Decreypt(strength);
            }
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetPbfFormDetails + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFPBFClinicalFormEntity>>(jsonResponse);
                oPIDForm = data._object;                
                oPIDForm.Pidfid = Convert.ToInt64(pidfid);
                oPIDForm.BusinessUnitId = Convert.ToInt32(bussnessId);
                oPIDForm.CreatedBy = Convert.ToInt32(logUserId);
                oPIDForm.BusinessUnitsByUser = GetUserWiseBusinessUnit(Convert.ToInt32(logUserId));
                TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                }
            }
            //oPIDForm.pidfEntity = _pidfEntity;

            //oPIDForm.BusinessUnitId = _pidfEntity.BusinessUnitId;
            return oPIDForm;
        }

        //-------------------------------------End-----KUldip NEw PBF Modules---------------------
        [NonAction]
        private string GetUserWiseBusinessUnit(int userid)
        {
            string _list = string.Empty;
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                List<MasterBusinessUnitEntity> listBusUnit = new List<MasterBusinessUnitEntity>();
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetBusinessUnitByUserId + "/" + userid, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<List<MasterBusinessUnitEntity>>>(jsonResponse);
                    listBusUnit = data._object;
                    int[] BUArr = listBusUnit.Select(x => x.BusinessUnitId).ToArray();
                    _list = string.Join(",", BUArr);

                    return _list;
                }
                else
                {
                    return _list;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region PBF

       
        public IActionResult PBF(string pidfid, string bui)
        {
            try
            {
                PBFFormEntity oPBForm = null;
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;
                oPBForm = GetPbfModel(pidfid, bui, null);
                return View(oPBForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }


            return View();
        }

        [NonAction]
        private PBFFormEntity GetPbfModel(string pidfid, string bui, string strength)
        {
            var oPBForm = new PBFFormEntity();
            pidfid = UtilityHelper.Decreypt(pidfid);
            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            string bussnessId = UtilityHelper.Decreypt(bui);
            string StrengthId = "0";
            if (strength != null)
            {
                StrengthId = UtilityHelper.Decreypt(strength);
            }
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetPbfFormDetails + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PBFFormEntity>>(jsonResponse);
                oPBForm = data._object;
                oPBForm.Pidfid = Convert.ToInt64(pidfid);
                oPBForm.BusinessUnitId = Convert.ToInt32(bussnessId);
                
                //oPBForm.BusinessUnitsByUser = GetUserWiseBusinessUnit(Convert.ToInt32(logUserId));

                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                }
            }
            oPBForm.BusinessUnitsByUser = _helper.GetAssignedBusinessUnit();
            return oPBForm;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PBF(int PIDFId, PBFFormEntity pbfEntity)
        {
            try
            {
                //rnd json data
                pbfEntity.RNDExicipients = JsonConvert.DeserializeObject<List<RNDExicipient>>(pbfEntity.RNDExicipientRawData);
                pbfEntity.RNDPackagings = JsonConvert.DeserializeObject<List<RNDPackaging>>(pbfEntity.RNDPackagingRawData);
                pbfEntity.RNDToolingChangeparts = JsonConvert.DeserializeObject<List<RNDToolingChangepart>>(pbfEntity.RNDToolingChangePartRawData);
                pbfEntity.RNDCapexMiscellaneousExpenses = JsonConvert.DeserializeObject<List<RNDCapexMiscellaneousExpense>>(pbfEntity.RNDCapexMiscellaneousExpensesRawData);
                pbfEntity.RNDPlantSupportCosts = JsonConvert.DeserializeObject<List<RNDPlantSupportCost>>(pbfEntity.RNDPlantSupportCostRawData);
                pbfEntity.RNDFillingExpenses = JsonConvert.DeserializeObject<List<RNDFillingExpense>>(pbfEntity.RNDFillingExpensesRawData);
                pbfEntity.RNDManPowerCosts = JsonConvert.DeserializeObject<List<RNDManPowerCost>>(pbfEntity.RNDManPowerCostProjectDurationRawData);

                //Analytical json data
                pbfEntity.AnalyticalEntities = JsonConvert.DeserializeObject<List<AnalyticalEntity>>(pbfEntity.AnalyticalRawData);
                pbfEntity.AnalyticalStrengthMappingEntities = JsonConvert.DeserializeObject<List<AnalyticalAmvcostStrengthMappingEntity>>(pbfEntity.AnalyticalStrengthMappingRawData);

                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PBF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }               
                //pbfEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SavePBF, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pbfEntity))).Result;

                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                ModelState.Clear();
                var data = JsonConvert.DeserializeObject<APIResponseEntity<dynamic>>(jsonResponse);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData[EmcureNPD.Web.Helpers.UserHelper.SuccessMessage] = data._Message;
                    return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
                }
                else
                {
                    ViewBag.errormessage = Convert.ToString(data._Message);
                    return View(pbfEntity);
                }
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                ModelState.Clear();
                //return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
                return View(pbfEntity);
            }
            //return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 6 });
        }



        #endregion
    }

}
