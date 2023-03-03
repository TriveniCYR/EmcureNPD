using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfClinicalPilotBioFed
    {
        public int PilotBioFedid { get; set; }
        public long PbfclinicalId { get; set; }
        public long StrengthId { get; set; }
        public string Fed { get; set; }
        public int? NumberofVolunteers { get; set; }
        public decimal? ClinicalCostandVol { get; set; }
        public decimal? DocCostandStudy { get; set; }
        public decimal TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual PidfPbfClinical Pbfclinical { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
