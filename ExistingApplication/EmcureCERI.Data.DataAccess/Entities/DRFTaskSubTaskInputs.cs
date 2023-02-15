using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class TaskSubTaskInputs
    {
        public int TaskOrder { get; set; }
        public string TaskName { get; set; }
        public int ParentID { get; set; }
        public string Action { get; set; }
        public int DRFID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PriorityID { get; set; }
        public string Priority { get; set; }
        public int TaskStatusID { get; set; }
        public string TaskStatus { get; set; }
        public int TaskDuration { get; set; }
        public decimal TotalPercentage { get; set; }
        public int EmpID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

       // public int SortOrder { get; set; }

    }
}
