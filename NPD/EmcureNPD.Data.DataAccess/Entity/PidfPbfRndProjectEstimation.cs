using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRndProjectEstimation
    {
        public int ProjectActivitiesId { get; set; }
        public int? DurationInDays { get; set; }
        public int ProjectActivityId { get; set; }
        public int? ManPowerInDays { get; set; }
        public long StrengthId { get; set; }
        public int? NoHeader { get; set; }
        public int? FourRld { get; set; }
        public int? NonRld { get; set; }
        public int? Rld { get; set; }

        public virtual PidfPbfRndProjectActivity ProjectActivity { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
