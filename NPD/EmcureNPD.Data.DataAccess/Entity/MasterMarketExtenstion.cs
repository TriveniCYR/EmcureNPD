using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterMarketExtenstion
    {
        public MasterMarketExtenstion()
        {
            PidfApiRnDs = new HashSet<PidfApiRnD>();
            Pidfs = new HashSet<Pidf>();
        }

        public int MarketExtenstionId { get; set; }
        public string MarketExtenstionName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<PidfApiRnD> PidfApiRnDs { get; set; }
        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
