using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalCost
    {
        public long PbfanalyticalCostId { get; set; }
        public long? PbfanalyticalId { get; set; }
        public long? StrengthId { get; set; }
        public double? TotalAmvcost { get; set; }
        public string Remark { get; set; }
        public double? TotalPrototypeCost { get; set; }
        public double? TotalScaleupCost { get; set; }
        public double? TotalExhibitCost { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfAnalytical Pbfanalytical { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
