using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDRFIP
    {
        int insertDRFIP(DRFIPHeaderAndDetails entity);
        int updateDRFIP(DRFIPHeaderAndDetails entity);
        IList<Tbl_DRF_IP_Details> GetIPHeaderFilledDetails(int InitializationID);
        IList<Tbl_DRF_Patent_Details> GetIPHeaderDetailsFilledDetails(int InitializationID);
    }
}
