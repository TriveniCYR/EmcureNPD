using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterRegionCountryMapping
    {
        public int RegionCountryMappingId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual MasterRegion Region { get; set; }
    }
}
