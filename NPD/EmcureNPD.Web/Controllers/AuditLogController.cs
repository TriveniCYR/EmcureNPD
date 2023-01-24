using EmcureNPD.Business.Models;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
                return View();
            //}
        }
    }
}
