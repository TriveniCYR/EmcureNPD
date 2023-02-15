using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Hubs;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static EmcureCERI.Web.Models.MasterModel;

namespace EmcureCERI.Web.Controllers
{
    [Authorize]
    public class MastersController : Controller
    {
        private readonly IConfiguration _config;
        IHostingEnvironment _env;
        private readonly IMasters _Masters;
        private readonly EmcureCERIDBContext _db;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        public MastersController(IMasters masters, IHubContext<NotificationHub> notificationHubContext, IConfiguration config, IHostingEnvironment env, IEmailService emailService, ISMTPService sMTPService)
        {
            this._Masters = masters;
            _db = new EmcureCERIDBContext();
            _config = config;
            this._env = env;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Master_Formulation()
        {
            return View();
        }

       [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("InsertFormulation")]
        [Obsolete]
        public ActionResult InsertFormulation(FormulationMaster formulationMaster)
        {
            if (ModelState.IsValid)
            {
                var formulationName = formulationMaster.FormulationName.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Formulation where x.Formulation.ToUpper() == formulationName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Formulation " + formulationMaster.FormulationName.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_Formulation tbl_Master_Formulation = new Tbl_Master_Formulation();
                    tbl_Master_Formulation.Id = Convert.ToInt32(formulationMaster.FormulationID);
                    tbl_Master_Formulation.Formulation = formulationMaster.FormulationName.Trim();
                    if(HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_Formulation.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_Formulation.IsActive = formulationMaster.IsActive;
                    }
                    //tbl_Master_Formulation.IsActive = formulationMaster.IsActive;
                    tbl_Master_Formulation.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Formulation.CreatedDate = DateTime.Today;

                    int data = _Masters.insertFormulation(tbl_Master_Formulation);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {                           
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "Formulation", formulationMaster.FormulationName.Trim(), "https://emprojects.emcure.com/Masters/Master_Formulation");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("UpdateFormulation")]
        public ActionResult UpdateFormulation(FormulationMaster formulationMaster)
        {
            if (ModelState.IsValid)
            {

                var formulationName = formulationMaster.FormulationName.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Formulation where x.Formulation.ToUpper() == formulationName select new { x.Id,x.Formulation }).ToList();

                if(chkduplicate.Count > 0)
                {
                    if (formulationMaster.FormulationID == chkduplicate[0].Id)
                    {
                        if (formulationName == chkduplicate[0].Formulation.ToUpper())
                        {
                            Tbl_Master_Formulation tbl_Master_Formulation = new Tbl_Master_Formulation();
                            tbl_Master_Formulation.Id = Convert.ToInt32(formulationMaster.FormulationID);
                            tbl_Master_Formulation.Formulation = formulationMaster.FormulationName;
                            tbl_Master_Formulation.IsActive = formulationMaster.IsActive;
                            tbl_Master_Formulation.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_Formulation.CreatedDate = DateTime.Today;

                            int data = _Masters.updateFormulation(tbl_Master_Formulation);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Formulation " + formulationMaster.FormulationName.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                       var displayMessage = "Formulation " + formulationMaster.FormulationName.Trim() + " is already exists in database.";
                        return Json(new { data = "fail" ,message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_Formulation tbl_Master_Formulation = new Tbl_Master_Formulation();
                    tbl_Master_Formulation.Id = Convert.ToInt32(formulationMaster.FormulationID);
                    tbl_Master_Formulation.Formulation = formulationMaster.FormulationName;
                    tbl_Master_Formulation.IsActive = formulationMaster.IsActive;
                    tbl_Master_Formulation.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Formulation.CreatedDate = DateTime.Today;

                    int data = _Masters.updateFormulation(tbl_Master_Formulation);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

                

                

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("DeleteFormulation")]
        public ActionResult DeleteFormulation(FormulationMaster formulationMaster)
        {
            //if (ModelState.IsValid)
            //{
                Tbl_Master_Formulation tbl_Master_Formulation = new Tbl_Master_Formulation();
                tbl_Master_Formulation.Id = Convert.ToInt32(formulationMaster.FormulationID);
                tbl_Master_Formulation.Formulation = formulationMaster.FormulationName;
                tbl_Master_Formulation.IsActive = formulationMaster.IsActive;
                tbl_Master_Formulation.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_Formulation.CreatedDate = DateTime.Today;

                int data = _Masters.deleteFormulation(tbl_Master_Formulation);

                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("GetSingleFormulationRecord")]
        public ActionResult GetSingleFormulationRecord(FormulationMaster formulationMaster)
        {
                IList<Tbl_Master_Formulation> List = new List<Tbl_Master_Formulation>();
                List = _Masters.GetFormulationSingleRecord(Convert.ToInt32(formulationMaster.FormulationID));
                return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("FormulationList")]
        public ActionResult FormulationList()
        {
            IList<Tbl_Master_Formulation> List = new List<Tbl_Master_Formulation>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterFormulation();
            }
            else
            {
                List = _Masters.GetMasterFormulationForuser();
            }
         
            return Json(new { data = List });
        }

        public IActionResult Master_ProductManufacture()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertProductManufacture")]
        [Obsolete]
        public ActionResult InsertProductManufacture(ProductManufactureMaster productManufactureMaster)
        {
            if (ModelState.IsValid)
            {
                var productManufactureName = productManufactureMaster.ProductManufacture.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ProductManufacture where x.ProductManufacture.ToUpper() == productManufactureName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Product Manufacture " + productManufactureMaster.ProductManufacture.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_ProductManufacture  tbl_Master_ProductManufacture = new Tbl_Master_ProductManufacture();
                    tbl_Master_ProductManufacture.Id = Convert.ToInt32(productManufactureMaster.ProductManufactureID);
                    tbl_Master_ProductManufacture.ProductManufacture = productManufactureMaster.ProductManufacture.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_ProductManufacture.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_ProductManufacture.IsActive = productManufactureMaster.IsActive;
                    }
                    //tbl_Master_ProductManufacture.IsActive = productManufactureMaster.IsActive;
                    tbl_Master_ProductManufacture.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProductManufacture.CreatedDate = DateTime.Today;

                    int data = _Masters.insertProductManufacture(tbl_Master_ProductManufacture);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "Product Manufacture", productManufactureMaster.ProductManufacture.Trim(), "https://emprojects.emcure.com/Master_ProductManufacture");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateProductManufacture")]
        public ActionResult UpdateProductManufacture(ProductManufactureMaster productManufactureMaster)
        {
            if (ModelState.IsValid)
            {
                var productManufactureName = productManufactureMaster.ProductManufacture.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ProductManufacture where x.ProductManufacture.ToUpper() == productManufactureName select new { x.Id, x.ProductManufacture }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (productManufactureMaster.ProductManufactureID == chkduplicate[0].Id)
                    {
                        if (productManufactureName == chkduplicate[0].ProductManufacture.ToUpper())
                        {

                            Tbl_Master_ProductManufacture tbl_Master_ProductManufacture = new Tbl_Master_ProductManufacture();
                            tbl_Master_ProductManufacture.Id = Convert.ToInt32(productManufactureMaster.ProductManufactureID);
                            tbl_Master_ProductManufacture.ProductManufacture = productManufactureMaster.ProductManufacture.Trim();
                            tbl_Master_ProductManufacture.IsActive = productManufactureMaster.IsActive;
                            tbl_Master_ProductManufacture.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_ProductManufacture.CreatedDate = DateTime.Today;

                            int data = _Masters.updateProductManufacture(tbl_Master_ProductManufacture);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Product Manufacture " + productManufactureMaster.ProductManufacture.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Product Manufacture " + productManufactureMaster.ProductManufacture.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_ProductManufacture tbl_Master_ProductManufacture = new Tbl_Master_ProductManufacture();
                    tbl_Master_ProductManufacture.Id = Convert.ToInt32(productManufactureMaster.ProductManufactureID);
                    tbl_Master_ProductManufacture.ProductManufacture = productManufactureMaster.ProductManufacture.Trim();
                    tbl_Master_ProductManufacture.IsActive = productManufactureMaster.IsActive;
                    tbl_Master_ProductManufacture.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ProductManufacture.CreatedDate = DateTime.Today;

                    int data = _Masters.updateProductManufacture(tbl_Master_ProductManufacture);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteProductManufacture")]
        public ActionResult DeleteProductManufacture(ProductManufactureMaster productManufactureMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_ProductManufacture tbl_Master_ProductManufacture = new Tbl_Master_ProductManufacture();
            tbl_Master_ProductManufacture.Id = Convert.ToInt32(productManufactureMaster.ProductManufactureID);
            tbl_Master_ProductManufacture.ProductManufacture = productManufactureMaster.ProductManufacture;
            tbl_Master_ProductManufacture.IsActive = productManufactureMaster.IsActive;
            tbl_Master_ProductManufacture.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_ProductManufacture.CreatedDate = DateTime.Today;

            int data = _Masters.deleteProductManufacture(tbl_Master_ProductManufacture);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleProductManufactureRecord")]
        public ActionResult GetSingleProductManufactureRecord(ProductManufactureMaster productManufactureMaster)
        {
            IList<Tbl_Master_ProductManufacture> List = new List<Tbl_Master_ProductManufacture>();
            List = _Masters.GetProductManufactureSingleRecord(Convert.ToInt32(productManufactureMaster.ProductManufactureID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("ProductManufactureList")]
        public ActionResult ProductManufactureList()
        {
            IList<Tbl_Master_ProductManufacture> List = new List<Tbl_Master_ProductManufacture>();

            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterProductManufacture();
            }
            else
            {
                List = _Masters.GetMasterProductManufactureForUser();
            }
            
            return Json(new { data = List });
        }

        public IActionResult Master_PackSize()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertPackSize")]
        [Obsolete]
        public ActionResult InsertPackSize(PackSizeMaster packSizeMaster)
        {
            if (ModelState.IsValid)
            {
                var packSizeName = packSizeMaster.PackSize.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_PackSize where x.PackSize.ToUpper() == packSizeName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Pack Size " + packSizeMaster.PackSize.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_PackSize tbl_Master_PackSize = new Tbl_Master_PackSize();
                    tbl_Master_PackSize.Id = Convert.ToInt32(packSizeMaster.PackSizeID);
                    tbl_Master_PackSize.PackSize = packSizeMaster.PackSize.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_PackSize.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_PackSize.IsActive = packSizeMaster.IsActive;
                    }
                    //tbl_Master_PackSize.IsActive = packSizeMaster.IsActive;
                    tbl_Master_PackSize.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_PackSize.CreatedDate = DateTime.Today;

                    int data = _Masters.insertPackSize(tbl_Master_PackSize);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "PackSize", packSizeMaster.PackSize.Trim(), "https://emprojects.emcure.com/Masters/Master_PackSize");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

       [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdatePackSize")]
        public ActionResult UpdatePackSize(PackSizeMaster packSizeMaster)
        {
            if (ModelState.IsValid)
            {
                var packSizeName = packSizeMaster.PackSize.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_PackSize where x.PackSize.ToUpper() == packSizeName select new { x.Id, x.PackSize }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (packSizeMaster.PackSizeID == chkduplicate[0].Id)
                    {
                        if (packSizeName == chkduplicate[0].PackSize.ToUpper())
                        {
                            Tbl_Master_PackSize tbl_Master_PackSize = new Tbl_Master_PackSize();
                            tbl_Master_PackSize.Id = Convert.ToInt32(packSizeMaster.PackSizeID);
                            tbl_Master_PackSize.PackSize = packSizeMaster.PackSize.Trim();
                            tbl_Master_PackSize.IsActive = packSizeMaster.IsActive;
                            tbl_Master_PackSize.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_PackSize.CreatedDate = DateTime.Today;

                            int data = _Masters.updatePackSize(tbl_Master_PackSize);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Pack Size " + packSizeMaster.PackSize.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Pack Size " + packSizeMaster.PackSize.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_PackSize tbl_Master_PackSize = new Tbl_Master_PackSize();
                    tbl_Master_PackSize.Id = Convert.ToInt32(packSizeMaster.PackSizeID);
                    tbl_Master_PackSize.PackSize = packSizeMaster.PackSize.Trim();
                    tbl_Master_PackSize.IsActive = packSizeMaster.IsActive;
                    tbl_Master_PackSize.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_PackSize.CreatedDate = DateTime.Today;

                    int data = _Masters.updatePackSize(tbl_Master_PackSize);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeletePackSize")]
        public ActionResult DeletePackSize(PackSizeMaster packSizeMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_PackSize tbl_Master_PackSize = new Tbl_Master_PackSize();
            tbl_Master_PackSize.Id = Convert.ToInt32(packSizeMaster.PackSizeID);
            tbl_Master_PackSize.PackSize = packSizeMaster.PackSize;
            tbl_Master_PackSize.IsActive = packSizeMaster.IsActive;
            tbl_Master_PackSize.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_PackSize.CreatedDate = DateTime.Today;

            int data = _Masters.deletePackSize(tbl_Master_PackSize);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSinglePackSizeRecord")]
        public ActionResult GetSinglePackSizeRecord(PackSizeMaster packSizeMaster)
        {
            IList<Tbl_Master_PackSize> List = new List<Tbl_Master_PackSize>();
            List = _Masters.GetPackSizeSingleRecord(Convert.ToInt32(packSizeMaster.PackSizeID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("PackSizeList")]
        public ActionResult PackSizeList()
        {

            IList<Tbl_Master_PackSize> List = new List<Tbl_Master_PackSize>();

            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterPackSize();
            }
            else
            {
                List = _Masters.GetMasterarPackSizeListForUser();
            }
               
            return Json(new { data = List });
        }
        
        //[HttpPost]
        //[ActionName("PackSizeUserList")]
        //public ActionResult PackSizeUserList()
        //{
        //    IList<Tbl_Master_PackSize> List = new List<Tbl_Master_PackSize>();


        //    List = _Masters.GetMasterallListForUser();
        //    return Json(new { data = List });
        //}
        public IActionResult Master_PackStyle()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertPackStyle")]
        [Obsolete]
        public ActionResult InsertPackStyle(PackStyleMaster packStyleMaster)
        {
            if (ModelState.IsValid)
            {
                var packStyleName = packStyleMaster.PackStyle.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_PackStyle where x.PackStyle.ToUpper() == packStyleName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Pack Style " + packStyleMaster.PackStyle.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_PackStyle tbl_Master_PackStyle = new Tbl_Master_PackStyle();
                    tbl_Master_PackStyle.Id = Convert.ToInt32(packStyleMaster.PackStyleID);
                    tbl_Master_PackStyle.PackStyle = packStyleMaster.PackStyle.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_PackStyle.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_PackStyle.IsActive = packStyleMaster.IsActive;
                    }
                    //tbl_Master_PackStyle.IsActive = packStyleMaster.IsActive;
                    tbl_Master_PackStyle.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_PackStyle.CreatedDate = DateTime.Today;

                    int data = _Masters.insertPackStyle(tbl_Master_PackStyle);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "PackStyle", packStyleMaster.PackStyle.Trim(), "https://emprojects.emcure.com/Masters/Master_PackStyle");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdatePackStyle")]
        public ActionResult UpdatePackStyle(PackStyleMaster packStyleMaster)
        {
            if (ModelState.IsValid)
            {
                var packStyleName = packStyleMaster.PackStyle.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_PackStyle where x.PackStyle.ToUpper() == packStyleName select new { x.Id, x.PackStyle }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (packStyleMaster.PackStyleID == chkduplicate[0].Id)
                    {
                        if (packStyleName == chkduplicate[0].PackStyle.ToUpper())
                        {
                            Tbl_Master_PackStyle tbl_Master_PackStyle = new Tbl_Master_PackStyle();
                            tbl_Master_PackStyle.Id = Convert.ToInt32(packStyleMaster.PackStyleID);
                            tbl_Master_PackStyle.PackStyle = packStyleMaster.PackStyle.Trim();
                            tbl_Master_PackStyle.IsActive = packStyleMaster.IsActive;
                            tbl_Master_PackStyle.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_PackStyle.CreatedDate = DateTime.Today;

                            int data = _Masters.updatePackStyle(tbl_Master_PackStyle);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Pack Style " + packStyleMaster.PackStyle.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Pack Style " + packStyleMaster.PackStyle.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_PackStyle tbl_Master_PackStyle = new Tbl_Master_PackStyle();
                    tbl_Master_PackStyle.Id = Convert.ToInt32(packStyleMaster.PackStyleID);
                    tbl_Master_PackStyle.PackStyle = packStyleMaster.PackStyle.Trim();
                    tbl_Master_PackStyle.IsActive = packStyleMaster.IsActive;
                    tbl_Master_PackStyle.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_PackStyle.CreatedDate = DateTime.Today;

                    int data = _Masters.updatePackStyle(tbl_Master_PackStyle);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeletePackStyle")]
        public ActionResult DeletePackStyle(PackStyleMaster packStyleMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_PackStyle tbl_Master_PackStyle = new Tbl_Master_PackStyle();
            tbl_Master_PackStyle.Id = Convert.ToInt32(packStyleMaster.PackStyleID);
            tbl_Master_PackStyle.PackStyle = packStyleMaster.PackStyle;
            tbl_Master_PackStyle.IsActive = packStyleMaster.IsActive;
            tbl_Master_PackStyle.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_PackStyle.CreatedDate = DateTime.Today;

            int data = _Masters.deletePackStyle(tbl_Master_PackStyle);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSinglePackStyleRecord")]
        public ActionResult GetSinglePackStyleRecord(PackStyleMaster packStyleMaster)
        {
            IList<Tbl_Master_PackStyle> List = new List<Tbl_Master_PackStyle>();
            List = _Masters.GetPackStyleSingleRecord(Convert.ToInt32(packStyleMaster.PackStyleID));
            return Json(new { data = List });
        }

       // [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("PackStyleList")]
        public ActionResult PackStyleList()
        {
            IList<Tbl_Master_PackStyle> List = new List<Tbl_Master_PackStyle>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                 List = _Masters.GetMasterPackStyle();
            }
            else
            {
                List = _Masters.GetMasterPackStyleForUser();
            }
           
            return Json(new { data = List });
        }


        public IActionResult Master_Strength()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertStrength")]
        [Obsolete]
        public ActionResult InsertStrength(StrengthMaster strengthMaster)
        {
            if (ModelState.IsValid)
            {
                var strengthName = strengthMaster.Strength.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Strength where x.Strength.ToUpper() == strengthName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Strength " + strengthMaster.Strength.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_Strength tbl_Master_Strength = new Tbl_Master_Strength();
                    tbl_Master_Strength.Id = Convert.ToInt32(strengthMaster.StrengthID);
                    tbl_Master_Strength.Strength = strengthMaster.Strength.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_Strength.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_Strength.IsActive = strengthMaster.IsActive;
                    }
                    //tbl_Master_Strength.IsActive = strengthMaster.IsActive;
                    tbl_Master_Strength.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Strength.CreatedDate = DateTime.Today;

                    int data = _Masters.insertStrength(tbl_Master_Strength);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "Strength", strengthMaster.Strength.Trim(), "https://emprojects.emcure.com/Masters/Master_Strength");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

       [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateStrength")]
        public ActionResult UpdateStrength(StrengthMaster strengthMaster)
        {
            if (ModelState.IsValid)
            {
                var strengthName = strengthMaster.Strength.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Strength where x.Strength.ToUpper() == strengthName select new { x.Id, x.Strength }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (strengthMaster.StrengthID == chkduplicate[0].Id)
                    {
                        if (strengthName == chkduplicate[0].Strength.ToUpper())
                        {
                            Tbl_Master_Strength tbl_Master_Strength = new Tbl_Master_Strength();
                            tbl_Master_Strength.Id = Convert.ToInt32(strengthMaster.StrengthID);
                            tbl_Master_Strength.Strength = strengthMaster.Strength.Trim();
                            tbl_Master_Strength.IsActive = strengthMaster.IsActive;
                            tbl_Master_Strength.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_Strength.CreatedDate = DateTime.Today;

                            int data = _Masters.updateStrength(tbl_Master_Strength);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Strength " + strengthMaster.Strength.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Strength " + strengthMaster.Strength.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_Strength tbl_Master_Strength = new Tbl_Master_Strength();
                    tbl_Master_Strength.Id = Convert.ToInt32(strengthMaster.StrengthID);
                    tbl_Master_Strength.Strength = strengthMaster.Strength.Trim();
                    tbl_Master_Strength.IsActive = strengthMaster.IsActive;
                    tbl_Master_Strength.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Strength.CreatedDate = DateTime.Today;

                    int data = _Masters.updateStrength(tbl_Master_Strength);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteStrength")]
        public ActionResult DeleteStrength(StrengthMaster strengthMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_Strength tbl_Master_Strength = new Tbl_Master_Strength();
            tbl_Master_Strength.Id = Convert.ToInt32(strengthMaster.StrengthID);
            tbl_Master_Strength.Strength = strengthMaster.Strength;
            tbl_Master_Strength.IsActive = strengthMaster.IsActive;
            tbl_Master_Strength.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_Strength.CreatedDate = DateTime.Today;

            int data = _Masters.deleteStrength(tbl_Master_Strength);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

       [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleStrengthRecord")]
        public ActionResult GetSingleStrengthRecord(StrengthMaster strengthMaster)
        {
            IList<Tbl_Master_Strength> List = new List<Tbl_Master_Strength>();
            List = _Masters.GetStrengthSingleRecord(Convert.ToInt32(strengthMaster.StrengthID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("StrengthList")]
        public ActionResult StrengthList()
        {
            IList<Tbl_Master_Strength> List = new List<Tbl_Master_Strength>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {

                List = _Masters.GetMasterStrength();
            }
            else
            {
                List = _Masters.GetMasterStrengthForUser();
            }

            return Json(new { data = List });
        }

        public IActionResult Master_Modeofshipment()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertModeofshipment")]
        [Obsolete]
        public ActionResult InsertModeofshipment(ModeofshipmentMaster modeofShipmentMaster)
        {
            if (ModelState.IsValid)
            {
                var shipmentName = modeofShipmentMaster.Modeofshipment.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Modeofshipment where x.Modeofshipment.ToUpper() == shipmentName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Mode of shipment " + modeofShipmentMaster.Modeofshipment.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_Modeofshipment tbl_Master_Modeofshipment = new Tbl_Master_Modeofshipment();
                    tbl_Master_Modeofshipment.Id = Convert.ToInt32(modeofShipmentMaster.ModeofshipmentID);
                    tbl_Master_Modeofshipment.Modeofshipment = modeofShipmentMaster.Modeofshipment.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_Modeofshipment.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_Modeofshipment.IsActive = modeofShipmentMaster.IsActive;
                    }
                    //tbl_Master_Modeofshipment.IsActive = modeofShipmentMaster.IsActive;
                    tbl_Master_Modeofshipment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Modeofshipment.CreatedDate = DateTime.Today;

                    int data = _Masters.insertModeofshipment(tbl_Master_Modeofshipment);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "ModeOfShipment", modeofShipmentMaster.Modeofshipment.Trim(), "https://emprojects.emcure.com/Masters/Master_Modeofshipment");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateModeofshipment")]
        public ActionResult UpdateModeofshipment(ModeofshipmentMaster modeofShipmentMaster)
        {
            if (ModelState.IsValid)
            {
                var shipmentName = modeofShipmentMaster.Modeofshipment.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Modeofshipment where x.Modeofshipment.ToUpper() == shipmentName select new { x.Id, x.Modeofshipment }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (modeofShipmentMaster.ModeofshipmentID == chkduplicate[0].Id)
                    {
                        if (shipmentName == chkduplicate[0].Modeofshipment.ToUpper())
                        {
                            Tbl_Master_Modeofshipment tbl_Master_Modeofshipment = new Tbl_Master_Modeofshipment();
                            tbl_Master_Modeofshipment.Id = Convert.ToInt32(modeofShipmentMaster.ModeofshipmentID);
                            tbl_Master_Modeofshipment.Modeofshipment = modeofShipmentMaster.Modeofshipment.Trim();
                            tbl_Master_Modeofshipment.IsActive = modeofShipmentMaster.IsActive;
                            tbl_Master_Modeofshipment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_Modeofshipment.CreatedDate = DateTime.Today;

                            int data = _Masters.updateModeofshipment(tbl_Master_Modeofshipment);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Mode of shipment " + modeofShipmentMaster.Modeofshipment.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Mode of shipment " + modeofShipmentMaster.Modeofshipment.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_Modeofshipment tbl_Master_Modeofshipment = new Tbl_Master_Modeofshipment();
                    tbl_Master_Modeofshipment.Id = Convert.ToInt32(modeofShipmentMaster.ModeofshipmentID);
                    tbl_Master_Modeofshipment.Modeofshipment = modeofShipmentMaster.Modeofshipment.Trim();
                    tbl_Master_Modeofshipment.IsActive = modeofShipmentMaster.IsActive;
                    tbl_Master_Modeofshipment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Modeofshipment.CreatedDate = DateTime.Today;

                    int data = _Masters.updateModeofshipment(tbl_Master_Modeofshipment);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteModeofshipment")]
        public ActionResult DeleteModeofshipment(ModeofshipmentMaster modeofShipmentMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_Modeofshipment tbl_Master_Modeofshipment = new Tbl_Master_Modeofshipment();
            tbl_Master_Modeofshipment.Id = Convert.ToInt32(modeofShipmentMaster.ModeofshipmentID);
            tbl_Master_Modeofshipment.Modeofshipment = modeofShipmentMaster.Modeofshipment;
            tbl_Master_Modeofshipment.IsActive = modeofShipmentMaster.IsActive;
            tbl_Master_Modeofshipment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_Modeofshipment.CreatedDate = DateTime.Today;

            int data = _Masters.deleteModeofshipment(tbl_Master_Modeofshipment);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleModeofshipmentRecord")]
        public ActionResult GetSingleModeofshipmentRecord(ModeofshipmentMaster modeofShipmentMaster)
        {
            IList<Tbl_Master_Modeofshipment> List = new List<Tbl_Master_Modeofshipment>();
            List = _Masters.GetModeofshipmentSingleRecord(Convert.ToInt32(modeofShipmentMaster.ModeofshipmentID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("ModeofshipmentList")]
        public ActionResult ModeofshipmentList()
        {
            IList<Tbl_Master_Modeofshipment> List = new List<Tbl_Master_Modeofshipment>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterModeofshipment();
            }
            else
            {
                List = _Masters.GetMasterModeofshipmentForUser();
            }

           
            return Json(new { data = List });
        }


        public IActionResult Master_ModeofFeesPayment()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertModeofFeesPayment")]
        [Obsolete]
        public ActionResult InsertModeofFeesPayment(ModeofFeesPaymentMaster modeofFeesPaymentMaster)
        {
            if (ModelState.IsValid)
            {
                var paymentName = modeofFeesPaymentMaster.ModeofFeesPayment.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ModeofFeesPayment where x.ModeofFeesPayment.ToUpper() == paymentName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Mode of Fees Payment " + modeofFeesPaymentMaster.ModeofFeesPayment.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_ModeofFeesPayment tbl_Master_ModeofFeesPayment = new Tbl_Master_ModeofFeesPayment();
                    tbl_Master_ModeofFeesPayment.Id = Convert.ToInt32(modeofFeesPaymentMaster.ModeofFeesPaymentID);
                    tbl_Master_ModeofFeesPayment.ModeofFeesPayment = modeofFeesPaymentMaster.ModeofFeesPayment.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_ModeofFeesPayment.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_ModeofFeesPayment.IsActive = modeofFeesPaymentMaster.IsActive;
                    }
                    //tbl_Master_ModeofFeesPayment.IsActive = modeofFeesPaymentMaster.IsActive;
                    tbl_Master_ModeofFeesPayment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ModeofFeesPayment.CreatedDate = DateTime.Today;

                    int data = _Masters.insertModeofFeesPayment(tbl_Master_ModeofFeesPayment);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "ModeOfFees", modeofFeesPaymentMaster.ModeofFeesPayment.Trim(), "https://emprojects.emcure.com/Masters/Master_ModeofFeesPayment");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateModeofFeesPayment")]
        public ActionResult UpdateModeofFeesPayment(ModeofFeesPaymentMaster modeofFeesPaymentMaster)
        {
            if (ModelState.IsValid)
            {
                var paymentName = modeofFeesPaymentMaster.ModeofFeesPayment.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ModeofFeesPayment where x.ModeofFeesPayment.ToUpper() == paymentName select new { x.Id, x.ModeofFeesPayment }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (modeofFeesPaymentMaster.ModeofFeesPaymentID == chkduplicate[0].Id)
                    {
                        if (paymentName == chkduplicate[0].ModeofFeesPayment.ToUpper())
                        {
                            Tbl_Master_ModeofFeesPayment tbl_Master_ModeofFeesPayment = new Tbl_Master_ModeofFeesPayment();
                            tbl_Master_ModeofFeesPayment.Id = Convert.ToInt32(modeofFeesPaymentMaster.ModeofFeesPaymentID);
                            tbl_Master_ModeofFeesPayment.ModeofFeesPayment = modeofFeesPaymentMaster.ModeofFeesPayment.Trim();
                            tbl_Master_ModeofFeesPayment.IsActive = modeofFeesPaymentMaster.IsActive;
                            tbl_Master_ModeofFeesPayment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_ModeofFeesPayment.CreatedDate = DateTime.Today;

                            int data = _Masters.updateModeofFeesPayment(tbl_Master_ModeofFeesPayment);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Mode of Fees Payment " + modeofFeesPaymentMaster.ModeofFeesPayment.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Mode of Fees Payment " + modeofFeesPaymentMaster.ModeofFeesPayment.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_ModeofFeesPayment tbl_Master_ModeofFeesPayment = new Tbl_Master_ModeofFeesPayment();
                    tbl_Master_ModeofFeesPayment.Id = Convert.ToInt32(modeofFeesPaymentMaster.ModeofFeesPaymentID);
                    tbl_Master_ModeofFeesPayment.ModeofFeesPayment = modeofFeesPaymentMaster.ModeofFeesPayment.Trim();
                    tbl_Master_ModeofFeesPayment.IsActive = modeofFeesPaymentMaster.IsActive;
                    tbl_Master_ModeofFeesPayment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ModeofFeesPayment.CreatedDate = DateTime.Today;

                    int data = _Masters.updateModeofFeesPayment(tbl_Master_ModeofFeesPayment);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteModeofFeesPayment")]
        public ActionResult DeleteModeofFeesPayment(ModeofFeesPaymentMaster modeofFeesPaymentMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_ModeofFeesPayment tbl_Master_ModeofFeesPayment = new Tbl_Master_ModeofFeesPayment();
            tbl_Master_ModeofFeesPayment.Id = Convert.ToInt32(modeofFeesPaymentMaster.ModeofFeesPaymentID);
            tbl_Master_ModeofFeesPayment.ModeofFeesPayment = modeofFeesPaymentMaster.ModeofFeesPayment;
            tbl_Master_ModeofFeesPayment.IsActive = modeofFeesPaymentMaster.IsActive;
            tbl_Master_ModeofFeesPayment.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_ModeofFeesPayment.CreatedDate = DateTime.Today;

            int data = _Masters.deleteModeofFeesPayment(tbl_Master_ModeofFeesPayment);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleModeofFeesPaymentRecord")]
        public ActionResult GetSingleModeofFeesPaymentRecord(ModeofFeesPaymentMaster modeofFeesPaymentMaster)
        {
            IList<Tbl_Master_ModeofFeesPayment> List = new List<Tbl_Master_ModeofFeesPayment>();
            List = _Masters.GetModeofFeesPaymentSingleRecord(Convert.ToInt32(modeofFeesPaymentMaster.ModeofFeesPaymentID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("ModeofFeesPaymentList")]
        public ActionResult ModeofFeesPaymentList()
        {
            IList<Tbl_Master_ModeofFeesPayment> List = new List<Tbl_Master_ModeofFeesPayment>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterModeofFeesPayment();
            }
            else
            {
                List = _Masters.GetMasterModeofFeesPaymentForUser();
            }
           
            return Json(new { data = List });
        }


        public IActionResult Master_Incoterms()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertIncoterms")]
        [Obsolete]
        public ActionResult InsertIncoterms(IncotermsMaster incotermsMaster)
        {
            if (ModelState.IsValid)
            {
                var incotermsName = incotermsMaster.Incoterms.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Incoterms where x.Incoterms.ToUpper() == incotermsName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "IncotermsName " + incotermsMaster.Incoterms.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_Incoterms tbl_Master_Incoterms = new Tbl_Master_Incoterms();
                    tbl_Master_Incoterms.Id = Convert.ToInt32(incotermsMaster.IncotermsID);
                    tbl_Master_Incoterms.Incoterms = incotermsMaster.Incoterms.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_Incoterms.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_Incoterms.IsActive = incotermsMaster.IsActive;
                    }
                    //tbl_Master_Incoterms.IsActive = incotermsMaster.IsActive;
                    tbl_Master_Incoterms.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Incoterms.CreatedDate = DateTime.Today;

                    int data = _Masters.insertIncoterms(tbl_Master_Incoterms);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "IncoTerms", incotermsMaster.Incoterms.Trim(), "https://emprojects.emcure.com/Masters/Master_Incoterms");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateIncoterms")]
        public ActionResult UpdateIncoterms(IncotermsMaster incotermsMaster)
        {
            if (ModelState.IsValid)
            {
                var incotermsName = incotermsMaster.Incoterms.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_Incoterms where x.Incoterms.ToUpper() == incotermsName select new { x.Id, x.Incoterms }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (incotermsMaster.IncotermsID == chkduplicate[0].Id)
                    {
                        if (incotermsName == chkduplicate[0].Incoterms.ToUpper())
                        {
                            Tbl_Master_Incoterms tbl_Master_Incoterms = new Tbl_Master_Incoterms();
                            tbl_Master_Incoterms.Id = Convert.ToInt32(incotermsMaster.IncotermsID);
                            tbl_Master_Incoterms.Incoterms = incotermsMaster.Incoterms.Trim();
                            tbl_Master_Incoterms.IsActive = incotermsMaster.IsActive;
                            tbl_Master_Incoterms.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_Incoterms.CreatedDate = DateTime.Today;

                            int data = _Masters.updateIncoterms(tbl_Master_Incoterms);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Incoterms " + incotermsMaster.Incoterms.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Incoterms " + incotermsMaster.Incoterms.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_Incoterms tbl_Master_Incoterms = new Tbl_Master_Incoterms();
                    tbl_Master_Incoterms.Id = Convert.ToInt32(incotermsMaster.IncotermsID);
                    tbl_Master_Incoterms.Incoterms = incotermsMaster.Incoterms.Trim();
                    tbl_Master_Incoterms.IsActive = incotermsMaster.IsActive;
                    tbl_Master_Incoterms.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_Incoterms.CreatedDate = DateTime.Today;

                    int data = _Masters.updateIncoterms(tbl_Master_Incoterms);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteIncoterms")]
        public ActionResult DeleteIncoterms(IncotermsMaster incotermsMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_Incoterms tbl_Master_Incoterms = new Tbl_Master_Incoterms();
            tbl_Master_Incoterms.Id = Convert.ToInt32(incotermsMaster.IncotermsID);
            tbl_Master_Incoterms.Incoterms = incotermsMaster.Incoterms;
            tbl_Master_Incoterms.IsActive = incotermsMaster.IsActive;
            tbl_Master_Incoterms.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_Incoterms.CreatedDate = DateTime.Today;

            int data = _Masters.deleteIncoterms(tbl_Master_Incoterms);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleIncotermsRecord")]
        public ActionResult GetSingleIncotermsRecord(IncotermsMaster incotermsMaster)
        {
            IList<Tbl_Master_Incoterms> List = new List<Tbl_Master_Incoterms>();
            List = _Masters.GetIncotermsSingleRecord(Convert.ToInt32(incotermsMaster.IncotermsID));
            return Json(new { data = List });
        }

       // [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("IncotermsList")]
        public ActionResult IncotermsList()
        {
            IList<Tbl_Master_Incoterms> List = new List<Tbl_Master_Incoterms>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterIncoterms();
            }
            else
            {
                List = _Masters.GetMasterIncotermsForUser();
            }
           
            return Json(new { data = List });
        }

        public IActionResult Master_DossierTemplate()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertDossierTemplate")]
        [Obsolete]
        public ActionResult InsertDossierTemplate(DossierTemplateMaster dossierTemplateMaster)
        {
            if (ModelState.IsValid)
            {
                var dossierTemplateName = dossierTemplateMaster.DossierTemplate.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_DossierTemplate where x.DossierTemplate.ToUpper() == dossierTemplateName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Dossier Template Name " + dossierTemplateMaster.DossierTemplate.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_DossierTemplate tbl_Master_DossierTemplate = new Tbl_Master_DossierTemplate();
                    tbl_Master_DossierTemplate.Id = Convert.ToInt32(dossierTemplateMaster.DossierTemplateID);
                    tbl_Master_DossierTemplate.DossierTemplate = dossierTemplateMaster.DossierTemplate.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_DossierTemplate.IsActive = false;
                    }
                    else 
                    {
                        tbl_Master_DossierTemplate.IsActive = dossierTemplateMaster.IsActive;
                    }
                    //tbl_Master_DossierTemplate.IsActive = dossierTemplateMaster.IsActive;
                    tbl_Master_DossierTemplate.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_DossierTemplate.CreatedDate = DateTime.Today;

                    int data = _Masters.insertDossierTemplate(tbl_Master_DossierTemplate);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "DossierTemplate", dossierTemplateMaster.DossierTemplate.Trim(), "https://emprojects.emcure.com/Masters/Master_DossierTemplate");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateDossierTemplate")]
        public ActionResult UpdateDossierTemplate(DossierTemplateMaster dossierTemplateMaster)
        {
            if (ModelState.IsValid)
            {
                var dossierTemplateName = dossierTemplateMaster.DossierTemplate.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_DossierTemplate where x.DossierTemplate.ToUpper() == dossierTemplateName select new { x.Id, x.DossierTemplate }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (dossierTemplateMaster.DossierTemplateID == chkduplicate[0].Id)
                    {
                        if (dossierTemplateName == chkduplicate[0].DossierTemplate.ToUpper())
                        {
                            Tbl_Master_DossierTemplate tbl_Master_DossierTemplate = new Tbl_Master_DossierTemplate();
                            tbl_Master_DossierTemplate.Id = Convert.ToInt32(dossierTemplateMaster.DossierTemplateID);
                            tbl_Master_DossierTemplate.DossierTemplate = dossierTemplateMaster.DossierTemplate.Trim();
                            tbl_Master_DossierTemplate.IsActive = dossierTemplateMaster.IsActive;
                            tbl_Master_DossierTemplate.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_DossierTemplate.CreatedDate = DateTime.Today;

                            int data = _Masters.updateDossierTemplate(tbl_Master_DossierTemplate);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Dossier Template " + dossierTemplateMaster.DossierTemplate.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Dossier Template " + dossierTemplateMaster.DossierTemplate.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_DossierTemplate tbl_Master_DossierTemplate = new Tbl_Master_DossierTemplate();
                    tbl_Master_DossierTemplate.Id = Convert.ToInt32(dossierTemplateMaster.DossierTemplateID);
                    tbl_Master_DossierTemplate.DossierTemplate = dossierTemplateMaster.DossierTemplate.Trim();
                    tbl_Master_DossierTemplate.IsActive = dossierTemplateMaster.IsActive;
                    tbl_Master_DossierTemplate.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_DossierTemplate.CreatedDate = DateTime.Today;

                    int data = _Masters.updateDossierTemplate(tbl_Master_DossierTemplate);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteDossierTemplate")]
        public ActionResult DeleteDossierTemplate(DossierTemplateMaster dossierTemplateMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_DossierTemplate tbl_Master_DossierTemplate = new Tbl_Master_DossierTemplate();
            tbl_Master_DossierTemplate.Id = Convert.ToInt32(dossierTemplateMaster.DossierTemplateID);
            tbl_Master_DossierTemplate.DossierTemplate = dossierTemplateMaster.DossierTemplate;
            tbl_Master_DossierTemplate.IsActive = dossierTemplateMaster.IsActive;
            tbl_Master_DossierTemplate.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_DossierTemplate.CreatedDate = DateTime.Today;

            int data = _Masters.deleteDossierTemplate(tbl_Master_DossierTemplate);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleDossierTemplateRecord")]
        public ActionResult GetSingleDossierTemplateRecord(DossierTemplateMaster dossierTemplateMaster)
        {
            IList<Tbl_Master_DossierTemplate> List = new List<Tbl_Master_DossierTemplate>();
            List = _Masters.GetDossierTemplateSingleRecord(Convert.ToInt32(dossierTemplateMaster.DossierTemplateID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DossierTemplateList")]
        public ActionResult DossierTemplateList()
        {
            IList<Tbl_Master_DossierTemplate> List = new List<Tbl_Master_DossierTemplate>();

            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterDossierTemplate();
            }
            else
            {
                List = _Masters.GetMasterDossierTemplateFouUser();
            }
           
            return Json(new { data = List });
        }


        public IActionResult Master_APISite()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertAPISite")]
        [Obsolete]
        public ActionResult InsertAPISite(APISiteMaster aPISiteMaster)
        {
            if (ModelState.IsValid)
            {
                var aPISiteName = aPISiteMaster.APISite.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_APISite where x.APISite.ToUpper() == aPISiteName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "API Site Name " + aPISiteMaster.APISite.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_APISite tbl_Master_APISite = new Tbl_Master_APISite();
                    tbl_Master_APISite.APIID = Convert.ToInt32(aPISiteMaster.APISiteID);
                    tbl_Master_APISite.APISite = aPISiteMaster.APISite.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_APISite.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_APISite.IsActive = aPISiteMaster.IsActive;
                    }
                    //tbl_Master_APISite.IsActive = aPISiteMaster.IsActive;
                    tbl_Master_APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_APISite.CreatedDate = DateTime.Today;

                    int data = _Masters.insertAPISite(tbl_Master_APISite);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "APISite", aPISiteMaster.APISite.Trim(), "https://emprojects.emcure.com/Masters/Master_APISite");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateAPISite")]
        public ActionResult UpdateAPISite(APISiteMaster aPISiteMaster)
        {
            if (ModelState.IsValid)
            {
                var aPISiteName = aPISiteMaster.APISite.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_APISite where x.APISite.ToUpper() == aPISiteName select new { x.APIID, x.APISite }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (aPISiteMaster.APISiteID == chkduplicate[0].APIID)
                    {
                        if (aPISiteName == chkduplicate[0].APISite.ToUpper())
                        {
                            Tbl_Master_APISite tbl_Master_APISite = new Tbl_Master_APISite();
                            tbl_Master_APISite.APIID = Convert.ToInt32(aPISiteMaster.APISiteID);
                            tbl_Master_APISite.APISite = aPISiteMaster.APISite.Trim();
                            tbl_Master_APISite.IsActive = aPISiteMaster.IsActive;
                            tbl_Master_APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_APISite.CreatedDate = DateTime.Today;

                            int data = _Masters.updateAPISite(tbl_Master_APISite);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "API Site " + aPISiteMaster.APISite.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "API Site " + aPISiteMaster.APISite.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_APISite tbl_Master_APISite = new Tbl_Master_APISite();
                    tbl_Master_APISite.APIID = Convert.ToInt32(aPISiteMaster.APISiteID);
                    tbl_Master_APISite.APISite = aPISiteMaster.APISite.Trim();
                    tbl_Master_APISite.IsActive = aPISiteMaster.IsActive;
                    tbl_Master_APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_APISite.CreatedDate = DateTime.Today;

                    int data = _Masters.updateAPISite(tbl_Master_APISite);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteAPISite")]
        public ActionResult DeleteAPISite(APISiteMaster aPISiteMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_APISite tbl_Master_APISite = new Tbl_Master_APISite();
            tbl_Master_APISite.APIID = Convert.ToInt32(aPISiteMaster.APISiteID);
            tbl_Master_APISite.APISite = aPISiteMaster.APISite;
            tbl_Master_APISite.IsActive = aPISiteMaster.IsActive;
            tbl_Master_APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_APISite.CreatedDate = DateTime.Today;

            int data = _Masters.deleteAPISite(tbl_Master_APISite);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleAPISiteRecord")]
        public ActionResult GetSingleAPISiteRecord(APISiteMaster aPISiteMaster)
        {
            IList<Tbl_Master_APISite> List = new List<Tbl_Master_APISite>();
            List = _Masters.GetAPISiteSingleRecord(Convert.ToInt32(aPISiteMaster.APISiteID));
            return Json(new { data = List });
        }

       // [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("APISiteList")]
        public ActionResult APISiteList()
        {
            IList<Tbl_Master_APISite> List = new List<Tbl_Master_APISite>();

            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterAPISite();
            }
            else
            {
                List = _Masters.GetMasterAPISiteForUser();
            }
        
            return Json(new { data = List });
        }

        public IActionResult Master_ManufacturingSite()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertManufacturingSite")]
        [Obsolete]
        public ActionResult InsertManufacturingSite(ManufacturingSiteMaster manufacturingSiteMaster)
        {
            if (ModelState.IsValid)
            {
                var manufacturingSiteName = manufacturingSiteMaster.ManufacturingSite.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ManufacturingSite where x.ManufacturingSite.ToUpper() == manufacturingSiteName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Manufacturing Site Name " + manufacturingSiteMaster.ManufacturingSite.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_ManufacturingSite tbl_Master_ManufacturingSite = new Tbl_Master_ManufacturingSite();
                    tbl_Master_ManufacturingSite.ManufacturingSiteID = Convert.ToInt32(manufacturingSiteMaster.ManufacturingSiteID);
                    tbl_Master_ManufacturingSite.ManufacturingSite = manufacturingSiteMaster.ManufacturingSite.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_ManufacturingSite.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_ManufacturingSite.IsActive = manufacturingSiteMaster.IsActive;
                    }
                    //tbl_Master_ManufacturingSite.IsActive = manufacturingSiteMaster.IsActive;
                    tbl_Master_ManufacturingSite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ManufacturingSite.CreatedDate = DateTime.Today;

                    int data = _Masters.insertManufacturingSite(tbl_Master_ManufacturingSite);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "ManufacturingSite", manufacturingSiteMaster.ManufacturingSite.Trim(), "https://emprojects.emcure.com/Masters/Master_ManufacturingSite");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateManufacturingSite")]
        public ActionResult UpdateManufacturingSite(ManufacturingSiteMaster manufacturingSiteMaster)
        {
            if (ModelState.IsValid)
            {
                var manufacturingSiteName = manufacturingSiteMaster.ManufacturingSite.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ManufacturingSite where x.ManufacturingSite.ToUpper() == manufacturingSiteName select new { x.ManufacturingSiteID, x.ManufacturingSite }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (manufacturingSiteMaster.ManufacturingSiteID == chkduplicate[0].ManufacturingSiteID)
                    {
                        if (manufacturingSiteName == chkduplicate[0].ManufacturingSite.ToUpper())
                        {
                            Tbl_Master_ManufacturingSite tbl_Master_ManufacturingSite = new Tbl_Master_ManufacturingSite();
                            tbl_Master_ManufacturingSite.ManufacturingSiteID = Convert.ToInt32(manufacturingSiteMaster.ManufacturingSiteID);
                            tbl_Master_ManufacturingSite.ManufacturingSite = manufacturingSiteMaster.ManufacturingSite.Trim();
                            tbl_Master_ManufacturingSite.IsActive = manufacturingSiteMaster.IsActive;
                            tbl_Master_ManufacturingSite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_ManufacturingSite.CreatedDate = DateTime.Today;

                            int data = _Masters.updateManufacturingSite(tbl_Master_ManufacturingSite);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Manufacturing Site " + manufacturingSiteMaster.ManufacturingSite.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Manufacture Site " + manufacturingSiteMaster.ManufacturingSite.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_ManufacturingSite tbl_Master_ManufacturingSite = new Tbl_Master_ManufacturingSite();
                    tbl_Master_ManufacturingSite.ManufacturingSiteID = Convert.ToInt32(manufacturingSiteMaster.ManufacturingSiteID);
                    tbl_Master_ManufacturingSite.ManufacturingSite = manufacturingSiteMaster.ManufacturingSite.Trim();
                    tbl_Master_ManufacturingSite.IsActive = manufacturingSiteMaster.IsActive;
                    tbl_Master_ManufacturingSite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ManufacturingSite.CreatedDate = DateTime.Today;

                    int data = _Masters.updateManufacturingSite(tbl_Master_ManufacturingSite);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteManufacturingSite")]
        public ActionResult DeleteManufacturingSite(ManufacturingSiteMaster manufacturingSiteMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_ManufacturingSite tbl_Master_ManufacturingSite = new Tbl_Master_ManufacturingSite();
            tbl_Master_ManufacturingSite.ManufacturingSiteID = Convert.ToInt32(manufacturingSiteMaster.ManufacturingSiteID);
            tbl_Master_ManufacturingSite.ManufacturingSite = manufacturingSiteMaster.ManufacturingSite;
            tbl_Master_ManufacturingSite.IsActive = manufacturingSiteMaster.IsActive;
            tbl_Master_ManufacturingSite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_ManufacturingSite.CreatedDate = DateTime.Today;

            int data = _Masters.deleteManufacturingSite(tbl_Master_ManufacturingSite);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleManufacturingSiteRecord")]
        public ActionResult GetSingleManufacturingSiteRecord(ManufacturingSiteMaster manufacturingSiteMaster)
        {
            IList<Tbl_Master_ManufacturingSite> List = new List<Tbl_Master_ManufacturingSite>();
            List = _Masters.GetManufacturingSiteSingleRecord(Convert.ToInt32(manufacturingSiteMaster.ManufacturingSiteID));
            return Json(new { data = List });
        }

       // [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("ManufacturingSiteList")]
        public ActionResult ManufacturingSiteList()
        {
            IList<Tbl_Master_ManufacturingSite> List = new List<Tbl_Master_ManufacturingSite>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterManufacturingSite();
            }
            else
            {
                List = _Masters.GetMasterManufacturingSiteForUser();
            }
           
            return Json(new { data = List });
        }


        public IActionResult Master_ArtworkType()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertArtworkType")]
        [Obsolete]
        public ActionResult InsertArtworkType(ArtworkTypeMaster artworkTypeMaster)
        {
            if (ModelState.IsValid)
            {
                var artworkTypeName = artworkTypeMaster.ArtworkType.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ArtworkType where x.ArtworkTypeName.ToUpper() == artworkTypeName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "Artwork Type Name " + artworkTypeMaster.ArtworkType.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_ArtworkType tbl_Master_ArtworkType = new Tbl_Master_ArtworkType();
                    tbl_Master_ArtworkType.ArtworkTypeId = Convert.ToInt32(artworkTypeMaster.ArtworkTypeID);
                    tbl_Master_ArtworkType.ArtworkTypeName = artworkTypeMaster.ArtworkType.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_ArtworkType.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_ArtworkType.IsActive = artworkTypeMaster.IsActive;
                    }
                    //tbl_Master_ArtworkType.IsActive = artworkTypeMaster.IsActive;
                    tbl_Master_ArtworkType.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ArtworkType.CreatedDate = DateTime.Today;

                    int data = _Masters.insertArtworkType(tbl_Master_ArtworkType);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "ArtWorkType", artworkTypeMaster.ArtworkType, "https://emprojects.emcure.com/Masters/Master_ArtworkType");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateArtworkType")]
        public ActionResult UpdateArtworkType(ArtworkTypeMaster artworkTypeMaster)
        {
            if (ModelState.IsValid)
            {
                var artworkTypeName = artworkTypeMaster.ArtworkType.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_ArtworkType where x.ArtworkTypeName.ToUpper() == artworkTypeName select new { x.ArtworkTypeId, x.ArtworkTypeName }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (artworkTypeMaster.ArtworkTypeID == chkduplicate[0].ArtworkTypeId)
                    {
                        if (artworkTypeName == chkduplicate[0].ArtworkTypeName.ToUpper())
                        {
                            Tbl_Master_ArtworkType tbl_Master_ArtworkType = new Tbl_Master_ArtworkType();
                            tbl_Master_ArtworkType.ArtworkTypeId = Convert.ToInt32(artworkTypeMaster.ArtworkTypeID);
                            tbl_Master_ArtworkType.ArtworkTypeName = artworkTypeMaster.ArtworkType.Trim();
                            tbl_Master_ArtworkType.IsActive = artworkTypeMaster.IsActive;
                            tbl_Master_ArtworkType.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_ArtworkType.CreatedDate = DateTime.Today;

                            int data = _Masters.updateArtworkType(tbl_Master_ArtworkType);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "Artwork Type " + artworkTypeMaster.ArtworkType.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "Artwork Type " + artworkTypeMaster.ArtworkType.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_ArtworkType tbl_Master_ArtworkType = new Tbl_Master_ArtworkType();
                    tbl_Master_ArtworkType.ArtworkTypeId = Convert.ToInt32(artworkTypeMaster.ArtworkTypeID);
                    tbl_Master_ArtworkType.ArtworkTypeName = artworkTypeMaster.ArtworkType.Trim();
                    tbl_Master_ArtworkType.IsActive = artworkTypeMaster.IsActive;
                    tbl_Master_ArtworkType.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_ArtworkType.CreatedDate = DateTime.Today;

                    int data = _Masters.updateArtworkType(tbl_Master_ArtworkType);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteArtworkType")]
        public ActionResult DeleteArtworkType(ArtworkTypeMaster artworkTypeMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_ArtworkType tbl_Master_ArtworkType = new Tbl_Master_ArtworkType();
            tbl_Master_ArtworkType.ArtworkTypeId = Convert.ToInt32(artworkTypeMaster.ArtworkTypeID);
            tbl_Master_ArtworkType.ArtworkTypeName = artworkTypeMaster.ArtworkType;
            tbl_Master_ArtworkType.IsActive = artworkTypeMaster.IsActive;
            tbl_Master_ArtworkType.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_ArtworkType.CreatedDate = DateTime.Today;

            int data = _Masters.deleteArtworkType(tbl_Master_ArtworkType);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleArtworkTypeRecord")]
        public ActionResult GetSingleArtworkTypeRecord(ArtworkTypeMaster artworkTypeMaster)
        {
            IList<Tbl_Master_ArtworkType> List = new List<Tbl_Master_ArtworkType>();
            List = _Masters.GetArtworkTypeSingleRecord(Convert.ToInt32(artworkTypeMaster.ArtworkTypeID));
            return Json(new { data = List });
        }

       // [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("ArtworkTypeList")]
        public ActionResult ArtworkTypeList()
        {
            IList<Tbl_Master_ArtworkType> List = new List<Tbl_Master_ArtworkType>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterArtworkType();
            }
            else
            {
                List = _Masters.GetMasterArtworkTypeForUser();
            }
           
            return Json(new { data = List });
        }

        public IActionResult Master_GMPAvailability()
        {
            return View();
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("InsertGMPAvailability")]
        [Obsolete]
        public ActionResult InsertGMPAvailability(GMPAvailabilityMaster gMPAvailabilityMaster)
        {
            if (ModelState.IsValid)
            {
                var gMPAvailabilityName = gMPAvailabilityMaster.GMPAvailability.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_GMPAvailability where x.GMPAvailability.ToUpper() == gMPAvailabilityName select x).ToList();

                if (chkduplicate.Count > 0)
                {
                    var displayMessage = "GMP Availability Name " + gMPAvailabilityMaster.GMPAvailability.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {
                    Tbl_Master_GMPAvailability tbl_Master_GMPAvailability = new Tbl_Master_GMPAvailability();
                    tbl_Master_GMPAvailability.GMPAvailabilityID = Convert.ToInt32(gMPAvailabilityMaster.GMPAvailabilityID);
                    tbl_Master_GMPAvailability.GMPAvailability = gMPAvailabilityMaster.GMPAvailability.Trim();
                    if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                    {
                        tbl_Master_GMPAvailability.IsActive = false;
                    }
                    else
                    {
                        tbl_Master_GMPAvailability.IsActive = gMPAvailabilityMaster.IsActive;
                    }
                    //tbl_Master_GMPAvailability.IsActive = gMPAvailabilityMaster.IsActive;
                    tbl_Master_GMPAvailability.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_GMPAvailability.CreatedDate = DateTime.Today;

                    int data = _Masters.insertGMPAvailability(tbl_Master_GMPAvailability);

                    if (data == 1)
                    {
                        if (HttpContext.Session.GetString("CurrentUserRole") != "Prescriber")
                        {
                            SendEmailForMaster("Prescriber", HttpContext.Session.GetString("CurrentUserName"), "GMPAvailability", gMPAvailabilityMaster.GMPAvailability.Trim(), "https://emprojects.emcure.com/Masters/Master_GMPAvailability");
                        }
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
            }
            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("UpdateGMPAvailability")]
        public ActionResult UpdateGMPAvailability(GMPAvailabilityMaster gMPAvailabilityMaster)
        {
            if (ModelState.IsValid)
            {
                var gMPAvailabilityName = gMPAvailabilityMaster.GMPAvailability.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_Master_GMPAvailability where x.GMPAvailability.ToUpper() == gMPAvailabilityName select new { x.GMPAvailabilityID, x.GMPAvailability }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (gMPAvailabilityMaster.GMPAvailabilityID == chkduplicate[0].GMPAvailabilityID)
                    {
                        if (gMPAvailabilityName == chkduplicate[0].GMPAvailability.ToUpper())
                        {
                            Tbl_Master_GMPAvailability tbl_Master_GMPAvailability = new Tbl_Master_GMPAvailability();
                            tbl_Master_GMPAvailability.GMPAvailabilityID = Convert.ToInt32(gMPAvailabilityMaster.GMPAvailabilityID);
                            tbl_Master_GMPAvailability.GMPAvailability = gMPAvailabilityMaster.GMPAvailability.Trim();
                            tbl_Master_GMPAvailability.IsActive = gMPAvailabilityMaster.IsActive;
                            tbl_Master_GMPAvailability.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_Master_GMPAvailability.CreatedDate = DateTime.Today;

                            int data = _Masters.updateGMPAvailability(tbl_Master_GMPAvailability);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            var displayMessage = "GMP Availability " + gMPAvailabilityMaster.GMPAvailability.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }

                    }
                    else
                    {
                        var displayMessage = "GMP Availability " + gMPAvailabilityMaster.GMPAvailability.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    Tbl_Master_GMPAvailability tbl_Master_GMPAvailability = new Tbl_Master_GMPAvailability();
                    tbl_Master_GMPAvailability.GMPAvailabilityID = Convert.ToInt32(gMPAvailabilityMaster.GMPAvailabilityID);
                    tbl_Master_GMPAvailability.GMPAvailability = gMPAvailabilityMaster.GMPAvailability.Trim();
                    tbl_Master_GMPAvailability.IsActive = gMPAvailabilityMaster.IsActive;
                    tbl_Master_GMPAvailability.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_Master_GMPAvailability.CreatedDate = DateTime.Today;

                    int data = _Masters.updateGMPAvailability(tbl_Master_GMPAvailability);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("DeleteGMPAvailability")]
        public ActionResult DeleteGMPAvailability(GMPAvailabilityMaster gMPAvailabilityMaster)
        {
            //if (ModelState.IsValid)
            //{
            Tbl_Master_GMPAvailability tbl_Master_GMPAvailability = new Tbl_Master_GMPAvailability();
            tbl_Master_GMPAvailability.GMPAvailabilityID = Convert.ToInt32(gMPAvailabilityMaster.GMPAvailabilityID);
            tbl_Master_GMPAvailability.GMPAvailability = gMPAvailabilityMaster.GMPAvailability;
            tbl_Master_GMPAvailability.IsActive = gMPAvailabilityMaster.IsActive;
            tbl_Master_GMPAvailability.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            tbl_Master_GMPAvailability.CreatedDate = DateTime.Today;

            int data = _Masters.deleteGMPAvailability(tbl_Master_GMPAvailability);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            //}

            //return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GetSingleGMPAvailabilityRecord")]
        public ActionResult GetSingleGMPAvailabilityRecord(GMPAvailabilityMaster gMPAvailabilityMaster)
        {
            IList<Tbl_Master_GMPAvailability> List = new List<Tbl_Master_GMPAvailability>();
            List = _Masters.GetGMPAvailabilitySingleRecord(Convert.ToInt32(gMPAvailabilityMaster.GMPAvailabilityID));
            return Json(new { data = List });
        }

        //[Authorize(Roles = "Prescriber,Line Manager")]
        [HttpPost]
        [ActionName("GMPAvailabilityList")]
        public ActionResult GMPAvailabilityList()
        {
            IList<Tbl_Master_GMPAvailability> List = new List<Tbl_Master_GMPAvailability>();
            if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            {
                List = _Masters.GetMasterGMPAvailability();
            }
            else
            {
                List = _Masters.GetMasterGMPAvailabilityForUser();
            }
           
            return Json(new { data = List });
        }

        public IActionResult DRFEnableDisabledList()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDRFList")]
        public ActionResult GetAllDRFList()
        {            
            var DRFList = _Masters.GetAllDRFListForEnabledDisabled();
            return Json(new { data = DRFList });
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("UpdateDRFEnabledDisabled")]
        public ActionResult UpdateDRFEnabledDisabled(int DRFID, string isDRFActive)
        {

            bool tempactive=false;
            if(isDRFActive =="true")
            {
                tempactive = true;
            }
            else
            {
                tempactive = false;
            }

            int data = _Masters.DRFEnabledDisabled(DRFID, tempactive);

            if (data == 1)
            {
                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }          
                     

            
        }
        [Obsolete]
        private void SendEmailForMaster(string RoleName,string UserName,string userMessageName,string MasterDataName,string MasterPageURL)
        {
            //get all user list of role Prescriber
            var emailresult = (from p in _db.PrescriberDetails
                               join q in _db.AspNetUsers on p.AspNetUserId equals q.UserId
                               join r in _db.AspNetUserRoles on q.Id equals r.UserId
                               join s in _db.AspNetRoles on r.RoleId equals s.Id
                               where q.IsEnabled == true && s.Name == "Prescriber"
                               select new { p.AspNetUserId, p.FirstName, p.LastName, q.Email }).ToList();

            string userMessage = userMessageName.Trim() + " Name : " + MasterDataName.Trim();
            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
            string userName = UserName.Trim() + " has Created following " + userMessageName.Trim() + " : ";
            string strEmailMessage = userName + "</br>" + userMessageName + " Name : " + MasterDataName.Trim();

            //send email notification code added by yogesh balapure on date 24/08/2021
            //get smtp details 
            SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();            
            SMTPDetailsVM sMTPDetailsVM = new SMTPDetailsVM();
            EmailDetailsVM emailDetailsVM = new EmailDetailsVM();

            if (sMTPDetailsModel != null)
            {
                sMTPDetailsVM.AliasName = sMTPDetailsModel.AliasName;
                sMTPDetailsVM.HostName = sMTPDetailsModel.HostName;
                sMTPDetailsVM.FromMail = sMTPDetailsModel.FromMail;
                sMTPDetailsVM.FromPassword = sMTPDetailsModel.FromPassword;
                sMTPDetailsVM.IsEnableSSL = sMTPDetailsModel.IsEnableSSL;
                sMTPDetailsVM.PortNumber = sMTPDetailsModel.PortNumber;
                sMTPDetailsVM.IsMailStatus = sMTPDetailsModel.IsMailStatus;
                sMTPDetailsVM.IsDefaultCredentials = sMTPDetailsModel.IsDefaultCredentials;
                sMTPDetailsVM.IsWithoutPassword = sMTPDetailsModel.IsWithoutPassword;
            }

            if (emailresult != null)
            {
                //get details
                emailDetailsVM.ToMail = "rahula.patil@emcure.co.in";
                List<string> testCC = new List<string>();
                testCC.Add("rahula.patil@emcure.co.in");
                List<string> testBCC = new List<string>();
                if (emailresult.Count > 0 || emailresult != null)
                {
                    foreach (var ccdata in emailresult)
                    {
                        testBCC.Add(ccdata.Email.Trim());
                    }
                }

                emailDetailsVM.CCMail = testCC;
                emailDetailsVM.BCCMail = testBCC;
                emailDetailsVM.Subject = "Master Data Created";
                clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                string tempurl = MasterPageURL;
                emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                emailDetailsVM.DispalyName = "";
            }

            if (sMTPDetailsModel != null && emailresult != null)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (Convert.ToBoolean(_config.GetSection("MailSend:IsMasterDataInsert").Value) == true)
                {
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }
            }
        }
    }
}