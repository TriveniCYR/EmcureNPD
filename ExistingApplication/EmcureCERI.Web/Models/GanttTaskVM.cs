using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class GanttTaskVM
    {
        public int Id { get; set; }
        //[MaxLength(255)]
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int SortOrder { get; set; }
        public decimal Progress { get; set; }
        public int? ParentId { get; set; }
        public int? Owner { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public DateTime deadLine { get; set; }
        public DateTime planned_start { get; set; }
        public DateTime planned_end { get; set; }
    }
}
