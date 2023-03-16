using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfGeneral
    {
        public PidfPbfGeneral()
        {
            PidfPbfClinicalPilotBioFastings = new HashSet<PidfPbfClinicalPilotBioFasting>();
            PidfPbfClinicalPilotBioFeds = new HashSet<PidfPbfClinicalPilotBioFed>();
            PidfPbfClinicalPivotalBioFastings = new HashSet<PidfPbfClinicalPivotalBioFasting>();
            PidfPbfClinicalPivotalBioFeds = new HashSet<PidfPbfClinicalPivotalBioFed>();
            PidfPbfRnDExicipientRequirements = new HashSet<PidfPbfRnDExicipientRequirement>();
        }

        public long PbfgeneralId { get; set; }
        public long Pbfid { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public string ProjectName { get; set; }
        public string SapCodeProjectCode { get; set; }
        public string ImprintingImbossingCodes { get; set; }
        public string Capex { get; set; }
        public double? TotalExpense { get; set; }
        public string ProjectComplexity { get; set; }
        public int ProductTypeId { get; set; }
        public string TestLicenseAvailability { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public DateTime? ProjectDevelopmentInitialDate { get; set; }
        public int? FormulationId { get; set; }
        public long StrengthId { get; set; }
        public int? AnalyticalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual PidfPbf Pbf { get; set; }
        public virtual Pidf Pidf { get; set; }
        public virtual MasterProductType ProductType { get; set; }
        public virtual PidfproductStrength Strength { get; set; }
        public virtual ICollection<PidfPbfClinicalPilotBioFasting> PidfPbfClinicalPilotBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPilotBioFed> PidfPbfClinicalPilotBioFeds { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFasting> PidfPbfClinicalPivotalBioFastings { get; set; }
        public virtual ICollection<PidfPbfClinicalPivotalBioFed> PidfPbfClinicalPivotalBioFeds { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientRequirement> PidfPbfRnDExicipientRequirements { get; set; }
    }
}
