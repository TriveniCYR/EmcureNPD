using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiOutsourceDatum
    {
        public int ApioutsourceDataId { get; set; }
        public int? ApioutsourceId { get; set; }
        public long? Pidfid { get; set; }
        public string Primary { get; set; }
        public string PotentialAlt1 { get; set; }
        public string PotentialAlt2 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual MasterApiOutsource Apioutsource { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
