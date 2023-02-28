using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfClinicalCostEntity
    {
        public int PBFClinicalCostId { get; set; }
        public int PBFClinicalId { get; set; }
        public int StrengthId { get; set; }      
        public decimal? TotalPilotFastingCost { get; set; }
        public decimal? TotalPilotFEDCost { get; set; }
        public decimal? TotalPivotalFastingCost { get; set; }
        public decimal? TotalPivotalFEDCost { get; set; }        
        public decimal? TotalCost { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
