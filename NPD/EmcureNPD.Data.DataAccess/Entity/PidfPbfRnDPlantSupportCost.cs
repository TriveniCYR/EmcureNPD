using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPlantSupportCost
    {
        public long PlantSupportCostId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public double? ScaleUp { get; set; }
        public double? Exhibit { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
