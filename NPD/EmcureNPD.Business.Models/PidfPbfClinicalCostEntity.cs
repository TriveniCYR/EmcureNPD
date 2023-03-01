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
        public double? TotalPilotFastingCost { get; set; }
        public double? TotalPilotFEDCost { get; set; }
        public double? TotalPivotalFastingCost { get; set; }
        public double? TotalPivotalFEDCost { get; set; }        
        public double? TotalCost { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
