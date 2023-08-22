using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    [ViewComponent]
    public class PIDFController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        private readonly IHelper _helper;

        #endregion Properties

        public PIDFController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;
            _helper = helper;
        }

        public IActionResult PIDFList()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PIDF, rolId);
            ViewBag._objPermission = objPermssion;

            if (TempData.ContainsKey(UserHelper.SuccessMessage))
            {
                TempData[UserHelper.SuccessMessage] = TempData[UserHelper.SuccessMessage];
            }
            return View();
        }

        public IActionResult PIDF(int? PIDFId, int? bui, bool _Partial = false, bool IsViewMode = false)
        {

            PIDFEntity pidf;
          
            try
            {
                //string IsView = HttpContext.Request.Query["IsView"];
                //if (!IsViewMode && !(IsView =="1"))
                //{
                //    if (!_helper.IsAccessToPIDF((int)ModuleEnum.PIDF, PIDFId))
                //    {
                //         return RedirectToAction("PIDFList","PIDF", new { ScreenId = Convert.ToString((int)EmcureNPD.Utility.Enums.PIDFScreen.PIDF) });
                //    }
                //}


                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PIDF, rolId);
                ViewBag.Access = objPermssion;
                if (!_Partial)
                {
                    if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit))
                    {
                        return RedirectToAction("AccessRestriction", "Home");
                    }
                }

                if (PIDFId == null || PIDFId <= 0)
                {
                    pidf = new PIDFEntity();
                    pidf._Partial = _Partial;
                    pidf.IsViewMode = IsViewMode;
                    pidf.LogInId = _helper.GetLoggedInUserId();
                    return View(pidf);
                }
                else
                {
                    HttpResponseMessage responseMessage;
                   // string buid = HttpContext.Request.Query["bui"];
                    var data = GetPidfFormModel(PIDFId, bui, out responseMessage);

                    if (data != null)
                    {
                        data._Partial = _Partial;
                        data.IsViewMode = IsViewMode;

                        if (bui != null && bui > 0)
                        {
                            data.SelectedBusinessUnitId = Convert.ToInt32(bui);
                        }

                        data.LogInId = _helper.GetLoggedInUserId();
                        return View(data);
                    }
                    else
                    {
                        TempData[UserHelper.ErrorMessage] = Convert.ToString(responseMessage.Content.ReadAsStringAsync().Result);
                        return RedirectToAction("PIDFList");
                    }
                }
            }
            catch (Exception e)
            {
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                return RedirectToAction("PIDFList");
            }
        }

        [NonAction]
        private PIDFEntity GetPidfFormModel(int? PIDFId, int? BusinessUnitId, out HttpResponseMessage responseMessage)
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetPIDFById_BUID + "/" + PIDFId + "/" + (BusinessUnitId == null ? 0 : BusinessUnitId), HttpMethod.Get, token).Result;

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
        public IActionResult PIDF(int PIDFId, PIDFEntity pIDFEntity)
        {
            try
            {
               
                if (pIDFEntity.SaveType == "submit")
                    pIDFEntity.StatusId = (Int32)Master_PIDFStatus.PIDFSubmitted;
                else {
                    pIDFEntity.StatusId = (Int32)Master_PIDFStatus.PIDFInProgress;
                }
                   

                if (pIDFEntity.PIDFID <= 0)
                    pIDFEntity.LastStatusId = pIDFEntity.StatusId;

                //pIDFEntity.CreatedBy = _helper.GetLoggedInUserId();

                string token = _helper.GetToken();
                APIRepository objapi = new(_cofiguration);
               
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SavePIDF, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(pIDFEntity))).Result;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                ModelState.Clear();
                var data = JsonConvert.DeserializeObject<APIResponseEntity<dynamic>>(jsonResponse);
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData[EmcureNPD.Web.Helpers.UserHelper.SuccessMessage] = data._Message;
                    if (!string.IsNullOrEmpty(pIDFEntity.PIDFIsInterested) && (pIDFEntity.PIDFIsInterested == "1" || pIDFEntity.PIDFIsInterested == "0"))
                    {
                        // redirect to the same screen
                        return RedirectToAction(nameof(PIDF), new { PIDFId = pIDFEntity.PIDFID, bui = pIDFEntity.SelectedBusinessUnitId });
                    }
                    else
                    {
                        return RedirectToAction(nameof(PIDFList), new { ScreenId = (int)PIDFScreen.PIDF });
                    }
                }
                else
                {
                    ViewBag.errormessage = Convert.ToString(data._Message);
                    return View(pIDFEntity);
                }
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                ModelState.Clear();
                return View(pIDFEntity);
            }
        }

        public IActionResult PIDFCommercial()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PIDFCommerciaLDetails(string pidfid, string bui, int? IsView)
        {
            ModelState.Clear();
            PIDFCommercialEntity oPIDForm = new();
            try
            {
                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PIDF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;
                oPIDForm = GetPIDFCommercialModel(pidfid, bui);
                oPIDForm.IsView = (IsView == null) ? 0 : (int)IsView;
                return View(oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("~/Views/Account/Login");
            }
        }

        [NonAction]
        private PIDFCommercialEntity GetPIDFCommercialModel(string pidfid, string bui)
        {
            PIDFCommercialEntity oPIDForm = new();
            pidfid = UtilityHelper.Decreypt(pidfid);
            bui = UtilityHelper.Decreypt(bui);
            string AssignedBusinessUnit = _helper.GetAssignedBusinessUnit();
            //HttpResponseMessage resMsg;
            //var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg); //PIDF Form data
            //var _ipdFormEntity = GetModelForIPDForm(pidfid, bui); //IPD Form Data
            //string bussnessId = _pidfEntity.BusinessUnitId.ToString();
            var StrengthId = 0;
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetCommercialFormData + "/" + pidfid + "/" + bui + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFCommercialEntity>>(jsonResponse);
                oPIDForm = data._object;
                oPIDForm.Pidfid = Convert.ToInt64(pidfid);
                oPIDForm.CreatedBy = _helper.GetLoggedInUserId();
                //TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                }
            }
            //oPIDForm.pidfEntity = _pidfEntity;
            //oPIDForm.IPDFormEntity = _ipdFormEntity;
            oPIDForm.BusinessUnitsByUser = AssignedBusinessUnit; //GetUserWiseBusinessUnit(Convert.ToInt32(logUserId));
            oPIDForm.BusinessUnitId = Convert.ToInt32(bui); //oPIDForm.pidfEntity.BusinessUnitId;
            return oPIDForm;
        }

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

        public IActionResult PBFAPI()
        {
            ViewData["Title"] = _stringLocalizerMaster["PIDFManagement"];
            return View();
        }

        public IActionResult PIDFFinance()
        {
            return View();
        }

        public IActionResult PIDFManagement()
        {
            return View();
        }

        public IActionResult ManagementPIDF()
        {
            return View();
        }

        public IActionResult IPDManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PBFRnDForm(string pidfid, string bui)
        {
            return View();
        }

        public IActionResult PBFAnalyticalForm(string pidfid, string bui)
        {
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

            PidfPbfFormEntity oPIDForm = new PidfPbfFormEntity();
            try
            {
                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.PIDF, rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");
                }
                ViewBag.Access = objPermssion;

                oPIDForm = GetPIDFPbfModel(pidfid, bui);
                PIDFPBFClinicalFormEntity clinicalEntity = new PIDFPBFClinicalFormEntity();
                clinicalEntity.PbfFormEntities = oPIDForm.PbfFormEntities;
                oPIDForm.PbfClinicalEntities = clinicalEntity;
                //return PartialView("~/Views/PIDF/_PBFForm.cshtml", oPIDForm);
                return View(oPIDForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

        [NonAction]
        private PidfPbfFormEntity GetPIDFPbfModel(string pidfid, string bui)
        {
            PidfPbfFormEntity oPIDForm = new();
            pidfid = UtilityHelper.Decreypt(pidfid);
            bui = UtilityHelper.Decreypt(bui);
            //HttpResponseMessage resMsg;
            // var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);

            //pidfid = int.Parse(pidfid) ? UtilityHelper.Decreypt(pidfid) : pidfid;
            string bussnessId = bui;
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
                oPIDForm.CreatedBy = _helper.GetLoggedInUserId();
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

        [NonAction] // Get Model for View PIDForm.cshtml
        public IPDEntity GetModelForIPDForm(string pidfid, string bussnessId)
        {
            IPDEntity oPIDForm = new();
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetIPDFormData + "/" + pidfid + "/" + bussnessId, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<IPDEntity>>(jsonResponse);
                    oPIDForm = data._object;
                    oPIDForm.BusinessUnitId = Convert.ToInt32(bussnessId);
                    oPIDForm.PIDFID = Convert.ToInt64(pidfid);
                    oPIDForm.LogInId = _helper.GetLoggedInUserId();
                    TempData["BusList"] = JsonConvert.SerializeObject(oPIDForm.MasterBusinessUnitEntities);

                    if (oPIDForm.pidf_IPD_PatentDetailsEntities == null || oPIDForm.pidf_IPD_PatentDetailsEntities.Count == 0)
                    {
                        oPIDForm.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                        oPIDForm.pidf_IPD_PatentDetailsEntities.Add(new PIDF_IPD_PatentDetailsEntity());
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
            catch (Exception ex)
            {
                throw ex;
            }
            return oPIDForm;
        }
    }
}