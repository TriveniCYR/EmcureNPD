﻿using System;
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
        public double? CapexMiscellaneousExpensesDevelopment { get; set; }
        public double? MiscellaneousDevelopment { get; set; }
        public double? Licensing { get; set; }
        public double? Capex1 { get; set; }
        public double? Capex2 { get; set; }
        public double? Capex3 { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
