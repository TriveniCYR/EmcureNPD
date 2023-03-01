using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterTimelineInMonth
    {
        public long PidfApiCharterTimelineInMonthsId { get; set; }
        public long PidfApiCharterId { get; set; }
        public int? TimelineInMonthsId { get; set; }
        public string TimelineInMonthsValue { get; set; }

        public virtual PidfApiCharter PidfApiCharter { get; set; }
    }
}
