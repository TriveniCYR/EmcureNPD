using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IHelper _helper;

        public ProjectController(IConfiguration configuration, IHelper helper)
        {
            _configuration = configuration;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult ProjectManagement(string pidfid, string bui, int? IsView)
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.Project, rolId);
            if (objPermssion == null || (!objPermssion.View && IsView == 1))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;

            ViewBag.IsView = (IsView == null) ? 0 : (int)IsView;
            var bid = long.Parse(UtilityHelper.Decreypt(bui));
            ViewBag.id = pidfid;
            ViewBag.bid = bid;
            ViewBag.uencbid = bui;
            return View();
        }

        [HttpPost]
        public IActionResult AddUpdateGanttTask([FromBody] ProjectTaskEntity addTask, string id = "", string act = "", string buid = "")
        {
            // addTask.ProjectTaskId = projecttaskid;

            if (addTask.ProjectTaskId > 0)
            {
                addTask.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
                addTask.ModifyBy = _helper.GetLoggedInUserId();
                addTask.ModifyDate = DateTime.Now;
                if (act == "Task") { }
                // addTask.TaskLevel = 1;
                else if (act == "SubTask") { }
                    //addTask.TaskLevel = 2;
            }
            else
            {
                if (act == "Task") { }
                    //addTask.TaskLevel = 1;
                else if (act == "SubTask") { }
                    //addTask.TaskLevel = 2;
                addTask.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
                addTask.CreatedBy = _helper.GetLoggedInUserId();
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
                return Json("success-" + data._Message.ToString());
            }
            else
            {
                TempData[UserHelper.ErrorMessage] = data._Message;
                ModelState.Clear();
                return Json("Error-" + data._Message.ToString());
            }
        }

        [HttpPost]
        public IActionResult AddUpdateTask(ProjectTaskEntity addTask, string id = "", string act = "", string buid = "")
        {
            // addTask.ProjectTaskId = projecttaskid;

            if (addTask.ProjectTaskId > 0)
            {
                addTask.ModifyBy = _helper.GetLoggedInUserId();
                addTask.ModifyDate = DateTime.Now;
            }
            else
            {
                if (act == "Task") { addTask.TaskLevel = 1; }
                else if (act == "SubTask") { addTask.TaskLevel = addTask.SubTaskLevel; }
                addTask.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
                addTask.CreatedBy = _helper.GetLoggedInUserId();
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
                return RedirectToAction("ProjectManagement", "Project", new { pidfid = UtilityHelper.Encrypt(addTask.Pidfid.ToString()), bui = buid });
            }
            else
            {
                TempData[UserHelper.ErrorMessage] = data._Message;
                ModelState.Clear();
                return RedirectToAction("ProjectManagement", "Project", new { pidfid = UtilityHelper.Encrypt(addTask.Pidfid.ToString()), bui = buid });
            }
        }

        public IActionResult Gantt(string pidfid)
        {
            ViewBag.id = pidfid;
            return View();
        }

        public IActionResult ProjectSummary(string pidfid)
        {
            ViewBag.id = pidfid;
            return View();
        }
    }
}