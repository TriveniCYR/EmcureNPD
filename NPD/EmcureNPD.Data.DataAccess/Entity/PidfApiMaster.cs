using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiMaster
    {
        public long PidfapimasterId { get; set; }
        public long Pidfid { get; set; }
        public int? UserId { get; set; }
        public bool Interested { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual Pidf Pidf { get; set; }
    }
}
