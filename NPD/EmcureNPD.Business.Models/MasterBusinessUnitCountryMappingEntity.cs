using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterBusinessUnitCountryMappingEntity
    {
        public int BusinessUnitCountryMappingId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MasterBusinessUnitEntity MasterBusinessUnitEntity { get; set; }


        public MasterCountryEntity MasterCountryEntity { get; set; }
    }
}
