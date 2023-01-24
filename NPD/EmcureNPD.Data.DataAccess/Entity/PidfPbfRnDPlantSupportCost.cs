using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDPlantSupportCost
    {
        public int PlantSupportCostId { get; set; }
        public int? StrengthApiid { get; set; }
        public string ScaleUp { get; set; }
        public string ExhibitBatch { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
