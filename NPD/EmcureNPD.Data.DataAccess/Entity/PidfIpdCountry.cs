using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdCountry
    {
        public long IpdcountryId { get; set; }
        public long Ipdid { get; set; }
        public int? CountryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual PidfIpd Ipd { get; set; }
    }
}
