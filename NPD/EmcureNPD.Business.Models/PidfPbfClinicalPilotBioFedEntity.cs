using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{ 
	public partial class PidfPbfClinicalPilotBioFedEntity
    {
        public int PilotBioFedid { get; set; }
        public long PBFClinicalId { get; set; }
        public long StrengthId { get; set; }
        public string Fed { get; set; }
        public int? NumberofVolunteers { get; set; }
        public decimal? ClinicalCostandVol { get; set; }
        public decimal? DocCostandStudy { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
