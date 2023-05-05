using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfHeadWiseBudget
    {
        public int HeadWiseBudgetId { get; set; }
        public int ProjectActivitiesId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? Prototype { get; set; }
        public double? ScaleUp { get; set; }
        public double? Exhibit { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
    }
}
