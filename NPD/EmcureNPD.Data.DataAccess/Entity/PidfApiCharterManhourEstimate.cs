using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterManhourEstimate
    {
        public long PidfApiCharterManhourEstimatesId { get; set; }
        public long PidfApiCharterId { get; set; }
        public long Pidfid { get; set; }
        public int? ManhourEstimatesId { get; set; }
        public string ManhourEstimatesNoOfEmployeeValue { get; set; }
        public string ManhourEstimatesMonthsValue { get; set; }
        public string ManhourEstimatesHoursValue { get; set; }
        public string ManhourEstimatesCostValue { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual PidfApiCharter PidfApiCharter { get; set; }
    }
}
