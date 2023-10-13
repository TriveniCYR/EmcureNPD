using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfPhaseWiseBudget
    {
        public int PhaseWiseBudgetId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? FeasabilityCumTotal { get; set; }
        public double? PrototypeCumTotal { get; set; }
        public double? ScaleUpCumTotal { get; set; }
        public double? AmvcumTotal { get; set; }
        public double? ExhibitCumTotal { get; set; }
        public double? FilingCumTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? FeasabilityCumTotalDate { get; set; }
        public DateTime? PrototypeCumTotalDate { get; set; }
        public DateTime? ScaleUpCumTotalDate { get; set; }
        public DateTime? AmvcumTotalDate { get; set; }
        public DateTime? ExhibitCumTotalDate { get; set; }
        public DateTime? FilingCumTotalDate { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
    }
}
