using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterProductType
    {
        public MasterProductType()
        {
            PidfApiIpds = new HashSet<PidfApiIpd>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfs = new HashSet<PidfPbf>();
        }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
    }
}
