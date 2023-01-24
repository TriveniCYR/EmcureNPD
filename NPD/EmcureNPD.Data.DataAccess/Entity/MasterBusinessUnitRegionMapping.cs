using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterBusinessUnitRegionMapping
    {
        public int BusinessUnitCountryMappingId { get; set; }
        public int BusinessUnitId { get; set; }
        public int RegionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterRegion Region { get; set; }
    }
}
