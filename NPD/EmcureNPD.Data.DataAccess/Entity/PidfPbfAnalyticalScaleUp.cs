using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalScaleUp
    {
        public int ScaleUpId { get; set; }
        public long PbfanalyticalId { get; set; }
        public long StrengthId { get; set; }
        public int? TestTypeId { get; set; }
        public int? Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public decimal? Cost { get; set; }
        public decimal? PrototypeCost { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfPbfAnalytical Pbfanalytical { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
        public virtual MasterTestType TestType { get; set; }
    }
}
