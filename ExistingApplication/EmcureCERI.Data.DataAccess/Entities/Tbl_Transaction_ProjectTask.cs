using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Transaction_ProjectTask
    {
        public Int64 ProjectTransactionID { get; set; }        
        public Int64? ProjectTaskMappingID { get; set; }        
        public int? Drfid { get; set; }        
        public int? Pidfid { get; set; }        
        public int? CountryID { get; set; }
        public int? TaskID { get; set; }        
        public int? SubTaskID { get; set; }        
        public DateTime? StartDate { get; set; }        
        public DateTime? EndDate { get; set; }        
        public int? TaskStatusID { get; set; }
        public string TaskStatus { get; set; }
        public int? TaskDuration { get; set; }        
        public decimal? TotalPercentage { get; set; }       
        public int? ProjectOwnerID { get; set; }
        public int? EmpID { get; set; }
        public bool IsActive { get; set; }        
        public int? CreatedBy { get; set; }        
        public DateTime? CreatedDate { get; set; }        
        public int? ModifiedBy { get; set; }        
        public DateTime? ModifyDate { get; set; }
    }
}
