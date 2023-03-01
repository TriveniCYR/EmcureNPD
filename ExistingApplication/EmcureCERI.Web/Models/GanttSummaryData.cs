using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class GanttSummaryData
    {
        public IEnumerable<GanttSummaryTaskData> data { get; set; }
    }
}
