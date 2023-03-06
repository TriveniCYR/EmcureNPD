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
    public class MedicalController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        //private IHostingEnvironment _env;
        #endregion

        public MedicalController(IConfiguration configuration,// IHostingEnvironment env,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
        }

        [HttpGet]
        public IActionResult Medical(string pidfid)
        {
            ViewBag.id = pidfid;
            ViewBag.baseUrl = _cofiguration.GetSection("Apiconfig").GetSection("baseurl").Value;
            PIDFMedicalViewModel oMedical = new();
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
                oMedical = GetModelForMedicalForm(pidfid);
                if (oMedical.PidfmedicalId <= 0)
                {
                    oMedical.MedicalOpinion = 1;
                }
                return View(oMedical);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("MedicalList");
            }
        }
        private PIDFMedicalViewModel GetModelForMedicalForm(string pidfid)
        {
            PIDFMedicalViewModel oMedical = new();
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
                    oMedical = data._object;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oMedical;
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

                //var response = httpClient.PostAsync($"/api/Medical/PIDMedicalForm", form).Result;
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
					return RedirectToAction("Medical", "Medical", new { pidfid = id });
				}
            }
            else
            {
                return View(medicalEntity);
            }
        }

        [HttpGet]
        public IActionResult ProjectManagement(string pidfid, string bussnessId)
        {
            return View();
        }
    }
}