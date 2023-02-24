using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterBusinessUnit
    {
        public MasterBusinessUnit()
        {
            MasterBusinessUnitRegionMappings = new HashSet<MasterBusinessUnitRegionMapping>();
            MasterDepartmentBusinessUnitMappings = new HashSet<MasterDepartmentBusinessUnitMapping>();
            MasterUserBusinessUnitMappings = new HashSet<MasterUserBusinessUnitMapping>();
            PidfApiIpds = new HashSet<PidfApiIpd>();
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfIpds = new HashSet<PidfIpd>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            Pidfs = new HashSet<Pidf>();
        }

        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public virtual ICollection<MasterBusinessUnitRegionMapping> MasterBusinessUnitRegionMappings { get; set; }
        public virtual ICollection<MasterDepartmentBusinessUnitMapping> MasterDepartmentBusinessUnitMappings { get; set; }
        public virtual ICollection<MasterUserBusinessUnitMapping> MasterUserBusinessUnitMappings { get; set; }
        public virtual ICollection<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfIpd> PidfIpds { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
