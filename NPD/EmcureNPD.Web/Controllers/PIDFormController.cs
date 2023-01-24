﻿using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace EmcureNPD.Web.Controllers
{
    public class PIDFormController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private IHostingEnvironment _env;
        #endregion

        public PIDFormController(IConfiguration configuration, IHostingEnvironment env,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _env = env;
        }

        //[Route("PIDForm/PIDForm")]
        [HttpGet]
        public IActionResult PIDForm(string pidfid, string bui)
        {

            ModelState.Clear();
            PIDFormEntity oPIDForm = new();
            try
            {                
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }
                ViewBag.Access = objPermssion;
                pidfid = UtilityHelper.Decreypt(pidfid);
                string bussnessId = "";
                if (string.IsNullOrEmpty(bui))
                {
                    bussnessId = Convert.ToString(HttpContext.Session.GetInt32(UserHelper.LoggedInBusId));
                }
                else
                    bussnessId = UtilityHelper.Decreypt(bui);

                oPIDForm= GetModelForPIDForm(pidfid, bussnessId);

                return View(oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }

        }

        [NonAction] // Get Model for View PIDForm.cshtml 
        private PIDFormEntity GetModelForPIDForm(string pidfid, string bussnessId)
        {
            PIDFormEntity oPIDForm = new();
            try
            {
               
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetIPDFormData + "/" + pidfid + "/" + bussnessId, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFormEntity>>(jsonResponse);
                    oPIDForm = data._object;
                    oPIDForm.BusinessUnitId = Convert.ToInt32(bussnessId);
                    oPIDForm.PIDFID = Convert.ToInt64(pidfid);
                    oPIDForm.LogInId = Convert.ToInt32(logUserId);
                    TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                    if (oPIDForm.pidf_IPD_PatentDetailsEntities == null || oPIDForm.pidf_IPD_PatentDetailsEntities.Count == 0)
                    {
                        oPIDForm.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                        oPIDForm.pidf_IPD_PatentDetailsEntities.Add(new PIDF_IPD_PatentDetailsEntity() { PatentNumber = "1" });
                        oPIDForm.TotalParent = oPIDForm.pidf_IPD_PatentDetailsEntities.Count;
                    }
                    else
                    {
                        oPIDForm.TotalParent = oPIDForm.pidf_IPD_PatentDetailsEntities.Count - 1;
                    }
                    HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                    if (responseMS.IsSuccessStatusCode)
                    {
                        string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                        var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                        oPIDForm.ProjectName = retPIDF._object.MoleculeName;

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return oPIDForm;
        }
        public IActionResult PIDFormList()
        {

            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
            if (objPermssion == null || !objPermssion.View)
            {
                return RedirectToAction("AccessRestriction", "Home");

            }
            ViewBag.Access = objPermssion;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PIDForm(PIDFormEntity pidFormEntity)
        {
            try
            {
                if (pidFormEntity.SaveType == "Sv")
                    pidFormEntity.StatusId = (Int32)Master_PIDFStatus.IPDCreated;
                else
                    pidFormEntity.StatusId = (Int32)Master_PIDFStatus.IPDBDPendingApproval;
                pidFormEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                string x = JsonConvert.SerializeObject(pidFormEntity);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SaveIPDForm, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pidFormEntity))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData[UserHelper.SuccessMessage] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    return RedirectToAction(nameof(PIDFormList));
                }
                else
                {
                    TempData[UserHelper.ErrorMessage] = Convert.ToString(responseMessage.Content.ReadAsStringAsync().Result);
                    //ModelState.Clear();
                    pidFormEntity.MasterBusinessUnitEntities = JsonConvert.DeserializeObject<List<MasterBusinessUnitEntity>>(Convert.ToString(TempData["BusList"]));

                    TempData.Keep("BusList");

                    return View((pidFormEntity));
                }
            }
            catch (Exception e)
            {
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                //ModelState.Clear();
                return View(nameof(PIDFormList));
            }
        }
    }
}
