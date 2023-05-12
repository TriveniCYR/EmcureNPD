using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharter
    {
        public PidfApiCharter()
        {
            PidfApiCharterAnalyticalDepartments = new HashSet<PidfApiCharterAnalyticalDepartment>();
            PidfApiCharterCapitalOtherExpenditures = new HashSet<PidfApiCharterCapitalOtherExpenditure>();
            PidfApiCharterHeadwiseBudgets = new HashSet<PidfApiCharterHeadwiseBudget>();
            PidfApiCharterManhourEstimates = new HashSet<PidfApiCharterManhourEstimate>();
            PidfApiCharterPrddepartments = new HashSet<PidfApiCharterPrddepartment>();
            PidfApiCharterTimelineInMonths = new HashSet<PidfApiCharterTimelineInMonth>();
        }

        public long PidfApiCharterId { get; set; }
        public long Pidfid { get; set; }
        public string ApigroupLeader { get; set; }
        public int ProjectComplexityId { get; set; }
        public string ManHourRates { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual ICollection<PidfApiCharterAnalyticalDepartment> PidfApiCharterAnalyticalDepartments { get; set; }
        public virtual ICollection<PidfApiCharterCapitalOtherExpenditure> PidfApiCharterCapitalOtherExpenditures { get; set; }
        public virtual ICollection<PidfApiCharterHeadwiseBudget> PidfApiCharterHeadwiseBudgets { get; set; }
        public virtual ICollection<PidfApiCharterManhourEstimate> PidfApiCharterManhourEstimates { get; set; }
        public virtual ICollection<PidfApiCharterPrddepartment> PidfApiCharterPrddepartments { get; set; }
        public virtual ICollection<PidfApiCharterTimelineInMonth> PidfApiCharterTimelineInMonths { get; set; }
    }
}
