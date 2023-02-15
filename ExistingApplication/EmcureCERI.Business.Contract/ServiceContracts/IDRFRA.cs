using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
   public interface IDRFRA
    {
        int insertDRFRA(Tbl_DRF_Requisite_RAInfo entity,int DossierTemplateID);

        int updateDRFRA(Tbl_DRF_Requisite_RAInfo entity, int DossierTemplateID); 

        IList<Tbl_DRF_Requisite_RAInfo> GetRAFilledDetails(int InitializationID);
    }
}
