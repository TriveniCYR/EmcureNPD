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
            PidfPbfAnalyticalCostStrengthMappings = new HashSet<PidfPbfAnalyticalCostStrengthMapping>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfGeneralStrengths = new HashSet<PidfPbfGeneralStrength>();
            PidfPbfRnDExicipientPrototypes = new HashSet<PidfPbfRnDExicipientPrototype>();
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
        public virtual ICollection<PidfPbfAnalyticalCostStrengthMapping> PidfPbfAnalyticalCostStrengthMappings { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
    }
}
