using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class GanttSummaryTaskData
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int duration { get; set; }
        public int? owner_id { get; set; }
        public decimal progress { get; set; }        
        public int? parent { get; set; }
        public string type { get; set; }
        public bool open { get { return true; } set { } }
        public string target { get; set; }
        public string priority { get; set; }

        public static explicit operator GanttSummaryTaskData(GanttSummaryTaskVM task)
        {
            return new GanttSummaryTaskData
            {
                id = task.Id,
                text = task.Text,
                start_date = task.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = task.EndDate.ToString("yyyy-MM-dd HH:mm"),
                duration = task.Duration,
                parent = task.ParentId,
                type = task.Type,
                progress = task.Progress,
                owner_id = task.Owner,
                priority = task.Priority
            };
        }

        public static explicit operator GanttSummaryTaskVM(GanttSummaryTaskData task)
        {

            return new GanttSummaryTaskVM
            {
                Id = task.id,
                Text = task.text,
                StartDate = DateTime.Parse(task.start_date, System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.Parse(task.end_date, System.Globalization.CultureInfo.InvariantCulture),
                Duration = task.duration,
                ParentId = task.parent,
                Type = task.type,
                Progress = task.progress,
                Owner = task.owner_id,
                Priority = task.priority
            };
        }
    }
}
