using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IReportsService
    {

        Object GetAllReportList();
        IList<Tbl_DRF_Initialization> GetAllReportList1();
        IList<TBL_Initialization_List> GetReportofMoleculeTimeline();
        IList<DRFHistoryDetails> GetDRFHistoryDetailsByID(int DRFID);
        IList<TBL_Initialization_ListForManF> GetReportofManfacturing();
    }
}
