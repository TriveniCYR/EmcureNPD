using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class Masters : IMasters
    {
        private readonly EmcureCERIDBContext _db;

        public Masters()
        {
            _db = new EmcureCERIDBContext();
        }

        public int deleteFormulation(Tbl_Master_Formulation entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterFormulation")
               .WithSqlParam("@FormulationID", entity.Id)
               .WithSqlParam("@Formulation", entity.Formulation)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int insertFormulation(Tbl_Master_Formulation entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterFormulation")
               .WithSqlParam("@FormulationID", entity.Id)
               .WithSqlParam("@Formulation", entity.Formulation)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "INSERT")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<Tbl_Master_Formulation> GetFormulationSingleRecord(int ID)
        {
            IList<Tbl_Master_Formulation> result = new List<Tbl_Master_Formulation>();
            try
            {
                _db.LoadStoredProc("USP_MasterFormulation")
               .WithSqlParam("@FormulationID", ID)
               .WithSqlParam("@Formulation", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Formulation>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Formulation> GetMasterFormulation()
        {
            IList<Tbl_Master_Formulation> result = new List<Tbl_Master_Formulation>();
            try
            {
                _db.LoadStoredProc("USP_MasterFormulation")
               .WithSqlParam("@FormulationID", 0)
               .WithSqlParam("@Formulation", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Formulation>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Formulation> GetMasterFormulationForuser()
        {
            IList<Tbl_Master_Formulation> result = new List<Tbl_Master_Formulation>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
              
                .WithSqlParam("@Action", "FormulationList")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Formulation>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int updateFormulation(Tbl_Master_Formulation entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterFormulation")
               .WithSqlParam("@FormulationID", entity.Id)
               .WithSqlParam("@Formulation", entity.Formulation)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteProductManufacture(Tbl_Master_ProductManufacture entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterProductManufacture")
               .WithSqlParam("@ProductManufactureID", entity.Id)
               .WithSqlParam("@ProductManufacture", entity.ProductManufacture)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_ProductManufacture> GetMasterProductManufacture()
        {
            IList<Tbl_Master_ProductManufacture> result = new List<Tbl_Master_ProductManufacture>();
            try
            {
                _db.LoadStoredProc("USP_MasterProductManufacture")
               .WithSqlParam("@ProductManufactureID", 0)
               .WithSqlParam("@ProductManufacture", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ProductManufacture>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }



        public IList<Tbl_Master_ProductManufacture> GetMasterProductManufactureForUser()
        {
            IList<Tbl_Master_ProductManufacture> result = new List<Tbl_Master_ProductManufacture>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
             
                .WithSqlParam("@Action", "ProductManufacture")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ProductManufacture>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ProductManufacture> GetProductManufactureSingleRecord(int ID)
        {
            IList<Tbl_Master_ProductManufacture> result = new List<Tbl_Master_ProductManufacture>();
            try
            {
                _db.LoadStoredProc("USP_MasterProductManufacture")
               .WithSqlParam("@ProductManufactureID", ID)
               .WithSqlParam("@ProductManufacture", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ProductManufacture>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertProductManufacture(Tbl_Master_ProductManufacture entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterProductManufacture")
               .WithSqlParam("@ProductManufactureID", entity.Id)
               .WithSqlParam("@ProductManufacture", entity.ProductManufacture)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateProductManufacture(Tbl_Master_ProductManufacture entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterProductManufacture")
               .WithSqlParam("@ProductManufactureID", entity.Id)
               .WithSqlParam("@ProductManufacture", entity.ProductManufacture)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deletePackSize(Tbl_Master_PackSize entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackSize")
               .WithSqlParam("@PackSizeID", entity.Id)
               .WithSqlParam("@PackSize", entity.PackSize)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_PackSize> GetMasterPackSize()
        {
            IList<Tbl_Master_PackSize> result = new List<Tbl_Master_PackSize>();
            try
            {
                _db.LoadStoredProc("USP_MasterPackSize")
               .WithSqlParam("@PackSizeID", 0)
               .WithSqlParam("@PackSize", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackSize>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }



        public IList<Tbl_Master_PackSize> GetMasterarPackSizeListForUser()
        {
            IList<Tbl_Master_PackSize> result = new List<Tbl_Master_PackSize>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
                     .WithSqlParam("@Action", "Paksizelist")

                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackSize>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }



        

        public IList<Tbl_Master_PackSize> GetPackSizeSingleRecord(int ID)
        {
            IList<Tbl_Master_PackSize> result = new List<Tbl_Master_PackSize>();
            try
            {
                _db.LoadStoredProc("USP_MasterPackSize")
               .WithSqlParam("@PackSizeID", ID)
               .WithSqlParam("@PackSize", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackSize>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertPackSize(Tbl_Master_PackSize entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackSize")
               .WithSqlParam("@PackSizeID", entity.Id)
               .WithSqlParam("@PackSize", entity.PackSize)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updatePackSize(Tbl_Master_PackSize entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackSize")
               .WithSqlParam("@PackSizeID", entity.Id)
               .WithSqlParam("@PackSize", entity.PackSize)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public int deletePackStyle(Tbl_Master_PackStyle entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackStyle")
               .WithSqlParam("@PackStyleID", entity.Id)
               .WithSqlParam("@PackStyle", entity.PackStyle)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_PackStyle> GetMasterPackStyle()
        {
            IList<Tbl_Master_PackStyle> result = new List<Tbl_Master_PackStyle>();
            try
            {
                _db.LoadStoredProc("USP_MasterPackStyle")
               .WithSqlParam("@PackStyleID", 0)
               .WithSqlParam("@PackStyle", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackStyle>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public IList<Tbl_Master_PackStyle> GetMasterPackStyleForUser()
        {
            IList<Tbl_Master_PackStyle> result = new List<Tbl_Master_PackStyle>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
                     .WithSqlParam("@Action", "PackStyle")

                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackStyle>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public IList<Tbl_Master_PackStyle> GetPackStyleSingleRecord(int ID)
        {
            IList<Tbl_Master_PackStyle> result = new List<Tbl_Master_PackStyle>();
            try
            {
                _db.LoadStoredProc("USP_MasterPackStyle")
               .WithSqlParam("@PackStyleID", ID)
               .WithSqlParam("@PackStyle", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_PackStyle>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertPackStyle(Tbl_Master_PackStyle entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackStyle")
               .WithSqlParam("@PackStyleID", entity.Id)
               .WithSqlParam("@PackStyle", entity.PackStyle)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updatePackStyle(Tbl_Master_PackStyle entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterPackStyle")
               .WithSqlParam("@PackStyleID", entity.Id)
               .WithSqlParam("@PackStyle", entity.PackStyle)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int deleteStrength(Tbl_Master_Strength entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterStrength")
               .WithSqlParam("@StrengthID", entity.Id)
               .WithSqlParam("@Strength", entity.Strength)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_Strength> GetMasterStrength()
        {
            IList<Tbl_Master_Strength> result = new List<Tbl_Master_Strength>();
            try
            {
                _db.LoadStoredProc("USP_MasterStrength")
               .WithSqlParam("@StrengthID", 0)
               .WithSqlParam("@Strength", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Strength>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Strength> GetMasterStrengthForUser()
        {
            IList<Tbl_Master_Strength> result = new List<Tbl_Master_Strength>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
               
                .WithSqlParam("@Action", "Strenght")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Strength>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        

        public IList<Tbl_Master_Strength> GetStrengthSingleRecord(int ID)
        {
            IList<Tbl_Master_Strength> result = new List<Tbl_Master_Strength>();
            try
            {
                _db.LoadStoredProc("USP_MasterStrength")
               .WithSqlParam("@StrengthID", ID)
               .WithSqlParam("@Strength", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Strength>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertStrength(Tbl_Master_Strength entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterStrength")
               .WithSqlParam("@StrengthID", entity.Id)
               .WithSqlParam("@Strength", entity.Strength)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateStrength(Tbl_Master_Strength entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterStrength")
               .WithSqlParam("@StrengthID", entity.Id)
               .WithSqlParam("@Strength", entity.Strength)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int deleteModeofshipment(Tbl_Master_Modeofshipment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofshipment")
               .WithSqlParam("@ModeofshipmentID", entity.Id)
               .WithSqlParam("@Modeofshipment", entity.Modeofshipment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_Modeofshipment> GetMasterModeofshipment()
        {
            IList<Tbl_Master_Modeofshipment> result = new List<Tbl_Master_Modeofshipment>();
            try
            {
                _db.LoadStoredProc("USP_MasterModeofshipment")
               .WithSqlParam("@ModeofshipmentID", 0)
               .WithSqlParam("@Modeofshipment", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Modeofshipment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Modeofshipment> GetMasterModeofshipmentForUser()
        {
            IList<Tbl_Master_Modeofshipment> result = new List<Tbl_Master_Modeofshipment>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
             
                .WithSqlParam("@Action", "Modeofshipment")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Modeofshipment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public IList<Tbl_Master_Modeofshipment> GetModeofshipmentSingleRecord(int ID)
        {
            IList<Tbl_Master_Modeofshipment> result = new List<Tbl_Master_Modeofshipment>();
            try
            {
                _db.LoadStoredProc("USP_MasterModeofshipment")
               .WithSqlParam("@ModeofshipmentID", ID)
               .WithSqlParam("@Modeofshipment", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Modeofshipment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertModeofshipment(Tbl_Master_Modeofshipment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofshipment")
               .WithSqlParam("@ModeofshipmentID", entity.Id)
               .WithSqlParam("@Modeofshipment", entity.Modeofshipment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateModeofshipment(Tbl_Master_Modeofshipment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofshipment")
               .WithSqlParam("@ModeofshipmentID", entity.Id)
               .WithSqlParam("@Modeofshipment", entity.Modeofshipment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int deleteModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofFeesPayment")
               .WithSqlParam("@ModeofFeesPaymentID", entity.Id)
               .WithSqlParam("@ModeofFeesPayment", entity.ModeofFeesPayment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_ModeofFeesPayment> GetMasterModeofFeesPayment()
        {
            IList<Tbl_Master_ModeofFeesPayment> result = new List<Tbl_Master_ModeofFeesPayment>();
            try
            {
                _db.LoadStoredProc("USP_MasterModeofFeesPayment")
               .WithSqlParam("@ModeofFeesPaymentID", 0)
               .WithSqlParam("@ModeofFeesPayment", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ModeofFeesPayment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ModeofFeesPayment> GetMasterModeofFeesPaymentForUser()
        {
            IList<Tbl_Master_ModeofFeesPayment> result = new List<Tbl_Master_ModeofFeesPayment>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
            
                .WithSqlParam("@Action", "ModeofFeesPayment")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ModeofFeesPayment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ModeofFeesPayment> GetModeofFeesPaymentSingleRecord(int ID)
        {
            IList<Tbl_Master_ModeofFeesPayment> result = new List<Tbl_Master_ModeofFeesPayment>();
            try
            {
                _db.LoadStoredProc("USP_MasterModeofFeesPayment")
               .WithSqlParam("@ModeofFeesPaymentID", ID)
               .WithSqlParam("@ModeofFeesPayment", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ModeofFeesPayment>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofFeesPayment")
               .WithSqlParam("@ModeofFeesPaymentID", entity.Id)
               .WithSqlParam("@ModeofFeesPayment", entity.ModeofFeesPayment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateModeofFeesPayment(Tbl_Master_ModeofFeesPayment entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterModeofFeesPayment")
               .WithSqlParam("@ModeofFeesPaymentID", entity.Id)
               .WithSqlParam("@ModeofFeesPayment", entity.ModeofFeesPayment)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteIncoterms(Tbl_Master_Incoterms entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterIncoterms")
               .WithSqlParam("@IncotermsID", entity.Id)
               .WithSqlParam("@Incoterms", entity.Incoterms)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_Incoterms> GetMasterIncoterms()
        {
            IList<Tbl_Master_Incoterms> result = new List<Tbl_Master_Incoterms>();
            try
            {
                _db.LoadStoredProc("USP_MasterIncoterms")
               .WithSqlParam("@IncotermsID", 0)
               .WithSqlParam("@Incoterms", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Incoterms>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Incoterms> GetMasterIncotermsForUser()
        {
            IList<Tbl_Master_Incoterms> result = new List<Tbl_Master_Incoterms>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
              
                .WithSqlParam("@Action", "Incoterms")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Incoterms>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_Incoterms> GetIncotermsSingleRecord(int ID)
        {
            IList<Tbl_Master_Incoterms> result = new List<Tbl_Master_Incoterms>();
            try
            {
                _db.LoadStoredProc("USP_MasterIncoterms")
               .WithSqlParam("@IncotermsID", ID)
               .WithSqlParam("@Incoterms", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_Incoterms>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertIncoterms(Tbl_Master_Incoterms entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterIncoterms")
               .WithSqlParam("@IncotermsID", entity.Id)
               .WithSqlParam("@Incoterms", entity.Incoterms)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateIncoterms(Tbl_Master_Incoterms entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterIncoterms")
               .WithSqlParam("@IncotermsID", entity.Id)
               .WithSqlParam("@Incoterms", entity.Incoterms)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int deleteDossierTemplate(Tbl_Master_DossierTemplate entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterDossierTemplate")
               .WithSqlParam("@DossierTemplateID", entity.Id)
               .WithSqlParam("@DossierTemplate", entity.DossierTemplate)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_DossierTemplate> GetMasterDossierTemplate()
        {
            IList<Tbl_Master_DossierTemplate> result = new List<Tbl_Master_DossierTemplate>();
            try
            {
                _db.LoadStoredProc("USP_MasterDossierTemplate")
               .WithSqlParam("@DossierTemplateID", 0)
               .WithSqlParam("@DossierTemplate", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_DossierTemplate>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_DossierTemplate> GetMasterDossierTemplateFouUser()
        {
            IList<Tbl_Master_DossierTemplate> result = new List<Tbl_Master_DossierTemplate>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
              
                .WithSqlParam("@Action", "DossierTemplate")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_DossierTemplate>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_DossierTemplate> GetDossierTemplateSingleRecord(int ID)
        {
            IList<Tbl_Master_DossierTemplate> result = new List<Tbl_Master_DossierTemplate>();
            try
            {
                _db.LoadStoredProc("USP_MasterDossierTemplate")
               .WithSqlParam("@DossierTemplateID", ID)
               .WithSqlParam("@DossierTemplate", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_DossierTemplate>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDossierTemplate(Tbl_Master_DossierTemplate entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterDossierTemplate")
               .WithSqlParam("@DossierTemplateID", entity.Id)
               .WithSqlParam("@DossierTemplate", entity.DossierTemplate)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateDossierTemplate(Tbl_Master_DossierTemplate entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterDossierTemplate")
               .WithSqlParam("@DossierTemplateID", entity.Id)
               .WithSqlParam("@DossierTemplate", entity.DossierTemplate)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteAPISite(Tbl_Master_APISite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterAPISite")
               .WithSqlParam("@APISiteID", entity.APIID)
               .WithSqlParam("@APISite", entity.APISite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_APISite> GetMasterAPISite()
        {
            IList<Tbl_Master_APISite> result = new List<Tbl_Master_APISite>();
            try
            {
                _db.LoadStoredProc("USP_MasterAPISite")
               .WithSqlParam("@APISiteID", 0)
               .WithSqlParam("@APISite", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_APISite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public IList<Tbl_Master_APISite> GetMasterAPISiteForUser()
        {
            IList<Tbl_Master_APISite> result = new List<Tbl_Master_APISite>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
              
                .WithSqlParam("@Action", "APISite")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_APISite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_APISite> GetAPISiteSingleRecord(int ID)
        {
            IList<Tbl_Master_APISite> result = new List<Tbl_Master_APISite>();
            try
            {
                _db.LoadStoredProc("USP_MasterAPISite")
               .WithSqlParam("@APISiteID", ID)
               .WithSqlParam("@APISite", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_APISite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertAPISite(Tbl_Master_APISite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterAPISite")
               .WithSqlParam("@APISiteID", entity.APIID)
               .WithSqlParam("@APISite", entity.APISite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateAPISite(Tbl_Master_APISite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterAPISite")
               .WithSqlParam("@APISiteID", entity.APIID)
               .WithSqlParam("@APISite", entity.APISite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteManufacturingSite(Tbl_Master_ManufacturingSite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterManufacturingSite")
               .WithSqlParam("@ManufacturingSiteID", entity.ManufacturingSiteID)
               .WithSqlParam("@ManufacturingSite", entity.ManufacturingSite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_ManufacturingSite> GetMasterManufacturingSite()
        {
            IList<Tbl_Master_ManufacturingSite> result = new List<Tbl_Master_ManufacturingSite>();
            try
            {
                _db.LoadStoredProc("USP_MasterManufacturingSite")
               .WithSqlParam("@ManufacturingSiteID", 0)
               .WithSqlParam("@ManufacturingSite", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ManufacturingSite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ManufacturingSite> GetMasterManufacturingSiteForUser()
        {
            IList<Tbl_Master_ManufacturingSite> result = new List<Tbl_Master_ManufacturingSite>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
               
                .WithSqlParam("@Action", "ManufacturingSite")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ManufacturingSite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ManufacturingSite> GetManufacturingSiteSingleRecord(int ID)
        {
            IList<Tbl_Master_ManufacturingSite> result = new List<Tbl_Master_ManufacturingSite>();
            try
            {
                _db.LoadStoredProc("USP_MasterManufacturingSite")
               .WithSqlParam("@ManufacturingSiteID", ID)
               .WithSqlParam("@ManufacturingSite", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ManufacturingSite>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertManufacturingSite(Tbl_Master_ManufacturingSite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterManufacturingSite")
               .WithSqlParam("@ManufacturingSiteID", entity.ManufacturingSiteID)
               .WithSqlParam("@ManufacturingSite", entity.ManufacturingSite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateManufacturingSite(Tbl_Master_ManufacturingSite entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterManufacturingSite")
               .WithSqlParam("@ManufacturingSiteID", entity.ManufacturingSiteID)
               .WithSqlParam("@ManufacturingSite", entity.ManufacturingSite)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteArtworkType(Tbl_Master_ArtworkType entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterArtworkType")
               .WithSqlParam("@ArtworkTypeID", entity.ArtworkTypeId)
               .WithSqlParam("@ArtworkType", entity.ArtworkTypeName)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_ArtworkType> GetMasterArtworkType()
        {
            IList<Tbl_Master_ArtworkType> result = new List<Tbl_Master_ArtworkType>();
            try
            {
                _db.LoadStoredProc("USP_MasterArtworkType")
               .WithSqlParam("@ArtworkTypeID", 0)
               .WithSqlParam("@ArtworkType", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ArtworkType>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_ArtworkType> GetMasterArtworkTypeForUser()
        {
            IList<Tbl_Master_ArtworkType> result = new List<Tbl_Master_ArtworkType>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
              
                .WithSqlParam("@Action", "ArtworkType")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ArtworkType>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public IList<Tbl_Master_ArtworkType> GetArtworkTypeSingleRecord(int ID)
        {
            IList<Tbl_Master_ArtworkType> result = new List<Tbl_Master_ArtworkType>();
            try
            {
                _db.LoadStoredProc("USP_MasterArtworkType")
               .WithSqlParam("@ArtworkTypeID", ID)
               .WithSqlParam("@ArtworkType", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_ArtworkType>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertArtworkType(Tbl_Master_ArtworkType entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterArtworkType")
                .WithSqlParam("@ArtworkTypeID", entity.ArtworkTypeId)
               .WithSqlParam("@ArtworkType", entity.ArtworkTypeName)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateArtworkType(Tbl_Master_ArtworkType entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterArtworkType")
               .WithSqlParam("@ArtworkTypeID", entity.ArtworkTypeId)
               .WithSqlParam("@ArtworkType", entity.ArtworkTypeName)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteGMPAvailability(Tbl_Master_GMPAvailability entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterGMPAvailability")
               .WithSqlParam("@GMPAvailabilityID", entity.GMPAvailabilityID)
               .WithSqlParam("@GMPAvailability", entity.GMPAvailability)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "DELETE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public IList<Tbl_Master_GMPAvailability> GetMasterGMPAvailability()
        {
            IList<Tbl_Master_GMPAvailability> result = new List<Tbl_Master_GMPAvailability>();
            try
            {
                _db.LoadStoredProc("USP_MasterGMPAvailability")
               .WithSqlParam("@GMPAvailabilityID", 0)
               .WithSqlParam("@GMPAvailability", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "LIST")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_GMPAvailability>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_GMPAvailability> GetMasterGMPAvailabilityForUser()
        {
            IList<Tbl_Master_GMPAvailability> result = new List<Tbl_Master_GMPAvailability>();
            try
            {
                _db.LoadStoredProc("USP_AllMasterListIaActiveForUser")
             
                .WithSqlParam("@Action", "GMPAvailability")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_GMPAvailability>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_Master_GMPAvailability> GetGMPAvailabilitySingleRecord(int ID)
        {
            IList<Tbl_Master_GMPAvailability> result = new List<Tbl_Master_GMPAvailability>();
            try
            {
                _db.LoadStoredProc("USP_MasterGMPAvailability")
               .WithSqlParam("@GMPAvailabilityID", ID)
               .WithSqlParam("@GMPAvailability", "")
               .WithSqlParam("@IsActive", false)
               .WithSqlParam("@CreatedBy", 0)
                .WithSqlParam("@CreatedDate", "")
                .WithSqlParam("@Action", "SINGLE RECORD")
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<Tbl_Master_GMPAvailability>();
                 });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertGMPAvailability(Tbl_Master_GMPAvailability entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterGMPAvailability")
              .WithSqlParam("@GMPAvailabilityID", entity.GMPAvailabilityID)
               .WithSqlParam("@GMPAvailability", entity.GMPAvailability)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
               .WithSqlParam("@Action", "INSERT")
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateGMPAvailability(Tbl_Master_GMPAvailability entity)
        {
            try
            {
                _db.LoadStoredProc("USP_MasterGMPAvailability")
               .WithSqlParam("@GMPAvailabilityID", entity.GMPAvailabilityID)
               .WithSqlParam("@GMPAvailability", entity.GMPAvailability)
               .WithSqlParam("@IsActive", entity.IsActive)
               .WithSqlParam("@CreatedBy", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@Action", "UPDATE")
                 .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<DRFListModel> GetAllDRFListForEnabledDisabled()
        {
            IList<DRFListModel> result = new List<DRFListModel>();
            try
            {
                _db.LoadStoredProc("USP_GetAllDRFList_For_EnabledDisabled")               
                 .ExecuteStoredProc((handler) =>
                 {
                     result = handler.ReadToList<DRFListModel>();
                 });
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int DRFEnabledDisabled(int DRFID,bool isActive)
        {
            try
            {
                _db.LoadStoredProc("USP_Update_DRF_EnabledDisabled")
                    .WithSqlParam("@DRFID", DRFID)
                    .WithSqlParam("@IsActive", isActive)               
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
