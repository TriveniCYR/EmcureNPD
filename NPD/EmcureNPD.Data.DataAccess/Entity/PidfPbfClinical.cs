using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfClinical
    {
        public long PbfclinicalId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int BioStudyTypeId { get; set; }
        public double? FastingOrFed { get; set; }
        public int? NumberofVolunteers { get; set; }
        public double? ClinicalCostAndVolume { get; set; }
        public double? BioAnalyticalCostAndVolume { get; set; }
        public double? DocCostandStudy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
