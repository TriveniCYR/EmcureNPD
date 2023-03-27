using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDExicipient
    {
        public long PidfpbfrndexicipientId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ExicipientPrototype { get; set; }
        public string ExicipientDevelopment { get; set; }
        public double? RsPerKg { get; set; }
        public string MgPerUnitDosage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
