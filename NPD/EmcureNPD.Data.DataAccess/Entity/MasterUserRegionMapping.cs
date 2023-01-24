using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUserRegionMapping
    {
        public int UserRegionId { get; set; }
        public int RegionId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterRegion Region { get; set; }
        public virtual MasterUser User { get; set; }
    }
}
