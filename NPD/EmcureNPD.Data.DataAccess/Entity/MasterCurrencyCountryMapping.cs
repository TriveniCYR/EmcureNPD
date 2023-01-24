using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterCurrencyCountryMapping
    {
        public int CurrencyCountryMappingId { get; set; }
        public int CurrencyId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual MasterCurrency Currency { get; set; }
    }
}
