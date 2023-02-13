using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
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
using System.Reflection;

namespace EmcureNPD.Web.Controllers
{
	public class PBFController : BaseController
	{
		#region Properties
		private readonly IConfiguration _cofiguration;
		private readonly IStringLocalizer<Errors> _stringLocalizerError;
		private readonly IStringLocalizer<Shared> _stringLocalizerShared;
		#endregion
		public PBFController(IConfiguration configuration,
			IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
		{
			_cofiguration = configuration;
			_stringLocalizerError = stringLocalizerError;
			_stringLocalizerShared = stringLocalizerShared;
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
			pidfid = UtilityHelper.Decreypt(pidfid);
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

        #region APIIPD Details Form _KD         
        
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
                    _APIIPDDetailsForm= data._object;
                }
                
                _APIIPDDetailsForm.IPEvalution = GetModelForPIDForm(pidfid, bussnessId);
                _APIIPDDetailsForm._commercialFormEntity= GetPIDFCommercialModel(pidfid, bussnessId);
                _APIIPDDetailsForm.ProjectName = _APIIPDDetailsForm.IPEvalution.ProjectName;
                _APIIPDDetailsForm.IPD_PatentDetailsList = _APIIPDDetailsForm.IPEvalution.pidf_IPD_PatentDetailsEntities;
                _APIIPDDetailsForm.Pidfid= UtilityHelper.Encrypt(pidfid);
                _APIIPDDetailsForm.BusinessUnitId = UtilityHelper.Encrypt(bussnessId);
                return View(_APIIPDDetailsForm);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult APIIPDDetailsForm(int _APIIPDDetailsID, PIDFAPIIPDFormEntity _IPDmodel)
        {
            _IPDmodel.Pidfid = UtilityHelper.Decreypt(_IPDmodel.Pidfid);
            _IPDmodel.BusinessUnitId = UtilityHelper.Decreypt(_IPDmodel.BusinessUnitId);
            //string[] PropertyToValidate = { "FormulationQuantity", "Development", "ScaleUp", "Exhibit", "PlantQC", "Total", "MarketDetailsNewPortCGIDetails" };
            //Type type = _IPDmodel.GetType();
            //PropertyInfo[] props = type.GetProperties();
            //foreach (PropertyInfo p in props)
            //{
            //    if (PropertyToValidate.Contains(p.Name))
            //    {
            //        var value = p.GetValue(_IPDmodel);
            //        if (value == null)
            //        {
            //            ModelState.AddModelError(p.Name, "Required");
            //        }
            //    }
            //}
            bool IsSaveError = false;
            if (_IPDmodel.SaveType== "SaveDraft")
			{
                //save logic
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                _IPDmodel.LoggedInUserId= Convert.ToInt32(logUserId);

                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
               
                _IPDmodel.MarketDetailsNewPortCGIDetails = null; // set to null else it gives Error-406 while POST to API

                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.InsertUpdateAPIIPD, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(_IPDmodel))).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["SaveStatus"] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
                    ModelState.Clear();
                    IsSaveError = false;
                    return RedirectToAction("PBFAPI", "PIDF"); // return to PBFAPI List
                }
                else
                {
                    IsSaveError = true;
                }
            }
            //if(ModelState.ErrorCount > 0 || IsSaveError)
            //{
            //             TempData["SaveStatus"] = IsSaveError ? "Save Error Occured !" : "Some Feilds are Missing";
            //             // return back with Invalid Model
            //             _IPDmodel._commercialFormEntity = GetPIDFCommercialModel(_IPDmodel.Pidfid, _IPDmodel.BusinessUnitId);
            //             _IPDmodel.IPEvalution = GetModelForPIDForm(_IPDmodel.Pidfid, _IPDmodel.BusinessUnitId);
            //             _IPDmodel.ProjectName = _IPDmodel.IPEvalution.ProjectName;
            //             _IPDmodel.IPD_PatentDetailsList = _IPDmodel.IPEvalution.pidf_IPD_PatentDetailsEntities;
            //             _IPDmodel.Pidfid = UtilityHelper.Encrypt (_IPDmodel.Pidfid);
            //             _IPDmodel.BusinessUnitId = UtilityHelper.Encrypt(_IPDmodel.BusinessUnitId);
            //             return View(_IPDmodel);
            //         }
            TempData["SaveStatus"] = IsSaveError ? "Save Error Occured !" : "Some Feilds are Missing";
            return RedirectToAction("PBFAPI", "PIDF"); // return to PBFAPI List
        }

        [NonAction] // Get Model for View PIDForm.cshtml (IP EVolution)
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
        public PIDFormEntity GetModelForIPDForm(string pidfid, string bussnessId)
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
            catch (Exception ex)
            {
                throw ex;
            }
            return oPIDForm;
        }
        #endregion




    }
}
