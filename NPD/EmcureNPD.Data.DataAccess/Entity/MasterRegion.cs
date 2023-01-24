using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterRegion
    {
        public MasterRegion()
        {
            MasterBusinessUnitRegionMappings = new HashSet<MasterBusinessUnitRegionMapping>();
            MasterRegionCountryMappings = new HashSet<MasterRegionCountryMapping>();
            MasterUserRegionMappings = new HashSet<MasterUserRegionMapping>();
            PidfIpdRegions = new HashSet<PidfIpdRegion>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<MasterBusinessUnitRegionMapping> MasterBusinessUnitRegionMappings { get; set; }
        public virtual ICollection<MasterRegionCountryMapping> MasterRegionCountryMappings { get; set; }
        public virtual ICollection<MasterUserRegionMapping> MasterUserRegionMappings { get; set; }
        public virtual ICollection<PidfIpdRegion> PidfIpdRegions { get; set; }
    }
}
