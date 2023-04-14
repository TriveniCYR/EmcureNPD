using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmcureNPD.Web.Controllers
{
    public class MastersController : BaseController
    {
        public IActionResult MasterFormulation()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterPackagingType()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterProductType()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterUnitofMeasurement()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterWorkflow()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterBusinessUnit()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterDepartment()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult RoleManagement()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterDosageForm()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterOral()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterAPISourcing()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterPlant()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterExipient()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterCountry()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterFormRNDDivision()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterAnalyticalGL()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterBatchSizeNumber()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterProductStrength()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterBERequirement()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterDIA()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterUser()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterMarketExtension()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterTransformForm()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterExtensionApplication()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterActivityType()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterFillingExpense()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterRegion()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterCurrency()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
    }
}