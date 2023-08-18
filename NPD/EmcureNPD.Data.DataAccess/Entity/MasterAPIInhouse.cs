using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterApiInhouse
    {
        public MasterApiInhouse()
        {
            PidfApiInhouses = new HashSet<PidfApiInhouse>();
        }

        public int ApiinhouseId { get; set; }
        public string ApiinhouseName { get; set; }

        public virtual ICollection<PidfApiInhouse> PidfApiInhouses { get; set; }
    }
}
