using EmcureNPD.Business.Models;
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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

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
        #endregion

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
        public IActionResult IPD(string pidfid, string bui)
        {

            ModelState.Clear();
            IPDEntity oIPD = new();
            try
            {
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
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

                oIPD = GetModelForIPDForm(pidfid, bussnessId);

                return View(oIPD);
            }
            catch (Exception e)
            {
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
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
                if (objPermssion == null || (!objPermssion.Add && !objPermssion.Edit))
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }
                ViewBag.Access = objPermssion;
            
                oIPD = GetModelForIPDForm(pidfid.ToString(), bui.ToString());

                return PartialView("_IPDPartial", oIPD);
            }
            catch (Exception e)
            {
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
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
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
                    oIPD.LogInId = Convert.ToInt32(logUserId);
                    oIPD.BusinessUnitsByUser = _helper.GetAssignedBusinessUnit();
                    TempData["BusList"] = JsonConvert.SerializeObject(oIPD.MasterBusinessUnitEntities);

                    if (oIPD.pidf_IPD_PatentDetailsEntities == null || oIPD.pidf_IPD_PatentDetailsEntities.Count == 0)
                    {
                        oIPD.pidf_IPD_PatentDetailsEntities = new List<PIDF_IPD_PatentDetailsEntity>();
                        oIPD.pidf_IPD_PatentDetailsEntities.Add(new PIDF_IPD_PatentDetailsEntity() { PatentNumber = "1" });
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntities.Count;
                    }
                    else
                    {
                        oIPD.TotalParent = oIPD.pidf_IPD_PatentDetailsEntities.Count - 1;
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
                throw ex;
            }
            return oIPD;
        }
        public IActionResult IPDList()
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
        public IActionResult IPD(IPDEntity IPDEntity)
        {
            try
            {
                if (IPDEntity.SaveType == "Sv")
                    IPDEntity.StatusId = (Int32)Master_PIDFStatus.IPDSubmitted;
                else
                    IPDEntity.StatusId = (Int32)Master_PIDFStatus.IPDInProgress;

                IPDEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                
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
                TempData[UserHelper.ErrorMessage] = Convert.ToString(e.StackTrace);
                //ModelState.Clear();
                return View(nameof(IPDList));
            }
        }

        [HttpGet]
        public IActionResult Medical(string pidfid)
        {
            ViewBag.id = pidfid;
            ViewBag.baseUrl = _cofiguration.GetSection("Apiconfig").GetSection("baseurl").Value;
            PIDFMedicalViewModel oIPD = new();
            try
            {
                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(this.ControllerContext.ActionDescriptor.ActionName), rolId);
                if (objPermssion == null || !objPermssion.View)
                {
                    return RedirectToAction("AccessRestriction", "Home");

                }
                ViewBag.Access = objPermssion;
                pidfid = UtilityHelper.Decreypt(pidfid);
                oIPD = GetModelForMedicalForm(pidfid);
                return View(oIPD);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("IPDList");
            }
        }
        private PIDFMedicalViewModel GetModelForMedicalForm(string pidfid)
        {
            PIDFMedicalViewModel oIPD = new();
            try
            {

                string logUserId = Convert.ToString(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetMedicalFormdata + "/" + pidfid, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<PIDFMedicalViewModel>>(jsonResponse);
                    oIPD = data._object;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oIPD;
        }
        [HttpPost]
        public IActionResult Medical(string id, PIDFMedicalViewModel medicalEntity)
        {
            if (ModelState.IsValid)
            {
                medicalEntity.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
                medicalEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                string x = JsonConvert.SerializeObject(medicalEntity);

                var form = new MultipartFormDataContent();
                if (medicalEntity.File != null)
                {
                    foreach (IFormFile file in medicalEntity.File)
                    {
                        var fileStream = file.OpenReadStream();
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
                        form.Add(fileContent, "file", file.FileName);
                    }
                }

                form.Add(new StringContent(JsonConvert.SerializeObject(medicalEntity)), "Data");
                //var streamContent = new StreamContent(form.ReadAsStreamAsync().Result);
                //streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                //var httpClient = new HttpClient()
                //{
                //    BaseAddress = new Uri("https://localhost:44368")
                //};

                //var response = httpClient.PostAsync($"/api/IPD/PIDMedicalForm", form).Result;
                HttpResponseMessage responseMessage = objapi.APIComm(APIURLHelper.PIDMedicalForm, HttpMethod.Post, token, form).Result;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<APIResponseEntity<dynamic>>(jsonResponse);

                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData[UserHelper.SuccessMessage] = data._Message;
					return RedirectToAction("PIDFList", "PIDF", new { ScreenId = (int)PIDFScreen.Medical });
                }
                else
                {
                    TempData[UserHelper.ErrorMessage] = data._Message;
                    ModelState.Clear();
					return RedirectToAction("Medical", "IPD", new { pidfid = id });
				}
            }
            else
            {
                return View(medicalEntity);
            }
        }
    }
}