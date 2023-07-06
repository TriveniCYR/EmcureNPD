using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRnDMaster
    {
        public long RnDmasterId { get; set; }
        public long PbfgeneralId { get; set; }
        public long BatchSizeId { get; set; }
        public double? ApirequirementMarketPrice { get; set; }
        public double? PlanSupportCostRsPerDay { get; set; }
        public double? ManHourRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? PlantId { get; set; }
        public int? LineId { get; set; }
        public string ApirequirementVendorName { get; set; }

        public virtual PidfPbfGeneral Pbfgeneral { get; set; }
        public virtual MasterPlant Plant { get; set; }
    }
}
