using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class Pidfapidetail
    {
        public long Pidfapiid { get; set; }
        public long Pidfid { get; set; }
        public string Apiname { get; set; }
        public int? ApisourcingId { get; set; }
        public string Apivendor { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
        public int? BusinessUnitId { get; set; }

        public virtual MasterApisourcing Apisourcing { get; set; }
        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual Pidf Pidf { get; set; }
    }
}
