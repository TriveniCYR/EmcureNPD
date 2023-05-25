using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfproductStrength
    {
        public PidfproductStrength()
        {
            PidfCommercials = new HashSet<PidfCommercial>();
            PidfPbfAnalyticalAmvcostStrengthMappings = new HashSet<PidfPbfAnalyticalAmvcostStrengthMapping>();
            PidfPbfAnalyticalCostStrengthMappings = new HashSet<PidfPbfAnalyticalCostStrengthMapping>();
            PidfPbfAnalyticals = new HashSet<PidfPbfAnalytical>();
            PidfPbfClinicals = new HashSet<PidfPbfClinical>();
            PidfPbfGeneralStrengths = new HashSet<PidfPbfGeneralStrength>();
            PidfPbfRnDApirequirements = new HashSet<PidfPbfRnDApirequirement>();
            PidfPbfRnDCapexMiscellaneousExpenses = new HashSet<PidfPbfRnDCapexMiscellaneousExpense>();
            PidfPbfRnDExicipientPrototypes = new HashSet<PidfPbfRnDExicipientPrototype>();
            PidfPbfRnDExicipientRequirements = new HashSet<PidfPbfRnDExicipientRequirement>();
            PidfPbfRnDFillingExpenses = new HashSet<PidfPbfRnDFillingExpense>();
            PidfPbfRnDPackagingMaterials = new HashSet<PidfPbfRnDPackagingMaterial>();
            PidfPbfRnDPlantSupportCosts = new HashSet<PidfPbfRnDPlantSupportCost>();
            PidfPbfRnDReferenceProductDetails = new HashSet<PidfPbfRnDReferenceProductDetail>();
            PidfPbfRnDToolingChangeparts = new HashSet<PidfPbfRnDToolingChangepart>();
            PidfPbfRndBatchSizes = new HashSet<PidfPbfRndBatchSize>();
        }

        public long PidfproductStrengthId { get; set; }
        public long Pidfid { get; set; }
        public string Strength { get; set; }
        public int? UnitofMeasurementId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }

        public virtual Pidf Pidf { get; set; }
        public virtual MasterUnitofMeasurement UnitofMeasurement { get; set; }
        public virtual ICollection<PidfCommercial> PidfCommercials { get; set; }
        public virtual ICollection<PidfPbfAnalyticalAmvcostStrengthMapping> PidfPbfAnalyticalAmvcostStrengthMappings { get; set; }
        public virtual ICollection<PidfPbfAnalyticalCostStrengthMapping> PidfPbfAnalyticalCostStrengthMappings { get; set; }
        public virtual ICollection<PidfPbfAnalytical> PidfPbfAnalyticals { get; set; }
        public virtual ICollection<PidfPbfClinical> PidfPbfClinicals { get; set; }
        public virtual ICollection<PidfPbfGeneralStrength> PidfPbfGeneralStrengths { get; set; }
        public virtual ICollection<PidfPbfRnDApirequirement> PidfPbfRnDApirequirements { get; set; }
        public virtual ICollection<PidfPbfRnDCapexMiscellaneousExpense> PidfPbfRnDCapexMiscellaneousExpenses { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientPrototype> PidfPbfRnDExicipientPrototypes { get; set; }
        public virtual ICollection<PidfPbfRnDExicipientRequirement> PidfPbfRnDExicipientRequirements { get; set; }
        public virtual ICollection<PidfPbfRnDFillingExpense> PidfPbfRnDFillingExpenses { get; set; }
        public virtual ICollection<PidfPbfRnDPackagingMaterial> PidfPbfRnDPackagingMaterials { get; set; }
        public virtual ICollection<PidfPbfRnDPlantSupportCost> PidfPbfRnDPlantSupportCosts { get; set; }
        public virtual ICollection<PidfPbfRnDReferenceProductDetail> PidfPbfRnDReferenceProductDetails { get; set; }
        public virtual ICollection<PidfPbfRnDToolingChangepart> PidfPbfRnDToolingChangeparts { get; set; }
        public virtual ICollection<PidfPbfRndBatchSize> PidfPbfRndBatchSizes { get; set; }
    }
}
