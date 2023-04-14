using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPackagingMaterial
    {
        public long PidfpbfrndpackagingId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public int? PackingTypeId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double? PackagingDevelopment { get; set; }
        public double? RsPerUnit { get; set; }
        public int? Quantity { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterPackingType PackingType { get; set; }
        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
