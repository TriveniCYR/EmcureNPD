using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiInhouse
    {
        public int PidfapiinhouseId { get; set; }
        public int? ApiinhouseId { get; set; }
        public long? Pidfid { get; set; }
        public string Primary { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual MasterApiInhouse Apiinhouse { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
