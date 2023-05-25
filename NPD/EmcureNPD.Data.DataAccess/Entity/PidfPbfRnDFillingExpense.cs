using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDFillingExpense
    {
        public long FillingExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int BusinessUnitId { get; set; }
        public bool? IsChecked { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public double? TotalCost { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
    }
}
