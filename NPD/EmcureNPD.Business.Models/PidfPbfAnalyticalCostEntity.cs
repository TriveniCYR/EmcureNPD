using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfAnalyticalCostEntity
    {
        public int PBFAnalyticalCostId { get; set; }
        public int PBFAnalyticalId { get; set; }
        public int StrengthId { get; set; }
        public decimal? TotalAWVCost { get; set; }
        public string Remark { get; set; }
        public decimal? TotalPrototypeCost { get; set; }
        public decimal? TotalExhibitCost { get; set; }
        public decimal? TotalScaleUpCost { get; set; }
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
