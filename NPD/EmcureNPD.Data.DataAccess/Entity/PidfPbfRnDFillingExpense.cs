using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDFillingExpense
    {
        public PidfPbfRnDFillingExpense()
        {
            PidfPbfRnDs = new HashSet<PidfPbfRnD>();
        }

        public int FillingExpensesId { get; set; }
        public int RegionId { get; set; }
        public decimal? Cost { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<PidfPbfRnD> PidfPbfRnDs { get; set; }
    }
}
