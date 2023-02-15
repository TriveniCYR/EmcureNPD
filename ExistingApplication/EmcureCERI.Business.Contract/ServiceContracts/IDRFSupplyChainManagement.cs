using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
   public interface IDRFSupplyChainManagement
    {
        int insertDRFSCM(Tbl_DRF_SupplyChainMgmt entity);
        int updateDRFSCM(Tbl_DRF_SupplyChainMgmt entity);
        IList<Tbl_DRF_SupplyChainMgmt> GetSCMFilledDetails(int InitializationID);
    }
}
