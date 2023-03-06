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
            ViewBag.id = pidfid;
            return View();
        }
        [HttpPost]
        public IActionResult AddUpdateTask(string id, string act, ProjectTaskEntity addTask)
        {
            addTask.Pidfid = long.Parse(UtilityHelper.Decreypt(id));
            if (act == "Task")
                addTask.TaskLevel = 1;
            else
                addTask.TaskLevel = 2;
            if (addTask.ProjectTaskId > 0)
            {
                addTask.ModifyBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
                addTask.ModifyDate = DateTime.Now;
            }
            else
            {
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
                return RedirectToAction("PIDFList", "PIDF", new { ScreenId = (int)PIDFScreen.Project });
            }
            else
            {
                TempData[UserHelper.ErrorMessage] = data._Message;
                ModelState.Clear();
                return RedirectToAction("ProjectManagement", "Project");
            }
        }
    }
}
