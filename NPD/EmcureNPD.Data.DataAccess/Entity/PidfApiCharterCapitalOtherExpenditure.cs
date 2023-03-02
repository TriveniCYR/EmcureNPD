using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharterCapitalOtherExpenditure
    {
        public long PidfApiCharterCapitalOtherExpenditureId { get; set; }
        public long PidfApiCharterId { get; set; }
        public long? Pidfid { get; set; }
        public int? CapitalOtherExpenditureId { get; set; }
        public string CapitalOtherExpenditureAmountValue { get; set; }
        public string CapitalOtherExpenditureRemarkValue { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual PidfApiCharter PidfApiCharter { get; set; }
    }
}
