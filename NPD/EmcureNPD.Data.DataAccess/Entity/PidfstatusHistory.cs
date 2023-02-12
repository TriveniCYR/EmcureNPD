using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfstatusHistory
    {
        public long PidfstatusHistoryId { get; set; }
        public int StatusId { get; set; }
        public long Pidfid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterPidfstatus Status { get; set; }
    }
}
