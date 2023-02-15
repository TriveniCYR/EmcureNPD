using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    class DRFTaskSubTaskList
    {
    }

    public class DRFTaskList
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int EMPID { get; set; }
        public string EmpName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public decimal TotalPercentage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string TaskStatus { get; set; }
    }

    public class DRFSubTaskList
    {
        public int TaskID { get; set; }
        public int SubTaskID { get; set; }
        public string SubTask { get; set; }
        public int TaskStatusID { get; set; }
        public string TaskStatus { get; set; }
        public int EMPID { get; set; }
        public string EmpName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public decimal TotalPercentage { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
