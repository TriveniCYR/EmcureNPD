using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFRA : IDRFRA
    {
        private readonly EmcureCERIDBContext _db;

        public DRFRA()
        {
            _db = new EmcureCERIDBContext();
        }

        public IList<Tbl_DRF_Requisite_RAInfo> GetRAFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_Requisite_RAInfo> result = new List<Tbl_DRF_Requisite_RAInfo>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "RA")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Requisite_RAInfo>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFRA(Tbl_DRF_Requisite_RAInfo entity, int DossierTemplateID)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFRADetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ACC", entity.ACC)
                .WithSqlParam("@ZoneII", entity.ZoneII)
                .WithSqlParam("@Ivbdata", entity.Ivbdata)
                .WithSqlParam("@ProtocolAvailability", entity.ProtocolAvailability)
                .WithSqlParam("@COPPAvailability", entity.COPPAvailability)
                .WithSqlParam("@GMPAvailabilityId", entity.GMPAvailabilityId)
                .WithSqlParam("@GMPAvailability", entity.GMPAvailability)
                .WithSqlParam("@MfgLicense", entity.MfgLicense)
                .WithSqlParam("@PlantInspection", entity.PlantInspection)
                .WithSqlParam("@ValidationBatches", entity.ValidationBatches)
                .WithSqlParam("@COAAvailability", entity.COAAvailability)
                .WithSqlParam("@BEAvailability", entity.BEAvailability)
                .WithSqlParam("@APIDMFstatus", entity.APIDMFstatus)
                .WithSqlParam("@PlantApproval", entity.PlantApproval)
                .WithSqlParam("@PlantApprovalIfYes", entity.PlantApprovalIfYes)
                .WithSqlParam("@RegistrationValidity", entity.RegistrationValidity)
                .WithSqlParam("@Timefordossierpreparation", entity.Timefordossierpreparation)
                .WithSqlParam("@AMV", entity.AMV)
                .WithSqlParam("@PDR", entity.PDR)
                .WithSqlParam("@SamplesAvailability", entity.SamplesAvailability)
                .WithSqlParam("@ImportPermit", entity.ImportPermit)
                .WithSqlParam("@BrandNameApproval", entity.BrandNameApproval)
                .WithSqlParam("@AvailabilityofCDA", entity.AvailabilityofCDA)
                .WithSqlParam("@CurrencyID", entity.CurrencyID)
                .WithSqlParam("@ProductRegistrationFee", entity.ProductRegistrationFee)
                .WithSqlParam("@ComparativeDissolutionProfileData", entity.ComparativeDissolutionProfileData)
                .WithSqlParam("@Remarks", entity.Remarks)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@DossierTemplateID", DossierTemplateID)
                 .WithSqlParam("@ConsultantCost", entity.ConsultantCost)
                  .WithSqlParam("@LegalizationCost", entity.LegalizationCost)
                   .WithSqlParam("@TranslationCost", entity.TranslationCost)
                    .WithSqlParam("@OtherCost", entity.OtherCost)

                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int updateDRFRA(Tbl_DRF_Requisite_RAInfo entity, int DossierTemplateID)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFRADetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@ACC", entity.ACC)
                .WithSqlParam("@ZoneII", entity.ZoneII)
                .WithSqlParam("@Ivbdata", entity.Ivbdata)
                .WithSqlParam("@ProtocolAvailability", entity.ProtocolAvailability)
                .WithSqlParam("@COPPAvailability", entity.COPPAvailability)
                .WithSqlParam("@GMPAvailabilityId", entity.GMPAvailabilityId)
                .WithSqlParam("@GMPAvailability", entity.GMPAvailability)
                .WithSqlParam("@MfgLicense", entity.MfgLicense)
                .WithSqlParam("@PlantInspection", entity.PlantInspection)
                .WithSqlParam("@ValidationBatches", entity.ValidationBatches)
                .WithSqlParam("@COAAvailability", entity.COAAvailability)
                .WithSqlParam("@BEAvailability", entity.BEAvailability)
                .WithSqlParam("@APIDMFstatus", entity.APIDMFstatus)
                .WithSqlParam("@PlantApproval", entity.PlantApproval)
                .WithSqlParam("@PlantApprovalIfYes", entity.PlantApprovalIfYes)
                .WithSqlParam("@RegistrationValidity", entity.RegistrationValidity)
                .WithSqlParam("@Timefordossierpreparation", entity.Timefordossierpreparation)
                .WithSqlParam("@AMV", entity.AMV)
                .WithSqlParam("@PDR", entity.PDR)
                .WithSqlParam("@SamplesAvailability", entity.SamplesAvailability)
                .WithSqlParam("@ImportPermit", entity.ImportPermit)
                .WithSqlParam("@BrandNameApproval", entity.BrandNameApproval)
                .WithSqlParam("@AvailabilityofCDA", entity.AvailabilityofCDA)
                .WithSqlParam("@CurrencyID", entity.CurrencyID)
                .WithSqlParam("@ProductRegistrationFee", entity.ProductRegistrationFee)
                .WithSqlParam("@ComparativeDissolutionProfileData", entity.ComparativeDissolutionProfileData)
                .WithSqlParam("@Remarks", entity.Remarks)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                 .WithSqlParam("@DossierTemplateID", DossierTemplateID)
                  .WithSqlParam("@ConsultantCost", entity.ConsultantCost)
                  .WithSqlParam("@LegalizationCost", entity.LegalizationCost)
                   .WithSqlParam("@TranslationCost", entity.TranslationCost)
                    .WithSqlParam("@OtherCost", entity.OtherCost)
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
