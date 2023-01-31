using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfAnalytical
    {
        public int AnalyticalId { get; set; }
        public int? StrengthId { get; set; }
        public int? PrototypeId { get; set; }
        public int? ScaleUpId { get; set; }
        public int? ExhibitId { get; set; }
        public int? AmvcostId { get; set; }
        public int? TotalAmvcostId { get; set; }
        public int? RemarkId { get; set; }
        public int? TotalCostId { get; set; }
    }
}
