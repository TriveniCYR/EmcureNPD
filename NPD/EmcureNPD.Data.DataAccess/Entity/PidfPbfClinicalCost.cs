using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfClinicalCost
    {
        public long PbfclinicalCostId { get; set; }
        public long Pbfgeneralld { get; set; }
        public long StrengthId { get; set; }
        public double? TotalPilotFastingCost { get; set; }
        public double? TotalPilotFedcost { get; set; }
        public double? TotalPivotalFastingCost { get; set; }
        public double? TotalPivotalFedcost { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral PbfgeneralldNavigation { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
