using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnD
    {
        public long PidfpbfrnDid { get; set; }
        public long? Pidfpbfid { get; set; }
        public int? NumberOf { get; set; }
        public int? ExicipientProtoypeId { get; set; }
        public int? ExicipientScaleUpId { get; set; }
        public int? ExicipientExhibitId { get; set; }
        public decimal? TotalExicipientCosts { get; set; }
        public int? PackagingPrototypeId { get; set; }
        public int? PackagingScaleUpId { get; set; }
        public int? PackagingExhibitId { get; set; }
        public decimal? TotalPackagingCosts { get; set; }
        public int? ToolingAndChangePartCostId { get; set; }
        public int? CapexAndMiscellaneousExpensesId { get; set; }
    }
}
