using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiIpd
    {
        public long PidfApiIpdId { get; set; }
        public long? Pidfid { get; set; }
        public string MarketDetailsFileName { get; set; }
        public int? ProductTypeId { get; set; }
        public string DrugsCategory { get; set; }
        public string ProductStrength { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public int? BusinessUnitId { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual MasterProductType ProductType { get; set; }
    }
}
