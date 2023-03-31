using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDReferenceProductDetail
    {
        public long ReferenceProductDetailId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? ReferenceProductDetailDevelopment { get; set; }
        public double? UnitCostOfReferenceProduct { get; set; }
        public double? FormulationDevelopment { get; set; }
        public double? PilotBe { get; set; }
        public double? PharmasuiticalEquivalence { get; set; }
        public double? PivotalBio { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
