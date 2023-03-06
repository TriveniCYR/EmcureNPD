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
        public double? TotalAMVCost { get; set; }
        public string Remark { get; set; }
        public double? TotalPrototypeCost { get; set; }
        public double? TotalExhibitCost { get; set; }
        public double? TotalScaleUpCost { get; set; }
        public double? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
