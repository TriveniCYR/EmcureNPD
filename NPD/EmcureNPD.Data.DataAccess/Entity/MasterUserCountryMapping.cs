using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUserCountryMapping
    {
        public int UserCountryId { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual MasterUser User { get; set; }
    }
}
