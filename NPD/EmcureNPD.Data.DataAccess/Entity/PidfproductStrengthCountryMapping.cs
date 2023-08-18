using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfproductStrengthCountryMapping
    {
        public long PidfproductStrengthCountryId { get; set; }
        public long PidfproductStrengthId { get; set; }
        public int CountryId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual PidfproductStrength PidfproductStrength { get; set; }
    }
}
