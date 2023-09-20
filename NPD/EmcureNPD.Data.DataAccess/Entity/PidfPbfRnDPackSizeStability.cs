using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPackSizeStability
    {
        public long PackSizeStabilityId { get; set; }
        public long Pidfid { get; set; }
        public long PbfgeneralId { get; set; }
        public int CountryId { get; set; }
        public int? StrengthId { get; set; }
        public int? PackSizeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
