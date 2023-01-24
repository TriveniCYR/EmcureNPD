using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterRegionCountryMappingEntity
    {
        public int RegionCountryMappingId { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MasterRegionEntity MasterRegionEntity { get; set; }
     

        public MasterCountryEntity MasterCountryEntity { get; set; }
    }
}
