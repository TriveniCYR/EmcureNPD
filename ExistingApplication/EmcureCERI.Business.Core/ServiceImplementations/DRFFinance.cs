using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFFinance : IDRFFinance
    {
        private readonly EmcureCERIDBContext _db;

        public DRFFinance()
        {
            _db = new EmcureCERIDBContext();
        }

        public int insertDRFFinanceApprovel(Tbl_DRF_FinanceDetails entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFFinanceApprovel")
               .WithSqlParam("@InitializationID", entity.InitializationID)
               .WithSqlParam("@IsOverallBusinessCaseFine", entity.Overallbusinesscase)
               .WithSqlParam("@Exworks", entity.Exworks)
               .WithSqlParam("@GCminimum", entity.GCminimum)
               .WithSqlParam("@ExworksYearTwo", entity.ExworksYearTwo)
               .WithSqlParam("@GCminimumYearTwo", entity.GCminimumYearTwo)
               .WithSqlParam("@ExworksYearThree", entity.ExworksYearThree)
               .WithSqlParam("@GCminimumYearThree", entity.GCminimumYearThree)
               .WithSqlParam("@Expenses", entity.Expenses)
               .WithSqlParam("@FilingCost", entity.FilingCost)
               .WithSqlParam("@Freight", entity.Freight)
               .WithSqlParam("@FreightYearTwo", entity.FreightYearTwo)
               .WithSqlParam("@FreightYearThree", entity.FreightYearThree)
               .WithSqlParam("@TotalContribution", entity.TotalContribution)
               .WithSqlParam("@TotalPercentage", entity.TotalPercentage)
               .WithSqlParam("@NetContribution", entity.NetContribution)
               .WithSqlParam("@NetPercentage", entity.NetPercentage)
               .WithSqlParam("@CreatedBy", entity.Createdby)
               .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .WithSqlParam("@LitigationCost", entity.LitigationCost)
                  .WithSqlParam("@FreightCost", entity.FreightCost)
                   .WithSqlParam("@RegistrationCost", entity.RegistrationCost)
                    .WithSqlParam("@ConsultantCost", entity.ConsultantCost)
                     .WithSqlParam("@LegalizationCost", entity.LegalizationCost)
                      .WithSqlParam("@TranslationCost", entity.TranslationCost)
                       .WithSqlParam("@OtherCost", entity.OtherCost)
                        .WithSqlParam("@BECost", entity.BECost)
                         .WithSqlParam("@BioCost", entity.BioCost)
                          .WithSqlParam("@CTCost", entity.CTCost)
               .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public int updateDRFFinanceApprovel(Tbl_DRF_FinanceDetails entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFFinanceApprovel")
               .WithSqlParam("@InitializationID", entity.InitializationID)
               .WithSqlParam("@IsOverallBusinessCaseFine", entity.Overallbusinesscase)
               .WithSqlParam("@Exworks", entity.Exworks)
               .WithSqlParam("@GCminimum", entity.GCminimum)
               .WithSqlParam("@ExworksYearTwo", entity.ExworksYearTwo)
               .WithSqlParam("@GCminimumYearTwo", entity.GCminimumYearTwo)
               .WithSqlParam("@ExworksYearThree", entity.ExworksYearThree)
               .WithSqlParam("@GCminimumYearThree", entity.GCminimumYearThree)
               .WithSqlParam("@Expenses", entity.Expenses)
               .WithSqlParam("@FilingCost", entity.FilingCost)
               .WithSqlParam("@Freight", entity.Freight)
               .WithSqlParam("@FreightYearTwo", entity.FreightYearTwo)
               .WithSqlParam("@FreightYearThree", entity.FreightYearThree)
               .WithSqlParam("@TotalContribution", entity.TotalContribution)
               // .WithSqlParam("@TotalPercentage", entity.TotalPercentage)
               .WithSqlParam("@NetContribution", entity.NetContribution)
                //.WithSqlParam("@NetPercentage", entity.NetPercentage)
               .WithSqlParam("@CreatedBy", entity.Createdby)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                 .WithSqlParam("@LitigationCost", entity.LitigationCost)
                  .WithSqlParam("@FreightCost", entity.FreightCost)
                   .WithSqlParam("@RegistrationCost", entity.RegistrationCost)
                    .WithSqlParam("@ConsultantCost", entity.ConsultantCost)
                     .WithSqlParam("@LegalizationCost", entity.LegalizationCost)
                      .WithSqlParam("@TranslationCost", entity.TranslationCost)
                       .WithSqlParam("@OtherCost", entity.OtherCost)
                        .WithSqlParam("@BECost", entity.BECost)
                         .WithSqlParam("@BioCost", entity.BioCost)
                          .WithSqlParam("@CTCost", entity.CTCost)

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
