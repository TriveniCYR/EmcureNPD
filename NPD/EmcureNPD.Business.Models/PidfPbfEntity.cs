using EmcureNPD.Business.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfEntity
    {
        public long Pidfpbfid { get; set; }
        public long Pidfid { get; set; }
		public int BusinessUnitId { get; set; }
		public string ProjectName { get; set; }
        public string Market { get; set; }
        public string BusinessRelationable { get; set; }
        public int? BerequirementId { get; set; }
        public string NumberOfApprovedAnda { get; set; }
        public int? ProductTypeId { get; set; }
        public int? PlantId { get; set; }
        public int? WorkflowId { get; set; }
        public int? DosageId { get; set; }
        public string PatentStatus { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public int? FormRnDdivisionId { get; set; }
        public DateTime? ProjectInitiationDate { get; set; }
        public string RnDhead { get; set; }
        public string ProjectManager { get; set; }
        public int? PackagingTypeId { get; set; }
		public int? StatusId { get; set; }
		public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public string SaveType { get; set; }
        public List<MasterBusinessUnitEntity> MasterBusinessUnitEntities { get; set; }		
		public PIDFEntity pidfEntity { get; set; }
		public PidfPbfRnDEntity pidfPbfRndEntity { get; set; }		
	}
}
