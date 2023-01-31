using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbf
    {
        public long Pidfpbfid { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public int PidfproductStrengthId { get; set; }
        public string ProjectName { get; set; }
        public string Market { get; set; }
        public string BusinessRelationable { get; set; }
        public int BerequirementId { get; set; }
        public string NumberOfApprovedAnda { get; set; }
        public int ProductTypeId { get; set; }
        public int PlantId { get; set; }
        public int WorkflowId { get; set; }
        public int DosageId { get; set; }
        public string PatentStatus { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public int FormRnDdivisionId { get; set; }
        public DateTime? ProjectInitiationDate { get; set; }
        public string RnDhead { get; set; }
        public string ProjectManager { get; set; }
        public int PackagingTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public virtual MasterBerequirement Berequirement { get; set; }
        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterDosage Dosage { get; set; }
        public virtual MasterFormRnDdivision FormRnDdivision { get; set; }
        public virtual MasterPackagingType PackagingType { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual MasterProductStrength PidfproductStrength { get; set; }
        public virtual MasterPlant Plant { get; set; }
        public virtual MasterProductType ProductType { get; set; }
        public virtual MasterWorkflow Workflow { get; set; }
    }
}
