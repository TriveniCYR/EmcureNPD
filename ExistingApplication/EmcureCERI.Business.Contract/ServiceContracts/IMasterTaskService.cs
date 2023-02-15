using System;
using System.Collections.Generic;
using System.Text;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IMasterTaskService
    {
        IList<Tbl_Master_Task> GetAllTask();
        int InsertTaskDetails(Tbl_Master_Task tbl_Master_Task);
        int UpdateTaskDetails(Tbl_Master_Task tbl_Master_Task);
    }
}
