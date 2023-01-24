using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDCapexandMiscellaneousExpenseEntity
    {
        public int CapexandMiscellaneousExpensesId { get; set; }
        public string Miscellaneous { get; set; }
        public string Licensing { get; set; }
        public string Capex1 { get; set; }
        public string Capex2 { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
