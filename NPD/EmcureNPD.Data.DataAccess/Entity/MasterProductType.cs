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
            PidfPbfs = new HashSet<PidfPbf>();
        }

        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ProductTypeFactor { get; set; }
        public double? ManPowerFactor { get; set; }

        public virtual ICollection<PidfApiIpd> PidfApiIpds { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
    }
}
