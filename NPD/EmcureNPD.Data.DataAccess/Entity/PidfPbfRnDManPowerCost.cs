using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDManPowerCost
    {
        public int ManPowerCostId { get; set; }
        public int ProjectActivitiesId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? DurationInDays { get; set; }
        public double? ManPowerInDays { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual MasterProjectActivity ProjectActivities { get; set; }
    }
}
