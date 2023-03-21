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
    public class APIController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IHelper _helper;
        #endregion
        public APIController(IConfiguration configuration,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _helper = helper;
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

        #region API Details Form _KD 
        public IActionResult APICharterSummaryDetailsForm(string pidfid, string bui)
        {
            ModelState.Clear();
            var _APICharterDetailsForm = new PIDFAPICharterFormEntity();
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

                short IsCharter = 2; //if Chartet then 1 if CharterSummary then 2
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAPICharterFormData + "/" + pidfid + "/" + IsCharter, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFAPICharterFormEntity>>(jsonResponse);
                    _APICharterDetailsForm = data._object;
                }
                _APICharterDetailsForm.Pidfid = UtilityHelper.Encrypt(pidfid);
                _APICharterDetailsForm.BusinessUnitId = UtilityHelper.Encrypt(bussnessId);
                return View(_APICharterDetailsForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult APICharterDetailsForm(string pidfid, string bui)
        {
            ModelState.Clear();
            var _APICharterDetailsForm = new PIDFAPICharterFormEntity();
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

                short IsCharter = 1; //if Chartet then 1 if CharterSummary then 2
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAPICharterFormData + "/" + pidfid + "/" + IsCharter, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFAPICharterFormEntity>>(jsonResponse);
                    _APICharterDetailsForm = data._object;
                }
                _APICharterDetailsForm.Pidfid = UtilityHelper.Encrypt(pidfid);
                _APICharterDetailsForm.BusinessUnitId = UtilityHelper.Encrypt(bussnessId);
                return View(_APICharterDetailsForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]                 
        public IActionResult APICharterDetailsForm(PIDFAPICharterFormEntity _Chartermodel)
        {
            _Chartermodel.Pidfid = UtilityHelper.Decreypt(_Chartermodel.Pidfid);
            _Chartermodel.BusinessUnitId = UtilityHelper.Decreypt(_Chartermodel.BusinessUnitId);
            bool IsSaveError = false;
            if (_Chartermodel.IsModelValid != "")
            {
                //save logic
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                _Chartermodel.LoggedInUserId = Convert.ToInt32(logUserId);
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.InsertUpdateAPICharter, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(_Chartermodel))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["SaveStatus"] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    IsSaveError = false;
                    return RedirectToAction("APICharterDetailsForm",
                        new { pidfid = UtilityHelper.Encrypt(_Chartermodel.Pidfid), bui = UtilityHelper.Encrypt(_Chartermodel.BusinessUnitId) }); // return to PBFAPI List
                }
                else
                {
                    IsSaveError = true;
                }
            }
            if (IsSaveError)
                TempData["SaveStatus"] = "Save Error Occured !";

            return View(_Chartermodel);

        }


        [HttpGet]
        public IActionResult APIRndDetailsForm(string pidfid, string bui)
        {
            ModelState.Clear();
            var _APIRnDDetailsForm = new PIDFAPIRnDFormEntity();
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


                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAPIRnDFormData + "/" + pidfid, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFAPIRnDFormEntity>>(jsonResponse);
                    _APIRnDDetailsForm = data._object;
                }

                _APIRnDDetailsForm.IPEvalution = GetModelForPIDForm(pidfid, bussnessId);
                _APIRnDDetailsForm._commercialFormEntity = GetPIDFCommercialModel(pidfid, bussnessId);
                _APIRnDDetailsForm.ProjectName = _APIRnDDetailsForm.IPEvalution.ProjectName;
                _APIRnDDetailsForm.IPD_PatentDetailsList = _APIRnDDetailsForm.IPEvalution.pidf_IPD_PatentDetailsEntities;
                _APIRnDDetailsForm.Pidfid = UtilityHelper.Encrypt(pidfid);
                _APIRnDDetailsForm.BusinessUnitId = UtilityHelper.Encrypt(bussnessId);
                return View(_APIRnDDetailsForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult APIRnDDetailsForm(int _APIRnDDetailsID, PIDFAPIRnDFormEntity _RnDmodel)
        {
            _RnDmodel.Pidfid = UtilityHelper.Decreypt(_RnDmodel.Pidfid);
            _RnDmodel.BusinessUnitId = UtilityHelper.Decreypt(_RnDmodel.BusinessUnitId);
            bool IsSaveError = false;
            if (_RnDmodel.IsModelValid != "")
            {
                //save logic
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                _RnDmodel.LoggedInUserId = Convert.ToInt32(logUserId);
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.InsertUpdateAPIRnD, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(_RnDmodel))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["SaveStatus"] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    IsSaveError = false;
                    return RedirectToAction("APIRndDetailsForm",
                        new { pidfid = UtilityHelper.Encrypt(_RnDmodel.Pidfid), bui = UtilityHelper.Encrypt(_RnDmodel.BusinessUnitId) }); // return to PBFAPI List
                }
                else
                {
                    IsSaveError = true;
                }
            }
            if (IsSaveError)
                TempData["SaveStatus"] = "Save Error Occured !";

            // return back with Invalid Model
            _RnDmodel._commercialFormEntity = GetPIDFCommercialModel(_RnDmodel.Pidfid, _RnDmodel.BusinessUnitId);
            _RnDmodel.IPEvalution = GetModelForPIDForm(_RnDmodel.Pidfid, _RnDmodel.BusinessUnitId);
            _RnDmodel.ProjectName = _RnDmodel.IPEvalution.ProjectName;
            _RnDmodel.IPD_PatentDetailsList = _RnDmodel.IPEvalution.pidf_IPD_PatentDetailsEntities;
            _RnDmodel.Pidfid = UtilityHelper.Encrypt(_RnDmodel.Pidfid);
            _RnDmodel.BusinessUnitId = UtilityHelper.Encrypt(_RnDmodel.BusinessUnitId);
            return View(_RnDmodel);

        }

        [HttpGet]
        public IActionResult APIIPDDetailsForm(string pidfid, string bui)
        {
            ModelState.Clear();
            var _APIIPDDetailsForm = new PIDFAPIIPDFormEntity();
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


                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAPIIPDFormData + "/" + pidfid, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFAPIIPDFormEntity>>(jsonResponse);
                    _APIIPDDetailsForm = data._object;
                }

                _APIIPDDetailsForm.IPEvalution = GetModelForPIDForm(pidfid, bussnessId);
                _APIIPDDetailsForm._commercialFormEntity = GetPIDFCommercialModel(pidfid, bussnessId);
                _APIIPDDetailsForm.ProjectName = _APIIPDDetailsForm.IPEvalution.ProjectName;
                _APIIPDDetailsForm.IPD_PatentDetailsList = _APIIPDDetailsForm.IPEvalution.pidf_IPD_PatentDetailsEntities;
                _APIIPDDetailsForm.Pidfid = UtilityHelper.Encrypt(pidfid);
                _APIIPDDetailsForm.BusinessUnitId = UtilityHelper.Encrypt(bussnessId);
                return View(_APIIPDDetailsForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult APIIPDDetailsForm(int _APIIPDDetailsID, PIDFAPIIPDFormEntity _IPDmodel)
        {
            _IPDmodel.Pidfid = UtilityHelper.Decreypt(_IPDmodel.Pidfid);
            _IPDmodel.BusinessUnitId = UtilityHelper.Decreypt(_IPDmodel.BusinessUnitId);
            bool IsSaveError = false;
            if (_IPDmodel.IsModelValid != "")
            {
                //save logic
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                _IPDmodel.LoggedInUserId = Convert.ToInt32(logUserId);

                var form = new MultipartFormDataContent();
                if (_IPDmodel.MarketDetailsNewPortCGIDetails != null)
                {
                    var fileStream = _IPDmodel.MarketDetailsNewPortCGIDetails.OpenReadStream();
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
                    form.Add(fileContent, "file", _IPDmodel.MarketDetailsNewPortCGIDetails.FileName);
                }

                form.Add(new StringContent(JsonConvert.SerializeObject(_IPDmodel)), "Data");

                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                _IPDmodel.MarketDetailsNewPortCGIDetails = null; // set to null else it gives Error-406 while POST to API

                //HttpResponseMessage responseMessage = objapi.APIComm(APIURLHelper.InsertUpdateAPIIPD, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(_IPDmodel))).Result;
                HttpResponseMessage responseMessage = objapi.APIComm(APIURLHelper.InsertUpdateAPIIPD, HttpMethod.Post, token, form).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["SaveStatus"] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    IsSaveError = false;
                    return RedirectToAction("APIIPDDetailsForm",
                        new { pidfid = UtilityHelper.Encrypt(_IPDmodel.Pidfid), bui = UtilityHelper.Encrypt(_IPDmodel.BusinessUnitId) }); // return to PBFAPI List
                }
                else
                {
                    IsSaveError = true;
                }
            }
            if (IsSaveError)
                TempData["SaveStatus"] = "Save Error Occured !";

            // return back with Invalid Model
            _IPDmodel._commercialFormEntity = GetPIDFCommercialModel(_IPDmodel.Pidfid, _IPDmodel.BusinessUnitId);
            _IPDmodel.IPEvalution = GetModelForPIDForm(_IPDmodel.Pidfid, _IPDmodel.BusinessUnitId);
            _IPDmodel.ProjectName = _IPDmodel.IPEvalution.ProjectName;
            _IPDmodel.IPD_PatentDetailsList = _IPDmodel.IPEvalution.pidf_IPD_PatentDetailsEntities;
            _IPDmodel.Pidfid = UtilityHelper.Encrypt(_IPDmodel.Pidfid);
            _IPDmodel.BusinessUnitId = UtilityHelper.Encrypt(_IPDmodel.BusinessUnitId);
            return View(_IPDmodel);

        }

        [NonAction] // Get Model for View PIDForm.cshtml (IP EVolution)
        private IPDEntity GetModelForPIDForm(string pidfid, string bussnessId)
        {
            IPDEntity oPIDForm = new();
            try
            {

                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
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
                    oPIDForm.LogInId = Convert.ToInt32(logUserId);
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


        [NonAction]
        private PIDFCommercialEntity GetPIDFCommercialModel(string pidfid, string bui)
        {
            PIDFCommercialEntity oPIDForm = new();
            //pidfid = UtilityHelper.Decreypt(pidfid);
            string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
            HttpResponseMessage resMsg;
            var _pidfEntity = GetPidfFormModel(int.Parse(pidfid), out resMsg);
            var _IPDFormdata = GetModelForIPDForm(pidfid, bui); //IPD Form Data
            string bussnessId = _pidfEntity.BusinessUnitId.ToString();
            var StrengthId = 0;
            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_cofiguration);
            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetCommercialFormData + "/" + pidfid + "/" + bussnessId + "/" + StrengthId, HttpMethod.Get, token).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFCommercialEntity>>(jsonResponse);
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
            oPIDForm.IPDFormEntity = _IPDFormdata;
            oPIDForm.BusinessUnitsByUser = GetUserWiseBusinessUnit(Convert.ToInt32(logUserId));
            oPIDForm.BusinessUnitId = oPIDForm.pidfEntity.BusinessUnitId;
            return oPIDForm;
        }

        [NonAction] // Get Model for View PIDForm.cshtml 
        public IPDEntity GetModelForIPDForm(string pidfid, string bussnessId)
        {
            IPDEntity oPIDForm = new();
            try
            {

                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
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
                    oPIDForm.LogInId = Convert.ToInt32(logUserId);
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
        #endregion

    }

}
