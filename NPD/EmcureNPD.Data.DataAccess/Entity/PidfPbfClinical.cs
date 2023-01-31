using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfClinical
    {
        public int ClinicalId { get; set; }
        public int? StrengthId { get; set; }
        public int? PilotBioFastingId { get; set; }
        public int? PilotBioFedid { get; set; }
        public int? PivotalBioFastingId { get; set; }
        public int? PivotalBioFedid { get; set; }
        public int? TotalCostId { get; set; }
    }
}
