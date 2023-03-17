using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalCostStrengthMapping
    {
        public long PbfanalyticalCostStrengthId { get; set; }
        public long PbfanalyticalCostId { get; set; }
        public long StrengthId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfAnalyticalCost PbfanalyticalCost { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
