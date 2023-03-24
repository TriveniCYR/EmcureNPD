using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class AuditLogController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        #endregion

        public AuditLogController(IConfiguration configuration)
        {
            _cofiguration = configuration;
        }

        public IActionResult AuditLogs()
        {
            //AuditLogEntity auditLogEntity = new AuditLogEntity();
            //HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            //APIRepository objapi = new(_cofiguration);
            //HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAllAuditlog, HttpMethod.Get, token).Result;

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
            //    var data = JsonConvert.DeserializeObject<APIResponseEntity<List<AuditLogEntity>>>(jsonResponse);
            //    auditLogEntity.masterAuditLogEntities = data._object;
            //    return View(auditLogEntity);
            //}
            //else
            //{
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
            if (objPermssion == null || !objPermssion.View)
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            return View();
            //}
        }
		public ActionResult AuditLogPartialView(DateTime createdDate, string createdBy,string log)
		{
            ViewBag.log = JsonConvert.DeserializeObject(log);
            if(createdBy=="null" || createdBy==null || createdBy == " ")
            {
                createdBy = "";
            }
            //createdBy = !string.IsNullOrEmpty(createdBy) ? createdBy : "";
		    var Model = new AuditLogEntity { CreatedDate = createdDate, CreatedBy = createdBy };
			return PartialView("_AuditLogPartialView", Model);
		}
	}
}
