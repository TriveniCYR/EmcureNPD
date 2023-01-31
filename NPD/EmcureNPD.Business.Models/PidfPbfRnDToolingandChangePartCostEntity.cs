using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfRnDToolingandChangePartCostEntity
	{
        public int ToolingandChangePartCostId { get; set; }
        public string PrototypeDevelopment { get; set; }
        public int? TotalCost { get; set; }
        public decimal? Cost { get; set; }
        public string Prototype { get; set; }
        public string ScaleUpandExhibitBatch { get; set; }
        public string TotalScaleupandExhibitBatch { get; set; }
        public decimal? TotalCost1 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
