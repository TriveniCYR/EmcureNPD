using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class SummaryGanttDataModel
    {
        public int id { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TaskDuration {get;set;}
        public string Owner { get; set; }
        public decimal TotalProgress { get; set; }
        public decimal ProjectStatus { get; set; }
        public string Priority { get; set; }

    }
}
