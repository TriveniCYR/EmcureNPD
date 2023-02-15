using System;
using System.Collections.Generic;
using System.Text;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface ITransactionsProjectTask
    {
        IList<Tbl_Transaction_ProjectTask> GetAllProjectTaskTransaction(int ProjectID);
        int InsertTransactionProjectTask(Tbl_Transaction_ProjectTask tbl_Transaction_ProjectTask);
        int UpdateTransactionProjectTask(Tbl_Transaction_ProjectTask tbl_Transaction_ProjectTask);
    }
}
