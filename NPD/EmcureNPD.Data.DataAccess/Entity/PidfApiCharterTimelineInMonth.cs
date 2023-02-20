﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterTimelineInMonth
    {
        public int PidfApiCharterTimelineInMonthsId { get; set; }
        public int PidfApiCharterId { get; set; }
        public long Pidfid { get; set; }
        public int? TimelineInMonthsId { get; set; }
        public string TimelineInMonthsValue { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
