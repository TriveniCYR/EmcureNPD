using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterBusinessUnitCountryMapping
    {
        public int BusinessUnitCountryMappingId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterCountry Country { get; set; }
    }
}
