using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRa
    {
        public long Pidfpbfraid { get; set; }
        public long Pidfid { get; set; }
        public long Pbfid { get; set; }
        public int CountryIdBuId { get; set; }
        public DateTime? PivotalBatchManufactured { get; set; }
        public DateTime? LastDataFromRnD { get; set; }
        public DateTime? BefinalReport { get; set; }
        public int CountryId { get; set; }
        public int? TypeOfSubmissionId { get; set; }
        public DateTime? DossierReadyDate { get; set; }
        public DateTime? EarliestSubmissionDexcl { get; set; }
        public DateTime? EarliestLaunchDexcl { get; set; }
        public DateTime? LasDateToRegulatory { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? CreatedBy { get; set; }

        public virtual PidfPbf Pbf { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
