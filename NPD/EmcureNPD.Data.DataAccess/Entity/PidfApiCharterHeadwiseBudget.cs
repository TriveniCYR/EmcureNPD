using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterHeadwiseBudget
    {
        public int PidfApiCharterHeadwiseBudgetId { get; set; }
        public int PidfApiCharterId { get; set; }
        public long Pidfid { get; set; }
        public int? HeadwiseBudgetId { get; set; }
        public string HeadwiseBudgetValue { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
