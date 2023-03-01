using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDRFFinance
    {
        int insertDRFFinanceApprovel(Tbl_DRF_FinanceDetails entity);
        int updateDRFFinanceApprovel(Tbl_DRF_FinanceDetails entity);
    }
}
