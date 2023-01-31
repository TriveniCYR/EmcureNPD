using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterPackagingType
    {
        public MasterPackagingType()
        {
            PidfCommercialYears = new HashSet<PidfCommercialYear>();
            PidfPbfs = new HashSet<PidfPbf>();
            Pidfs = new HashSet<Pidf>();
        }

        public int PackagingTypeId { get; set; }
        public string PackagingTypeName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfCommercialYear> PidfCommercialYears { get; set; }
        public virtual ICollection<PidfPbf> PidfPbfs { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
