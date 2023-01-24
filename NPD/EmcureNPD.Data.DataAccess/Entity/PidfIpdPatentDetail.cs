using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdPatentDetail
    {
        public long PatentDetailsId { get; set; }
        public long Ipdid { get; set; }
        public string PatentNumber { get; set; }
        public string Type { get; set; }
        public DateTime? OriginalExpiryDate { get; set; }
        public DateTime? ExtensionExpiryDate { get; set; }
        public string Comments { get; set; }
        public string Strategy { get; set; }

        public virtual PidfIpd Ipd { get; set; }
    }
}
