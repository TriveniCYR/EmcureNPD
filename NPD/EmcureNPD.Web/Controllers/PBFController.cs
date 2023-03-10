using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

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
            PidfPbfEntity oPIDForm = new();
            try
            {
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
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
        [NonAction]
        private PidfPbfEntity GetPIDFPbfModel(string pidfid, string bui)
        {
            PidfPbfEntity oPIDForm = new();
           // pidfid = UtilityHelper.Decreypt(pidfid);
            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);
            string bussnessId = _pidfEntity.BusinessUnitId.ToString();
            var StrengthId = 0;
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetPbfFormData + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PidfPbfEntity>>(jsonResponse);
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
            oPIDForm.pidfEntity = _pidfEntity;

            oPIDForm.BusinessUnitId = oPIDForm.pidfEntity.BusinessUnitId;
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
        public IActionResult PBF(int PIDFId, PidfPbfFormEntity pbfEntity)
        {
            try
            {
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
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
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SavePBF, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pbfEntity))).Result;

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
    }

}
