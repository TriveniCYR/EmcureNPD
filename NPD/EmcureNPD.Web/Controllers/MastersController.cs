using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
    public class MastersController : BaseController
    {
        #region Properties

        private readonly IHelper _helper;

        #endregion Properties

        public MastersController(IHelper helper)
        {
            _helper = helper;
        }

        public IActionResult MasterFormulation()
        {
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
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
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterDosage()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterFilingType()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterManufacturing()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterTestType()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterPackingType()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterPackSize()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.MasterManagement, rolId);
            if (objPermssion == null || !(objPermssion.View || objPermssion.Add || objPermssion.Edit || objPermssion.Delete))
            {
                return RedirectToAction("AccessRestriction", "Home");
            }
            ViewBag.Access = objPermssion;
            return View();
        }
        public IActionResult MasterExcipientRequirement()
        {
            int rolId = _helper.GetLoggedInRoleId();
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