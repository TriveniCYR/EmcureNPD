using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalyticalAmvcostStrengthMapping
    {
        public long PbfanalyticalCostStrengthId { get; set; }
        public long TotalAmvcostId { get; set; }
        public long StrengthId { get; set; }
        public bool? IsChecked { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfproductStrength Strength { get; set; }
        public virtual PidfPbfAnalyticalAmvcost TotalAmvcost { get; set; }
    }
}
