using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfBusinessUnitCountry
    {
        public long PidfbusinessUnitCountryId { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
    }
}
