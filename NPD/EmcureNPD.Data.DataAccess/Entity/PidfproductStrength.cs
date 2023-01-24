using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfproductStrength
    {
        public PidfproductStrength()
        {
            PidfCommercials = new HashSet<PidfCommercial>();
        }

        public long PidfproductStrengthId { get; set; }
        public long Pidfid { get; set; }
        public string Strength { get; set; }
        public int UnitofMeasurementId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterUnitofMeasurement UnitofMeasurement { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
    }
}
