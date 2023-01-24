using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpd
    {
        public PidfIpd()
        {
            PidfIpdCountries = new HashSet<PidfIpdCountry>();
            PidfIpdPatentDetails = new HashSet<PidfIpdPatentDetail>();
            PidfIpdRegions = new HashSet<PidfIpdRegion>();
        }

        public long Ipdid { get; set; }
        public long? Pidfid { get; set; }
        public string MarketName { get; set; }
        public string DataExclusivity { get; set; }
        public string FillingType { get; set; }
        public string ApprovedGenetics { get; set; }
        public string LaunchedGenetics { get; set; }
        public string Innovators { get; set; }
        public string LegalStatus { get; set; }
        public int? CostOfLitication { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public int? BusinessUnitId { get; set; }
        public bool? IsComment { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual ICollection<PidfIpdCountry> PidfIpdCountries { get; set; }
        public virtual ICollection<PidfIpdPatentDetail> PidfIpdPatentDetails { get; set; }
        public virtual ICollection<PidfIpdRegion> PidfIpdRegions { get; set; }
    }
}
