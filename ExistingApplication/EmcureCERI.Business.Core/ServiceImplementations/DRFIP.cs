using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFIP : IDRFIP
    {
        private readonly EmcureCERIDBContext _db;

        public DRFIP()
        {
            _db = new EmcureCERIDBContext();
        }

        public IList<Tbl_DRF_Patent_Details> GetIPHeaderDetailsFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_Patent_Details> result = new List<Tbl_DRF_Patent_Details>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "IPDetail")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Patent_Details>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public IList<Tbl_DRF_IP_Details> GetIPHeaderFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_IP_Details> result = new List<Tbl_DRF_IP_Details>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action","IPHeader")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList <Tbl_DRF_IP_Details>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFIP(DRFIPHeaderAndDetails entity)
        {
            int result = 0;
            try
            {
                _db.LoadStoredProc("USP_InsertDRFIPHeader")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ProjectName", entity.ProjectName)
                .WithSqlParam("@Markets", entity.Markets)
                .WithSqlParam("@NumbersOfApprovedANDA", entity.NumbersOfApprovedANDA)
                .WithSqlParam("@PatentStatus", entity.PatentStatus)
                .WithSqlParam("@LegalStatus", entity.LegalStatus)
                .WithSqlParam("@IPDComments", entity.IPDComments)
                .WithSqlParam("@NumbersOfApprovedGeneric", entity.NumbersOfApprovedGeneric)
                .WithSqlParam("@TypeOfFiling", entity.TypeOfFiling)
                .WithSqlParam("@CostofLitigation", entity.CostofLitigation)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = Convert.ToInt32(handler.ReadToValue<int>());
                 });

                for (int i = 0; i < entity.tbl_DRF_Patent_Details.Count; i++)
                {
                    _db.LoadStoredProc("USP_InsertDRFIPDetails")
                    .WithSqlParam("@IPID", result)
                    .WithSqlParam("@PatentNumbers", entity.tbl_DRF_Patent_Details[i].PatentNumbers)
                    .WithSqlParam("@OriginalExpiryDate", entity.tbl_DRF_Patent_Details[i].OriginalExpiryDate)
                    .WithSqlParam("@Type", entity.tbl_DRF_Patent_Details[i].Type)
                    .WithSqlParam("@ExtensionApplication", entity.tbl_DRF_Patent_Details[i].ExtensionApplication)
                    .WithSqlParam("@ExtnExpiryDate", entity.tbl_DRF_Patent_Details[i].ExtnExpiryDate)
                    .WithSqlParam("@Comment", entity.tbl_DRF_Patent_Details[i].Comment)
                    .WithSqlParam("@Strategy", entity.tbl_DRF_Patent_Details[i].Strategy)
                    .WithSqlParam("@Createdby", entity.CreatedBy)
                    .WithSqlParam("@CreatedDate", entity.CreatedDate)
                    .ExecuteStoredNonQuery();
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int updateDRFIP(DRFIPHeaderAndDetails entity)
        {
            int result = 0;
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFIPHeader")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ProjectName", entity.ProjectName)
                .WithSqlParam("@Markets", entity.Markets)
                .WithSqlParam("@NumbersOfApprovedANDA", entity.NumbersOfApprovedANDA)
                .WithSqlParam("@PatentStatus", entity.PatentStatus)
                .WithSqlParam("@LegalStatus", entity.LegalStatus)
                .WithSqlParam("@IPDComments", entity.IPDComments)
                .WithSqlParam("@NumbersOfApprovedGeneric", entity.NumbersOfApprovedGeneric)
                .WithSqlParam("@TypeOfFiling", entity.TypeOfFiling)
                .WithSqlParam("@CostofLitigation", entity.CostofLitigation)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                 .ExecuteStoredProc((handler) =>
                 {
                     result = Convert.ToInt32(handler.ReadToValue<int>());
                 });

                for (int i = 0; i < entity.tbl_DRF_Patent_Details.Count; i++)
                {
                    _db.LoadStoredProc("USP_InsertDRFIPDetails")
                   .WithSqlParam("@IPID", result)
                     .WithSqlParam("@Id", entity.tbl_DRF_Patent_Details[i].Id)
                   .WithSqlParam("@PatentNumbers", entity.tbl_DRF_Patent_Details[i].PatentNumbers)
                 
                    .WithSqlParam("@OriginalExpiryDate", entity.tbl_DRF_Patent_Details[i].OriginalExpiryDate)
                    .WithSqlParam("@Type", entity.tbl_DRF_Patent_Details[i].Type)
                    .WithSqlParam("@ExtensionApplication", entity.tbl_DRF_Patent_Details[i].ExtensionApplication)
                    .WithSqlParam("@ExtnExpiryDate", entity.tbl_DRF_Patent_Details[i].ExtnExpiryDate)
                    .WithSqlParam("@Comment", entity.tbl_DRF_Patent_Details[i].Comment)
                    .WithSqlParam("@Strategy", entity.tbl_DRF_Patent_Details[i].Strategy)
                    .WithSqlParam("@Createdby", entity.CreatedBy)
                    .WithSqlParam("@CreatedDate", entity.CreatedDate)
                    .ExecuteStoredNonQuery();
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


    }
}
