using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDCapexMiscellaneousExpense
    {
        public long CapexMiscellaneousExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public double? StrengthMiscellaneousExpense { get; set; }
        public string MiscellaneousDevelopment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
