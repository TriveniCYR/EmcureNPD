using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfMarketMapping
    {
        public long PidfpbfmarketId { get; set; }
        public long Pidfpbfid { get; set; }
        public int BusinessUnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual PidfPbf Pidfpbf { get; set; }
    }
}
