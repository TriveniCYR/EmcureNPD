using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfRnDFillingExpenseEntity
    {
        public int FillingExpensesId { get; set; }
        public int? RegionId { get; set; }
        public decimal? Cost { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
