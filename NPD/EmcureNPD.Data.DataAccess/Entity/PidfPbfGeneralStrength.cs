using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfGeneralStrength
    {
        public long PidfpbfgeneralStrengthId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string ProjectCode { get; set; }
        public string ImprintingEmbossingCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
