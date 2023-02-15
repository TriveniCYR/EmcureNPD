using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Controllers
{
    public class ProductMasterDataController : Controller
    {
        private readonly IConfiguration _config;
        private readonly EmcureCERIDBContext _db;
        private readonly IProductMasterDataService _productMasterDataService;

        public ProductMasterDataController(IProductMasterDataService productMasterDataService, IConfiguration config)
        {
            this._productMasterDataService = productMasterDataService;
            _db = new EmcureCERIDBContext();
            _config = config;            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AllProductMasterDataList")]
        public ActionResult AllProductMasterDataList()
        {
            IList<Tbl_Master_ProductData> List = new List<Tbl_Master_ProductData>();
            //if (HttpContext.Session.GetString("CurrentUserRole") == "Prescriber")
            //{
            List = _productMasterDataService.GetAllProductMaster(0, "All");
            //}
            //else
            //{
            //    List = _Masters.GetMasterFormulationForuser();
            //}

            return Json(new { data = List });
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("GetSingleMasterProductData")]
        public ActionResult GetSingleMasterProductData(MasterProductDataModel masterProductDataModel)
        {
            IList<Tbl_Master_ProductData> List = new List<Tbl_Master_ProductData>();
            List = _productMasterDataService.GetAllProductMaster(masterProductDataModel.UPID,"1");
            return Json(new { data = List });
        }

        [Authorize(Roles = "Prescriber")]
        [HttpPost]
        [ActionName("InsertMasterProductData")]
        [Obsolete]
        public ActionResult InsertMasterProductData(MasterProductDataModel masterProductDataModel)
        {
            if (ModelState.IsValid)
            {
                Tbl_Master_ProductData tbl_Master_ProductData = new Tbl_Master_ProductData();
                tbl_Master_ProductData.UPID = masterProductDataModel.UPID;
                tbl_Master_ProductData.GenericName = masterProductDataModel.GenericName;
                tbl_Master_ProductData.BrandName = masterProductDataModel.BrandName;
                tbl_Master_ProductData.FormulationID = masterProductDataModel.FormulationID;
                tbl_Master_ProductData.FormName = masterProductDataModel.FormName;
                tbl_Master_ProductData.StrengthID = masterProductDataModel.StrengthID;
                tbl_Master_ProductData.Strength = masterProductDataModel.Strength;
                tbl_Master_ProductData.PackStyleID = masterProductDataModel.PackStyleID;
                tbl_Master_ProductData.PackStyle = masterProductDataModel.PackStyle;
                tbl_Master_ProductData.PackSizeID = masterProductDataModel.PackSizeID;
                tbl_Master_ProductData.PackSize = masterProductDataModel.PackSize;
                tbl_Master_ProductData.PlantID = masterProductDataModel.PlantID;
                tbl_Master_ProductData.PlantName = masterProductDataModel.PlantName;
                tbl_Master_ProductData.IsActive = masterProductDataModel.IsActive;
                tbl_Master_ProductData.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProductData.CreatedDate = DateTime.Today;
                tbl_Master_ProductData.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProductData.ModifiedDate = DateTime.Today;

                var chkduplicate = _productMasterDataService.CheckProductMasterDetails(tbl_Master_ProductData);
                if (chkduplicate != null)
                {
                    var displayMessage = "Product Manufacture " + masterProductDataModel.GenericName.Trim() + " is already exists in database.";
                    return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                }
                else
                {                    
                    int data = _productMasterDataService.InsertProductMaster(tbl_Master_ProductData);

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
        [ActionName("UpdateMasterProductData")]
        public ActionResult UpdateProductManufacture(MasterProductDataModel masterProductDataModel)
        {
            if (ModelState.IsValid)
            {
                Tbl_Master_ProductData tbl_Master_ProductData = new Tbl_Master_ProductData();
                tbl_Master_ProductData.UPID = masterProductDataModel.UPID;
                tbl_Master_ProductData.GenericName = masterProductDataModel.GenericName;
                tbl_Master_ProductData.BrandName = masterProductDataModel.BrandName;
                tbl_Master_ProductData.FormulationID = masterProductDataModel.FormulationID;
                tbl_Master_ProductData.FormName = masterProductDataModel.FormName;
                tbl_Master_ProductData.StrengthID = masterProductDataModel.StrengthID;
                tbl_Master_ProductData.Strength = masterProductDataModel.Strength;
                tbl_Master_ProductData.PackStyleID = masterProductDataModel.PackStyleID;
                tbl_Master_ProductData.PackStyle = masterProductDataModel.PackStyle;
                tbl_Master_ProductData.PackSizeID = masterProductDataModel.PackSizeID;
                tbl_Master_ProductData.PackSize = masterProductDataModel.PackSize;
                tbl_Master_ProductData.PlantID = masterProductDataModel.PlantID;
                tbl_Master_ProductData.PlantName = masterProductDataModel.PlantName;
                tbl_Master_ProductData.IsActive = masterProductDataModel.IsActive;
                tbl_Master_ProductData.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProductData.CreatedDate = DateTime.Today;
                tbl_Master_ProductData.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_Master_ProductData.ModifiedDate = DateTime.Today;

                var chkduplicate = _productMasterDataService.CheckProductMasterDetails(tbl_Master_ProductData);
                 
                if (chkduplicate !=null)
                {
                    if (masterProductDataModel.UPID == chkduplicate.UPID)
                    {
                        if (masterProductDataModel.GenericName.ToUpper() == chkduplicate.GenericName.ToUpper())
                        {                            
                            int data = _productMasterDataService.UpdateProductMaster(tbl_Master_ProductData);

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
                            var displayMessage = "Master Product Data" + masterProductDataModel.GenericName.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                        }
                    }
                    else
                    {
                        var displayMessage = "Master Product Data" + masterProductDataModel.GenericName.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = displayMessage }, new JsonSerializerSettings());
                    }
                }
                else
                {                    
                    int data = _productMasterDataService.UpdateProductMaster(tbl_Master_ProductData);

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
    }
}
