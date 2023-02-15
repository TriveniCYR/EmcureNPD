using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDRFMedical
    {
        int insertDRFMedical(Tbl_DRF_Medical entity);
        int updateDRFMedical(Tbl_DRF_Medical entity);
        IList<Tbl_DRF_Medical> GetMedicalFilledDetails(int InitializationID);
    }
}
