using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class GanttTaskData
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string order { get; set; }
        public int duration { get; set; }
        public int? owner_id { get; set; }
        public decimal progress { get; set; }
        public int? parent { get; set; }
        public string type { get; set; }        
        public bool open { get { return true; } set { } }
        public string target { get; set; }
        public string priority { get; set; }
        public string deadLine { get; set; }
        public string planned_start { get; set; }
        public string planned_end { get; set; }

        public static explicit operator GanttTaskData(GanttTaskVM task)
        {
            return new GanttTaskData
            {
                id = task.Id,
                text = task.Text,
                start_date = task.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = task.EndDate.ToString("yyyy-MM-dd HH:mm"),
                order = Convert.ToString(task.SortOrder),
                duration = task.Duration,
                parent = task.ParentId,
                type = task.Type,
                progress = task.Progress,
                owner_id = task.Owner,
                priority = task.Priority,
                deadLine = task.deadLine.ToString("yyyy-MM-dd HH:mm"),// task.DeadLine,
                planned_start = task.planned_start.ToString("yyyy-MM-dd HH:mm"),// task.Planned_Start,
                planned_end = task.planned_end.ToString("yyyy-MM-dd HH:mm")
            };
        }

        public static explicit operator GanttTaskVM(GanttTaskData task)
        {

            return new GanttTaskVM
            {
                Id = task.id,
                Text = task.text,
                StartDate = DateTime.Parse(task.start_date, System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.Parse(task.end_date, System.Globalization.CultureInfo.InvariantCulture),
                SortOrder = Convert.ToInt16(task.order),
                Duration = task.duration,
                ParentId = task.parent,
                Type = task.type,
                Progress = task.progress,
                Owner = task.owner_id,
                Priority = task.priority,
                deadLine = DateTime.Parse(task.deadLine, System.Globalization.CultureInfo.InvariantCulture),//task.DeadLine,
                planned_start = DateTime.Parse(task.planned_start, System.Globalization.CultureInfo.InvariantCulture),//task.Planned_Start,
                planned_end = DateTime.Parse(task.planned_end, System.Globalization.CultureInfo.InvariantCulture),//task.Planned_End
            };
        }
    }
}
