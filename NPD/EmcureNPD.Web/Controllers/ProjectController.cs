using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using Microsoft.Extensions.Configuration;
using EmcureNPD.Utility.Models;

namespace EmcureNPD.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult ProjectManagement(string pidfid, string bussnessId)
        {
            //int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            //RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
            //if (objPermssion == null || !objPermssion.View)
            //{
            //    return RedirectToAction("AccessRestriction", "Home");
            //}
            //ViewBag.Access = objPermssion;
            ViewBag.id = pidfid;
            return View();
        }
        [HttpPost]
        public IActionResult AddUpdateTask(ProjectTaskEntity addTask, string id = "", string act = "")
        {
           // addTask.ProjectTaskId = projecttaskid;
           
            if (addTask.ProjectTaskId > 0)
            {
                addTask.ModifyBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                addTask.ModifyDate = DateTime.Now;
            }
            else
            {
                if (act == "Task")
                    addTask.TaskLevel = 1;
                else if (act == "SubTask")
                    addTask.TaskLevel = 2;
                addTask.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
                addTask.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                addTask.CreatedDate = DateTime.Now;
            }

            HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
            APIRepository objapi = new(_configuration);
            string formData = JsonConvert.SerializeObject(addTask);

            HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.AddUpdateTask, HttpMethod.Post, token, new StringContent(formData)).Result;
            string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<APIResponseEntity<dynamic>>(jsonResponse);

            if (responseMessage.IsSuccessStatusCode)
            {
                TempData[UserHelper.SuccessMessage] = data._Message;
                return RedirectToAction("ProjectManagement", "Project", new {pidfid = UtilityHelper.Encrypt(addTask.Pidfid.ToString())});
            }
            else
            {
                TempData[UserHelper.ErrorMessage] = data._Message;
                ModelState.Clear();
                return RedirectToAction("ProjectManagement", "Project", new { pidfid = UtilityHelper.Encrypt(addTask.Pidfid.ToString())});
            }
        }
    }
}
