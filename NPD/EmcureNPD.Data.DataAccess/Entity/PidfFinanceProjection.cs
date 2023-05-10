using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfFinanceProjection
    {
        public int FinanceProjectionId { get; set; }
        public int PidffinaceId { get; set; }
        public int? BusinessUnitId { get; set; }
        public long? Pidfid { get; set; }
        public string Year { get; set; }
        public double? Expiries { get; set; }
        public double? AnnualFee { get; set; }
        public double? AnnualConfirmatoryRelease { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual PidfFinance Pidffinace { get; set; }
    }
}
