using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFMedical :IDRFMedical
    {
        private readonly EmcureCERIDBContext _db;

        public DRFMedical()
        {
            _db = new EmcureCERIDBContext();
        }

        public IList<Tbl_DRF_Medical> GetMedicalFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_Medical> result = new List<Tbl_DRF_Medical>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "MEDICAL")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_Medical>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFMedical(Tbl_DRF_Medical entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFMedicalDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@BeCtVitroAvailable", entity.BeCtVitroAvailable)
                .WithSqlParam("@BioWaiver", entity.BioWaiver)
                 .WithSqlParam("@CTWaiver", entity.CTWaiver)
                 .WithSqlParam("@Remark1", entity.Remark1)
                 .WithSqlParam("@Remark2", entity.Remark2)
                 .WithSqlParam("@Remark3", entity.Remark3)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
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

        public int updateDRFMedical(Tbl_DRF_Medical entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFMedicalDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@BeCtVitroAvailable", entity.BeCtVitroAvailable)
                .WithSqlParam("@BioWaiver", entity.BioWaiver)
                 .WithSqlParam("@CTWaiver", entity.CTWaiver)
                 .WithSqlParam("@Remark1", entity.Remark1)
                 .WithSqlParam("@Remark2", entity.Remark2)
                 .WithSqlParam("@Remark3", entity.Remark3)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
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
