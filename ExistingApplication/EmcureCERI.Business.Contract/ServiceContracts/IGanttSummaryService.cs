using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IGanttSummaryService
    {
        IList<SummaryGanttDataModel> GetAllProjectSummary(string strAction);
    }
}
