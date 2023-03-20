using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfRnDEntity
    {
        public long PidfpbfrnDid { get; set; }
        public long? Pidfpbfid { get; set; }
		public long Pidfid { get; set; }
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
        public string SaveSubmitType { get; set; }
		public List<PidfProductStregthEntity> MasterStrengthEntities { get; set; }
		public List<PidfPbfRnDExicipientExhibitEntity> ExicipientExhibitEntities { get; set; }
		public List<PidfPbfRnDExicipientProtoypeEntity> ExicipientProtoypeEntities { get; set; }
		public List<PidfPbfRnDExicipientScaleUpEntity> ExicipientScaleUpEntities { get; set; }	
        public List<PidfPbfRnDExicipientPrototypeEntity> PidfPbfRnDExicipientPrototypeEntity { get; set; }

		public PBFFormEntity objPBFFormEntity { get; set; }
	}
}
