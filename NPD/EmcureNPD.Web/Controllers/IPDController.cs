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
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class IPDController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IHelper _helper;
        //private IHostingEnvironment _env;

        #endregion Properties

        public IPDController(IConfiguration configuration,// IHostingEnvironment env,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _helper = helper;
        }

        //[Route("IPD/IPD")]
        [HttpGet]
        public IActionResult IPD(string pidfid, string bui, bool _Partial = false, bool IsViewMode = false)
        {
            ModelState.Clear();
            IPDEntity oIPD = new();
            try
            {
                string IsView = HttpContext.Request.Query["IsView"];
                //if (!IsViewMode && !(IsView == "1"))
                //{
                //    string PIDFID = UtilityHelper.Decreypt(pidfid);
                //    if (!_helper.IsAccessToPIDF((int)ModuleEnum.IPD, int.Parse(PIDFID)))
                //    {
                //        return RedirectToAction("PIDFList", "PIDF", new { ScreenId = Convert.ToString((int)EmcureNPD.Utility.Enums.PIDFScreen.IPD) });
                //    }
                //}

                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.IPD, rolId);
                if (!_Partial)
                {
                    if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit))
                    {
                        return RedirectToAction("AccessRestriction", "Home");
                    }
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

                oIPD = GetModelForIPDForm(pidfid, bussnessId);
                oIPD._Partial = _Partial;
                oIPD.IsViewMode = IsViewMode;
                return View(oIPD);
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult IPDPartial(long pidfid, int bui)
        {
            IPDEntity oIPD = new();
            try
            {
                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.IPD, rolId);
                //if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit))
                //{
                //    return RedirectToAction("AccessRestriction", "Home");
                //}
                ViewBag.Access = objPermssion;

                oIPD = GetModelForIPDForm(pidfid.ToString(), bui.ToString());

                return PartialView("_IPDPartial", oIPD);
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

        [NonAction] // Get Model for View IPD.cshtml
        private IPDEntity GetModelForIPDForm(string pidfid, string bussnessId)
        {
            IPDEntity oIPD = new();
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetIPDFormData + "/" + pidfid + "/" + bussnessId, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<IPDEntity>>(jsonResponse);
                    oIPD = data._object;
                    oIPD.BusinessUnitId = Convert.ToInt32(bussnessId);
                    oIPD.PIDFID = Convert.ToInt64(pidfid);
                    oIPD.LogInId = _helper.GetLoggedInUserId();
                    oIPD.BusinessUnitsByUser = _helper.GetAssignedBusinessUnit();
                    TempData["BusList"] = JsonConvert.SerializeObject(oIPD.MasterBusinessUnitEntities);

                    if (oIPD.pidf_IPD_PatentDetailsEntities == null || oIPD.pidf_IPD_PatentDetailsEntities.Count == 0)
                    {
                        oIPD.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                        oIPD.pidf_IPD_PatentDetailsEntities.Add(new PIDF_IPD_PatentDetailsEntity());
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntities.Count;
                    }
                    else
                    {
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntities.Count - 1;
                    }
                    if (oIPD.pidf_IPD_PatentDetailsEntitiesAPI == null || oIPD.pidf_IPD_PatentDetailsEntitiesAPI.Count == 0)
                    {
                        oIPD.pidf_IPD_PatentDetailsEntitiesAPI = new List<PIDF_IPD_PatentDetailsEntity>();
                        oIPD.pidf_IPD_PatentDetailsEntitiesAPI.Add(new PIDF_IPD_PatentDetailsEntity());
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntitiesAPI.Count;
                    }
                    else
                    {
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntitiesAPI.Count - 1;
                    }
                    HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + pidfid, HttpMethod.Get, token).Result;

                    if (responseMS.IsSuccessStatusCode)
                    {
                        string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                        var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                        oIPD.ProjectName = retPIDF._object.MoleculeName;
                    }
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw ex;
            }
            return oIPD;
        }

        public IActionResult IPDList()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.IPD, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IPD(IPDEntity IPDEntity)
        {
            try
            {
                if (IPDEntity.SaveType == "Sv")
                    IPDEntity.StatusId = (Int32)Master_PIDFStatus.IPDSubmitted;
                else
                    IPDEntity.StatusId = (Int32)Master_PIDFStatus.IPDInProgress;

                IPDEntity.CreatedBy = _helper.GetLoggedInUserId();

                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                string x = JsonConvert.SerializeObject(IPDEntity);

                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SaveIPDForm, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(IPDEntity))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData[UserHelper.SuccessMessage] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    return RedirectToAction("PIDFList", "PIDF", new { ScreenId = (int)PIDFScreen.IPD });
                }
                else
                {
                    TempData[UserHelper.ErrorMessage] = Convert.ToString(responseMessage.Content.ReadAsStringAsync().Result);
                    //ModelState.Clear();
                    IPDEntity.MasterBusinessUnitEntities = JsonConvert.DeserializeObject<List<MasterBusinessUnitEntity>>(Convert.ToString(TempData["BusList"]));

                    TempData.Keep("BusList");

                    return View((IPDEntity));
                }
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                //ModelState.Clear();
                return View(nameof(IPDList));
            }
        }
    }
}