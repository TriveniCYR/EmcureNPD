using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public class DRFSupplyChainManagement : IDRFSupplyChainManagement
    {
        private readonly EmcureCERIDBContext _db;

        public DRFSupplyChainManagement()
        {
            _db = new EmcureCERIDBContext();
        }

        public IList<Tbl_DRF_SupplyChainMgmt> GetSCMFilledDetails(int InitializationID)
        {
            IList<Tbl_DRF_SupplyChainMgmt> result = new List<Tbl_DRF_SupplyChainMgmt>();
            try
            {
                _db.LoadStoredProc("USP_GetDRFIPMANSCMRAFilledUpDetails")
                .WithSqlParam("@InitializationID", InitializationID)
                .WithSqlParam("@Action", "SCM")
                .ExecuteStoredProc((handler) =>
                {
                    result = handler.ReadToList<Tbl_DRF_SupplyChainMgmt>();
                });

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public int insertDRFSCM(Tbl_DRF_SupplyChainMgmt entity)
        {
            try
            {
                _db.LoadStoredProc("USP_InsertDRFSCMDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@FreightCost", entity.FreightCost)
                .WithSqlParam("@TentativeDestination", entity.TentativeDestination)
                 .WithSqlParam("@TentativeShipmente", entity.TentativeShipmente)
                 .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
                .ExecuteStoredNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updateDRFSCM(Tbl_DRF_SupplyChainMgmt entity)
        {
            try
            {
                _db.LoadStoredProc("USP_UpdateDRFSCMDetails")
                .WithSqlParam("@InitializationId", entity.InitializationId)
                .WithSqlParam("@FreightCost", entity.FreightCost)
                .WithSqlParam("@TentativeDestination", entity.TentativeDestination)
                 .WithSqlParam("@TentativeShipmente", entity.TentativeShipmente)
                 .WithSqlParam("@Remark", entity.Remark)
                .WithSqlParam("@Createdby", entity.CreatedBy)
                .WithSqlParam("@CreatedDate", entity.CreatedDate)
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
