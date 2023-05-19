using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdRegion
    {
        public long IpdregionId { get; set; }
        public long Ipdid { get; set; }
        public int? RegionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PidfIpd Ipd { get; set; }
        public virtual MasterRegion Region { get; set; }
    }
}
