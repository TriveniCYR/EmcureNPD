using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfGeneral
    {
        public PidfPbfGeneral()
        {
            PidfPbfAnalyticalAmvcosts = new HashSet<PidfPbfAnalyticalAmvcost>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfGeneralStrengths = new HashSet<PidfPbfGeneralStrength>();
            PidfPbfRnDApirequirements = new HashSet<PidfPbfRnDApirequirement>();
            PidfPbfRnDCapexMiscellaneousExpenses = new HashSet<PidfPbfRnDCapexMiscellaneousExpense>();
            PidfPbfRnDExicipientPrototypes = new HashSet<PidfPbfRnDExicipientPrototype>();
            PidfPbfRnDExicipientRequirements = new HashSet<PidfPbfRnDExicipientRequirement>();
            PidfPbfRnDExicipientScaleUps = new HashSet<PidfPbfRnDExicipientScaleUp>();
            PidfPbfRnDFillingExpenses = new HashSet<PidfPbfRnDFillingExpense>();
            PidfPbfRnDManPowerCosts = new HashSet<PidfPbfRnDManPowerCost>();
            PidfPbfRnDMasters = new HashSet<PidfPbfRnDMaster>();
            PidfPbfRnDPackagingMaterials = new HashSet<PidfPbfRnDPackagingMaterial>();
            PidfPbfRnDPlantSupportCosts = new HashSet<PidfPbfRnDPlantSupportCost>();
            PidfPbfRnDReferenceProductDetails = new HashSet<PidfPbfRnDReferenceProductDetail>();
            PidfPbfRnDToolingChangeparts = new HashSet<PidfPbfRnDToolingChangepart>();
            PidfPbfRndBatchSizes = new HashSet<PidfPbfRndBatchSize>();
        }

        public long PbfgeneralId { get; set; }
        public long Pidfpbfid { get; set; }
        public int BusinessUnitId { get; set; }
        public string Capex { get; set; }
        public double? TotalExpense { get; set; }
        public string ProjectComplexity { get; set; }
        public int ProductTypeId { get; set; }
        public string TestLicenseAvailability { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public DateTime? ProjectDevelopmentInitialDate { get; set; }
        public int? FormulationGlid { get; set; }
        public int? AnalyticalGlid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual MasterUser AnalyticalGl { get; set; }
        public virtual MasterBusinessUnit BusinessUnit { get; set; }
        public virtual MasterUser FormulationGl { get; set; }
        public virtual PidfPbf Pidfpbf { get; set; }
        public virtual MasterProductType ProductType { get; set; }
        public virtual ICollection<PidfPbfAnalyticalAmvcost> PidfPbfAnalyticalAmvcosts { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual ICollection<PidfPbfRnDApirequirement> PidfPbfRnDApirequirements { get; set; }
        public virtual ICollection<PidfPbfRnDCapexMiscellaneousExpense> PidfPbfRnDCapexMiscellaneousExpenses { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientRequirement> PidfPbfRnDExicipientRequirements { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientScaleUp> PidfPbfRnDExicipientScaleUps { get; set; }
        public virtual ICollection<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual ICollection<PidfPbfRnDManPowerCost> PidfPbfRnDManPowerCosts { get; set; }
        public virtual ICollection<PidfPbfRnDMaster> PidfPbfRnDMasters { get; set; }
        public virtual ICollection<PidfPbfRnDPackagingMaterial> PidfPbfRnDPackagingMaterials { get; set; }
        public virtual ICollection<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual ICollection<PidfPbfRnDReferenceProductDetail> PidfPbfRnDReferenceProductDetails { get; set; }
        public virtual ICollection<PidfPbfRnDToolingChangepart> PidfPbfRnDToolingChangeparts { get; set; }
        public virtual ICollection<PidfPbfRndBatchSize> PidfPbfRndBatchSizes { get; set; }
    }
}
