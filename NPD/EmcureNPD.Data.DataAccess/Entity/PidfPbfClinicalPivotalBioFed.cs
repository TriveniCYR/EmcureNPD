using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfClinicalPivotalBioFed
    {
        public int PivotalBioFedid { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string Fed { get; set; }
        public int? NumberofVolunteers { get; set; }
        public decimal? ClinicalCostandVol { get; set; }
        public decimal? DocCostandStudy { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
