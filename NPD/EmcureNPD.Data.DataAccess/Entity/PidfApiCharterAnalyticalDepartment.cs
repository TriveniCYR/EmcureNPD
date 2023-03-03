using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterAnalyticalDepartment
    {
        public long PidfApiCharterAnalyticalDepartmentId { get; set; }
        public long PidfApiCharterId { get; set; }
        public long? Pidfid { get; set; }
        public int? AnalyticalDepartmentId { get; set; }
        public string AnalyticalDepartmentArdvalue { get; set; }
        public string AnalyticalDepartmentImpurityValue { get; set; }
        public string AnalyticalDepartmentStabilityValue { get; set; }
        public string AnalyticalDepartmentAmvvalue { get; set; }
        public string AnalyticalDepartmentAmtvalue { get; set; }

        public virtual PidfApiCharter PidfApiCharter { get; set; }
    }
}
