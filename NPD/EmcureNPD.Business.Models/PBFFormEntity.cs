using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PBFFormEntity
    {
        public long Pidfpbfid { get; set; }
        public long Pidfid { get; set; }
        public string OralName { get; set; }
        public string ProjectName { get; set; }
        public string MarketIds { get; set; }
        public int?[] MarketMappingId { get; set; }//insert into seperate table with business unit id
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string BusinessRelationable { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? BerequirementId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string NumberOfApprovedAnda { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? ProductTypeId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? PlantId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? WorkflowId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? DosageId { get; set; }

        public string PatentStatus { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string SponsorBusinessPartner { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? FillingTypeId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string ScopeObjectives { get; set; }


        // Formulation Detils
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? FormRnDdivisionId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public DateTime? ProjectInitiationDate { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string RnDhead { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string ProjectManager { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? PackagingTypeId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? ManufacturingId { get; set; }

        //RFD sectipn
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string BrandName { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string RFDApplicant { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int RFDCountryId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string RFDIndication { get; set; }

        //--------------------Reference detail------------------------------
           [Display(Name = "RFDInnovators", ResourceType = typeof(Master))]
        public string RFDInnovators { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDInitialRevenuePotential", ResourceType = typeof(Master))]
        public string RFDInitialRevenuePotential { get; set; }

        //[Required(ErrorMessage = "This field required with value within 0-100")]
        [Display(Name = "RFDPriceDiscounting", ResourceType = typeof(Master))]
        public string RFDPriceDiscounting { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDCommercialBatchSize", ResourceType = typeof(Master))]
        public string RFDCommercialBatchSize { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }


        //--------------------Reference detail------------------------------
        //General section 
        public long PBFGeneralId { get; set; }
        public int BusinessUnitId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string Capex { get; set; }
        public double TotalExpense { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? ProjectComplexity { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? GeneralProductTypeId { get; set; }
        public string TestLicenseAvailability { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public DateTime? ProjectDevelopmentInitialDate { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? FormulationGLId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int? AnalyticalGLId { get; set; }
        public int StrengthId { get; set; }
        public string SaveType { get; set; }
        public string BusinessUnitsByUser { get; set; }

        #region Tab Veriables 
        public List<GeneralStrengthEntity> GeneralStrengthEntities { get; set; }
        public List<ClinicalEntity> ClinicalEntities { get; set; }
        public string AnalyticalRawData { get; set; }
        public List<AnalyticalEntity> AnalyticalEntities { get; set; }
        public AnalyticalAmvcost AnalyticalAMVCosts { get; set; }
        public string AnalyticalStrengthMappingRawData { get; set; }
        public List<AnalyticalAmvcostStrengthMappingEntity> AnalyticalStrengthMappingEntities { get; set; }

        public string RNDExicipientRawData { get; set; }
        public string RNDPackagingRawData { get; set; }
        public RNDMasterEntity RNDMasterEntities { get; set; }
        public List<RNDBatchSize> RNDBatchSizes { get; set; }
        public List<RNDApirequirement> RNDApirequirements { get; set; }
        public List<RNDExicipient> RNDExicipients { get; set; }
        public List<RNDPackaging> RNDPackagings { get; set; }
        public List<RNDToolingChangepart> RNDToolingChangeparts { get; set; }
        public string RNDToolingChangePartRawData { get; set; }
        public List<RNDCapexMiscellaneousExpense> RNDCapexMiscellaneousExpenses { get; set; }
        public string RNDCapexMiscellaneousExpensesRawData { get; set; }
        public List<RNDPlantSupportCost> RNDPlantSupportCosts { get; set; }
        //public string RNDPlantSupportCostRawData { get; set; }
        public List<RNDReferenceProductDetail> RNDReferenceProductDetails { get; set; }
        public string RNDFillingExpensesRawData { get; set; }
        public List<RNDFillingExpense> RNDFillingExpenses { get; set; }
        public string RNDManPowerCostProjectDurationRawData { get; set; }
        public List<RNDManPowerCost> RNDManPowerCosts { get; set; }
        public string RNDHeadWiseBudgetRawData { get; set; }
        public List<RNDHeadWiseBudget> RNDHeadWiseBudgets { get; set; }
        public string RNDPhaseWiseBudgetRawData { get; set; }
        public List<RNDPhaseWiseBudget> RNDPhaseWiseBudgets { get; set; }

        #endregion
    }
    #region Clinical Classes 
    public class GeneralStrengthEntity
    {
        public long PBFGeneralId { get; set; }
        public int StrengthId { get; set; }
        public string ProjectCode { get; set; }
        public string ImprintingEmbossingCode { get; set; }
    }
    public class ClinicalEntity
    {
        public long PBFClinicalId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int BioStudyTypeId { get; set; }
        public double? FastingOrFed { get; set; }
        public int? NumberofVolunteers { get; set; }
        public double? ClinicalCostAndVolume { get; set; }
        public double? BioAnalyticalCostAndVolume { get; set; }
        public double? DocCostandStudy { get; set; }
    }
    #endregion
    #region Analytical Classes 
    public class AnalyticalEntity
    {
        public long PBFAnalyticalId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int TestTypeId { get; set; }
        public int ActivityTypeId { get; set; }
        public int? Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public int? CostPerTest { get; set; }
        public int PrototypeCost { get; set; }

    }
    public class AnalyticalAmvcost
    {
        public long TotalAmvcostId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? TotalAmvcost { get; set; }
        public string TotalAmvtitle { get; set; }
        public string Remark { get; set; }

    }
    public class AnalyticalAmvcostStrengthMappingEntity
    {
        public long PbfanalyticalCostStrengthId { get; set; }
        public long TotalAmvcostId { get; set; }
        public long StrengthId { get; set; }
        public bool? IsChecked { get; set; }
    }
    #endregion
    #region RND Classes 
    public class RNDMasterEntity
    {
        public long RnDmasterId { get; set; }
        public long PbfgeneralId { get; set; }
        public long BatchSizeId { get; set; }
        public double? ApirequirementMarketPrice { get; set; }
        public double? PlanSupportCostRsPerDay { get; set; }
        public double? ManHourRate { get; set; }
    }
    public class RNDBatchSize
    {
        public long BatchSizeId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? PrototypeFormulation { get; set; }
        public double? ScaleUpbatch { get; set; }
        public double? ExhibitBatch1 { get; set; }
        public double? ExhibitBatch2 { get; set; }
        public double? ExhibitBatch3 { get; set; }
    }
    public class RNDExicipient
    {
        public long PIDFPBFRNDExicipientId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ExicipientPrototype { get; set; }
        public double? ExicipientDevelopment { get; set; }
        public double? RsPerKg { get; set; }
        public double? MgPerUnitDosage { get; set; }
    }
    public class RNDPackaging
    {
        public long PIDFPBFRNDPackagingId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public int PackingTypeId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double? PackagingDevelopment { get; set; }
        public double? RsPerUnit { get; set; }
        public int? Quantity { get; set; }
    }

    public class RNDApirequirement
    {
        public long ApirequirementId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? Prototype { get; set; }
        public double? ScaleUp { get; set; }
        public double? ExhibitBatch1 { get; set; }
        public double? ExhibitBatch2 { get; set; }
        public double? ExhibitBatch3 { get; set; }
        public double? PrototypeCost { get; set; }
        public double? ScaleUpCost { get; set; }
        public double? ExhibitBatchCost { get; set; }
        public double? TotalCost { get; set; }

    }
    public class RNDToolingChangepart
    {
        public long ToolingChangepartId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public double? Cost { get; set; }
        public string Prototype { get; set; }
        public double? StrengthUnitQuantity { get; set; }


    }
    public class RNDCapexMiscellaneousExpense
    {
        public long CapexMiscellaneousExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public double? StrengthMiscellaneousExpense { get; set; }
        public string MiscellaneousDevelopment { get; set; }
    }
    public class RNDPlantSupportCost
    {
        public long PlantSupportCostId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public double? ScaleUp { get; set; }
        public double? Exhibit { get; set; }


    }
    public class RNDReferenceProductDetail
    {
        public long ReferenceProductDetailId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? UnitCostOfReferenceProduct { get; set; }
        public double? FormulationDevelopment { get; set; }
        public double? PilotBe { get; set; }
        public double? PharmasuiticalEquivalence { get; set; }
        public double? PivotalBio { get; set; }
        public double? TotalCost { get; set; }
    }

    public class RNDFillingExpense
    {
        public long FillingExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int BusinessUnitId { get; set; }
        public bool? IsChecked { get; set; }
        public double TotalCost { get; set; }


    }

    public class RNDManPowerCost
    {
        public int ManPowerCostId { get; set; }
        public int ProjectActivitiesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public double? DurationInDays { get; set; }
        public double? ManPowerInDays { get; set; }
    }

    public class RNDHeadWiseBudget
    {
        public int HeadWiseBudgetId { get; set; }
        public int ProjectActivitiesId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? Prototype { get; set; }
        public double? ScaleUp { get; set; }
        public double? Exhibit { get; set; }
    }
    public class RNDPhaseWiseBudget
    {
        public int PhaseWiseBudgetId { get; set; }
        public long PbfgeneralId { get; set; }
        public double? FeasabilityCumTotal { get; set; }
        public double? PrototypeCumTotal { get; set; }
        public double? ScaleUpCumTotal { get; set; }
        public double? AmvcumTotal { get; set; }
        public double? ExhibitCumTotal { get; set; }
        public double? FilingCumTotal { get; set; }
    }
    #endregion
}
