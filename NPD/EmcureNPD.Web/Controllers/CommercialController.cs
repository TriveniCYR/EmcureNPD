using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;

namespace EmcureNPD.Web.Controllers
{
    public class CommercialController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        private readonly IHelper _helper;
        private bool IsCommercialController;

        #endregion Properties

        public CommercialController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,
            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;
            _helper = helper;
        }

        public IActionResult PIDFCommercial()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PIDFCommerciaLDetails(string pidfid, string bui, int? IsView, bool _Partial = false)
        {
            ModelState.Clear();
            PIDFCommercialEntity oPIDForm = new();
            try
            {
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                //if (!(IsView == 1))
                //{
                //    string PIDFID = UtilityHelper.Decreypt(pidfid);
                //    if (!_helper.IsAccessToPIDF((int)ModuleEnum.CommercialManagement, int.Parse(PIDFID)))
                //    {
                //        return RedirectToAction("PIDFList", "PIDF", new { ScreenId = Convert.ToString((int)EmcureNPD.Utility.Enums.PIDFScreen.Commercial) });
                //    }
                //}
                int rolId = _helper.GetLoggedInRoleId();
                RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.Commercial, rolId);
                if (!_Partial)
                {
                    if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit))
                    {
                        return RedirectToAction("AccessRestriction", "Home");
                    }
                }
                ViewBag.Access = objPermssion;
                oPIDForm = GetPIDFCommercialModel(pidfid, bui);
                oPIDForm.IsView = (IsView == null) ? 0 : (int)IsView;
                oPIDForm._Partial = _Partial;
                HttpResponseMessage responseMS = objapi.APICommunication(APIURLHelper.GetPIDFById + "/" + oPIDForm.Pidfid, HttpMethod.Get, token).Result;
                if (responseMS.IsSuccessStatusCode)
                {
                    string jsnRs = responseMS.Content.ReadAsStringAsync().Result;
                    var retPIDF = JsonConvert.DeserializeObject<APIResponseEntity<PIDFEntity>>(jsnRs);
                    ViewBag.StatusId = retPIDF._object.StatusId;
                }
                return View(oPIDForm);
            }
            catch (Exception e)
            {
                _helper.LogExceptions(e);
                ViewBag.errormessage = Convert.ToString(e.StackTrace);
                return View("~/Views/Account/Login");
            }
        }

        [NonAction]
        private PIDFCommercialEntity GetPIDFCommercialModel(string pidfid, string bui)
        {
            PIDFCommercialEntity oPIDForm = new();
            pidfid = UtilityHelper.Decreypt(pidfid);
            bui = UtilityHelper.Decreypt(bui);
            string AssignedBusinessUnit = _helper.GetAssignedBusinessUnit();
            oPIDForm.Pidfid = Convert.ToInt64(pidfid);
            oPIDForm.CreatedBy = _helper.GetLoggedInUserId();
            oPIDForm.BusinessUnitsByUser = AssignedBusinessUnit;
            oPIDForm.BusinessUnitId = Convert.ToInt32(bui);
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
                _helper.LogExceptions(ex);
                throw;
            }
        }
    }
}