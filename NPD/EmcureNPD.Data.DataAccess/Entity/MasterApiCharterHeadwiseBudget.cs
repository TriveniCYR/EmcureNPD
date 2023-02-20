using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterApiCharterHeadwiseBudget
    {
        public int MasterApiCharterHeadwiseBudgetId { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
    }
}
