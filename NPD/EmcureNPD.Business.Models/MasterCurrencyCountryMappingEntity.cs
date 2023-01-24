using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterCurrencyCountryMappingEntity
    {
        public int CurrencyCountryMappingId { get; set; }
        public int CurrencyId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MasterCurrencyEntity MasterCurrencyEntity { get; set; }


        public MasterCountryEntity MasterCountryEntity { get; set; }
    }
}
