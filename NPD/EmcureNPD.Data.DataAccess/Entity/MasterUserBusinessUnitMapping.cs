using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUserBusinessUnitMapping
    {
        public int UserBusinessUnitId { get; set; }
        public int BusinessUnitId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterUser User { get; set; }
    }
}
