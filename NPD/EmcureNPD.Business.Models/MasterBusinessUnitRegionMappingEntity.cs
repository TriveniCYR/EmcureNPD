using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public  class MasterBusinessUnitRegionMappingEntity
    {
        public int BusinessUnitCountryMappingId { get; set; }
        public int BusinessUnitId { get; set; }
        public int RegionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterBusinessUnitEntity MasterBusinessUnitEntity { get; set; }
        public virtual MasterRegionEntity MasterRegionEntity { get; set; }
    }
}
