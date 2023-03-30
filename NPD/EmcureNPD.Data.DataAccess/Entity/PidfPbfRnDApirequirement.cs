using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDApirequirement
    {
        public long ApirequirementId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string Prototype { get; set; }
        public string ScaleUp { get; set; }
        public string ExhibitBatch1 { get; set; }
        public string ExhibitBatch2 { get; set; }
        public string ExhibitBatch3 { get; set; }
        public double? PrototypeCost { get; set; }
        public double? ScaleUpCost { get; set; }
        public double? ExhibitBatchCost { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
