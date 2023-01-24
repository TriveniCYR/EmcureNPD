using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class RoleController : BaseController
    {

        #region Properties
        private readonly IConfiguration _cofiguration;
        #endregion


        public RoleController(IConfiguration configuration)
        {
            _cofiguration = configuration;
        }

        public IActionResult Roles()
        {
            ModelState.Clear();
            try
            {

                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new APIRepository(_cofiguration);
                MasterRoleEntity oRoleList = new MasterRoleEntity();
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAllRole, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<APIResponseEntity<List<MasterRoleEntity>>>(jsonResponse);
                    oRoleList.Roles = data._object;

                    return View(oRoleList);

                }
            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("Login");
            }
            return View();
        }


        public IActionResult RoleManage(int? roleId)
        {
            ViewBag.IsEdit = roleId != null;
            MasterRoleEntity MasterRole = new MasterRoleEntity();
            if (roleId == null)
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetAllModule + "/" + roleId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<APIResponseEntity<List<MasterModuleEntity>>>(jsonResponse);
                    MasterRole.MasterModules = data._object;
                    return View(MasterRole);
                }

                return View();
            }
            else
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.GetRoleById + "/" + roleId, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<APIResponseEntity<MasterRoleEntity>>(jsonResponse);
                    if (data._object is null)
                    {
                        return NotFound();
                    }
                    return View(data._object);
                }
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleManage(int roleId, MasterRoleEntity masterRole)
        {
            try
            {


                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.SaveRole, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(masterRole))).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    if(masterRole.RoleId>0)
                    {
                        UtilityHelper.RemoveModuleRole(masterRole.RoleId);

                        HttpResponseMessage resRoles = objapi.APICommunication(APIURLHelper.GetByPermisionRoleUsingRoleId + "/" + masterRole.RoleId, HttpMethod.Get, token).Result;
                        if (resRoles.IsSuccessStatusCode)
                        {
                            string rolJson = resRoles.Content.ReadAsStringAsync().Result;
                            var data = JsonConvert.DeserializeObject<APIResponseEntity<IEnumerable<RolePermissionModel>>>(rolJson);
                            UtilityHelper.AddModuleRole(masterRole.RoleId, data._object);                          
                        }
                    }

                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    ModelState.Clear();
                    return RedirectToAction(nameof(Roles));
                }


            }
            catch (Exception e)
            {
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                ModelState.Clear();
                return View(nameof(Roles));
            }
            ModelState.Clear();
            return RedirectToAction(nameof(Roles));
        }




    }
}
