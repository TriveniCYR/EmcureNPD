using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class ProductMasterDataService : IProductMasterDataService
    {
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _config;

        public ProductMasterDataService(EmcureCERIDBContext db, IConfiguration config)
        {
            _db = db;
            _config = config;           
        }

        public Tbl_Master_ProductData CheckProductMasterDetails(Tbl_Master_ProductData tbl_Master_ProductData)
        {
            IList<Tbl_Master_ProductData> result = new List<Tbl_Master_ProductData>();
            try
            {
                _db.LoadStoredProc("USP_Tbl_Master_ProductData_CheckExistsByID")
                    .WithSqlParam("@UPID", tbl_Master_ProductData.UPID)
                    .WithSqlParam("@GenericName", tbl_Master_ProductData.GenericName)
                    .WithSqlParam("@BrandName", tbl_Master_ProductData.BrandName)
                    .WithSqlParam("@FormulationID", tbl_Master_ProductData.FormulationID)
                    .WithSqlParam("@FormName", tbl_Master_ProductData.FormName)
                    .WithSqlParam("@StrengthID", tbl_Master_ProductData.StrengthID)
                    .WithSqlParam("@Strength", tbl_Master_ProductData.Strength)
                    .WithSqlParam("@PackStyleID", tbl_Master_ProductData.PackStyleID)
                    .WithSqlParam("@PackStyle", tbl_Master_ProductData.PackStyle)
                    .WithSqlParam("@PackSizeID", tbl_Master_ProductData.PackSizeID)
                    .WithSqlParam("@PackSize", tbl_Master_ProductData.PackSize)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ProductData>();
                 });
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int DeleteProductMaster(Int64 ProductID)
        {
            try
            {
                _db.LoadStoredProc("USP_Tbl_Master_ProductData_Delete")
                 .WithSqlParam("@UPID", ProductID)
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<Tbl_Master_ProductData> GetAllProductMaster(Int64 ProductID, string strAction)
        {
            IList<Tbl_Master_ProductData> result = new List<Tbl_Master_ProductData>();
            try
            {
                _db.LoadStoredProc("USP_Tbl_Master_ProductData_GetAll")
                    .WithSqlParam("@UPID", ProductID)
                    .WithSqlParam("@Action", strAction)                   
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ProductData>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public Tbl_Master_ProductData GetProductMasterDetails(Tbl_Master_ProductData tbl_Master_ProductData)
        {
            IList<Tbl_Master_ProductData> result = new List<Tbl_Master_ProductData>();
            try
            {
                _db.LoadStoredProc("USP_Tbl_Master_ProductData_CheckExistsByID")
                .WithSqlParam("@UPID", tbl_Master_ProductData.UPID)
                .WithSqlParam("@GenericName", tbl_Master_ProductData.GenericName)
                .WithSqlParam("@BrandName", tbl_Master_ProductData.BrandName)
                .WithSqlParam("@FormulationID", tbl_Master_ProductData.FormulationID)
                .WithSqlParam("@FormName", tbl_Master_ProductData.FormName)
                .WithSqlParam("@StrengthID", tbl_Master_ProductData.StrengthID)
                .WithSqlParam("@Strength", tbl_Master_ProductData.Strength)
                .WithSqlParam("@PackStyleID", tbl_Master_ProductData.PackStyleID)
                .WithSqlParam("@PackStyle", tbl_Master_ProductData.PackStyle)
                .WithSqlParam("@PackSizeID", tbl_Master_ProductData.PackSizeID)
                .WithSqlParam("@PackSize", tbl_Master_ProductData.PackSize)                    
                .ExecuteStoredProc((handler) =>
                {
                     result = handler.ReadToList<Tbl_Master_ProductData>();
                });
                if(result.Count>0)
                {
                    return result[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertProductMaster(Tbl_Master_ProductData tbl_Master_ProductData)
        {
            try
            {
                var testoutput = _db.LoadStoredProc("USP_Tbl_Master_ProductData_Insert")
                .WithSqlParam("@GenericName", tbl_Master_ProductData.GenericName)
                .WithSqlParam("@BrandName", tbl_Master_ProductData.BrandName)
                .WithSqlParam("@FormulationID", tbl_Master_ProductData.FormulationID)
                .WithSqlParam("@FormName", tbl_Master_ProductData.FormName)
                .WithSqlParam("@StrengthID", tbl_Master_ProductData.StrengthID)
                .WithSqlParam("@Strength", tbl_Master_ProductData.Strength)
                .WithSqlParam("@PackStyleID", tbl_Master_ProductData.PackStyleID)
                .WithSqlParam("@PackStyle", tbl_Master_ProductData.PackStyle)
                .WithSqlParam("@PackSizeID", tbl_Master_ProductData.PackSizeID)
                .WithSqlParam("@PackSize", tbl_Master_ProductData.PackSize)
                .WithSqlParam("@PlantID", tbl_Master_ProductData.PlantID)
                .WithSqlParam("@PlantName", tbl_Master_ProductData.PlantName)
                .WithSqlParam("@CreatedBy", tbl_Master_ProductData.CreatedBy)                                      
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int UpdateProductMaster(Tbl_Master_ProductData tbl_Master_ProductData)
        {
            try
            {
                var testoutput = _db.LoadStoredProc("USP_Tbl_Master_ProductData_Update")
                .WithSqlParam("@UPID", tbl_Master_ProductData.UPID)
                .WithSqlParam("@GenericName", tbl_Master_ProductData.GenericName)
                .WithSqlParam("@BrandName", tbl_Master_ProductData.BrandName)
                .WithSqlParam("@FormulationID", tbl_Master_ProductData.FormulationID)
                .WithSqlParam("@FormName", tbl_Master_ProductData.FormName)
                .WithSqlParam("@StrengthID", tbl_Master_ProductData.StrengthID)
                .WithSqlParam("@Strength", tbl_Master_ProductData.Strength)
                .WithSqlParam("@PackStyleID", tbl_Master_ProductData.PackStyleID)
                .WithSqlParam("@PackStyle", tbl_Master_ProductData.PackStyle)
                .WithSqlParam("@PackSizeID", tbl_Master_ProductData.PackSizeID)
                .WithSqlParam("@PackSize", tbl_Master_ProductData.PackSize)
                .WithSqlParam("@PlantID", tbl_Master_ProductData.PlantID)
                .WithSqlParam("@PlantName", tbl_Master_ProductData.PlantName)
                .WithSqlParam("@CreatedBy", tbl_Master_ProductData.CreatedBy)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
