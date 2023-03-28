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
        public string CapexMiscellaneousExpensesDevelopment { get; set; }
        public string MiscellaneousDevelopment { get; set; }
        public string Licensing { get; set; }
        public string Capex1 { get; set; }
        public string Capex2 { get; set; }
        public string Capex3 { get; set; }
        public double? TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
