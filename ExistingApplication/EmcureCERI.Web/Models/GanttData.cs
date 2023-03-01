using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class GanttData
    {
        public IEnumerable<GanttTaskData> data { get; set; }
        public IEnumerable<GanttLinkData> links { get; set; }
        public Dictionary<string, object> collections { get; set; }
    }
}
