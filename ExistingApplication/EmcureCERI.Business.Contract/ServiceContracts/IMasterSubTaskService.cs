using System;
using System.Collections.Generic;
using System.Text;
using EmcureCERI.Data.DataAccess.Entities;

namespace EmcureCERI.Business.Core.ServiceImplementations
{
    public interface IMasterSubTaskService
    {
        IList<Tbl_Master_Task> GetAllTask();
        IList<Tbl_Master_SubTask> GetAllSubTask(int TaskID);
        int InsertTaskDetails(Tbl_Master_SubTask tbl_Master_SubTask);
        int UpdateTaskDetails(Tbl_Master_SubTask tbl_Master_SubTask);
    }
}
