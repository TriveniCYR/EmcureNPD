using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalytical
    {
        public long PidfpbfanalyticalId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int TestTypeId { get; set; }
        public int ActivityTypeId { get; set; }
        public string Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public int? CostPerTest { get; set; }
        public int PrototypeCost { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
        public virtual MasterTestType TestType { get; set; }
    }
}
