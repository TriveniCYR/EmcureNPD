using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class clsAllContinentData
    {
        public IList<PidfCountryDetailsNew> CIS_ContinentCountries { get; set; }
        public IList<PidfCountryDetailsNew> LATAM_ContinentCountries { get; set; }
        public IList<PidfCountryDetailsNew> ASIA_ContinentCountries { get; set; }
        public IList<PidfCountryDetailsNew> AFRICA_ContinentCountries { get; set; }
        public IList<PidfCountryDetailsNew> MENA_ContinentCountries { get; set; }

    }
}
