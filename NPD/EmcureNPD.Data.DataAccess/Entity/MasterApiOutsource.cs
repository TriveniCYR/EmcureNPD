using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterApiOutsource
    {
        public MasterApiOutsource()
        {
            PidfApiOutsourceData = new HashSet<PidfApiOutsourceDatum>();
        }

        public int ApioutsourceId { get; set; }
        public string ApioutsourceName { get; set; }

        public virtual ICollection<PidfApiOutsourceDatum> PidfApiOutsourceData { get; set; }
    }
}
