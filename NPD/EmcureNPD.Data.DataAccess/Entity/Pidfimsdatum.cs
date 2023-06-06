using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class Pidfimsdatum
    {
        public long PidfimsdataId { get; set; }
        public long Pidfid { get; set; }
        public double Imsvalue { get; set; }
        public double Imsvolume { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }

        public virtual Pidf Pidf { get; set; }
    }
}
