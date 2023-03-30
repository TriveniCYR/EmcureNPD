using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PBFFormEntity
    {
        public long Pidfpbfid { get; set; }
        public long Pidfid { get; set; }
        public string ProjectName { get; set; }
        public string MarketIds { get; set; }
        public int[] MarketMappingId { get; set; }//insert into seperate table with business unit id
        public string BusinessRelationable { get; set; }
        public int BerequirementId { get; set; }
        public string NumberOfApprovedAnda { get; set; }
        public int ProductTypeId { get; set; }
        public int PlantId { get; set; }
        public int WorkflowId { get; set; }
        public int DosageId { get; set; }
        public string PatentStatus { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public int FillingTypeId { get; set; }
        public string ScopeObjectives { get; set; }


        // Formulation Detils
        public int FormRnDdivisionId { get; set; }
        public DateTime? ProjectInitiationDate { get; set; }
        public string RnDhead { get; set; }
        public string ProjectManager { get; set; }
        public int? PackagingTypeId { get; set; }
        public int ManufacturingId { get; set; }

        //RFD sectipn
        public string BrandName { get; set; }
        public string RFDApplicant { get; set; }
        public int RFDCountryId { get; set; }
        public string RFDIndication { get; set; }
        //General section 
        public long PBFGeneralId { get; set; }
        public int BusinessUnitId { get; set; }
        public string Capex { get; set; }
        public double TotalExpense { get; set; }
        public string ProjectComplexity { get; set; }
        public int GeneralProductTypeId { get; set; }
        public string TestLicenseAvailability { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public DateTime? ProjectDevelopmentInitialDate { get; set; }
        public int FormulationGLId { get; set; }
        public int AnalyticalGLId { get; set; }
        public int StrengthId { get; set; }
        public string SaveType { get; set; }
        public string BusinessUnitsByUser { get; set; }

        #region Tab Veriables 
        public List<GeneralStrengthEntity> GeneralStrengthEntities { get; set; }
        public List<ClinicalEntity> ClinicalEntities { get; set; }
        public string AnalyticalRawData { get; set; }
        public List<AnalyticalEntity> AnalyticalEntities { get; set; }
        public AMVCost AMVCosts { get; set; }
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
        public string RNDPlantSupportCostRawData { get; set; }
        public List<RNDReferenceProductDetail> RNDReferenceProductDetails { get; set; }
        public string RNDFillingExpensesRawData { get; set; }
        public List<RNDFillingExpense> RNDFillingExpenses { get; set; }
        public string RNDManPowerCostProjectDurationRawData { get; set; }
        public List<RNDManPowerCost> RNDManPowerCosts { get; set; }


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
        public string Numberoftests { get; set; }
        public string PrototypeDevelopment { get; set; }
        public int? CostPerTest { get; set; }
        public int? PrototypeCost { get; set; }

    }
    public class AMVCost
    {
        public long PBFGeneralId { get; set; }
        public int? TotalAmvcost { get; set; }
        public string Remark { get; set; }
        public int[] StrengthId { get; set; }
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
        public string PrototypeFormulation { get; set; }
        public string ScaleUpbatch { get; set; }
        public string ExhibitBatch1 { get; set; }
        public string ExhibitBatch2 { get; set; }
        public string ExhibitBatch3 { get; set; }
    }
    public class RNDExicipient
    {
        public long PIDFPBFRNDExicipientId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ExicipientPrototype { get; set; }
        public string ExicipientDevelopment { get; set; }
        public double? RsPerKg { get; set; }
        public string MgPerUnitDosage { get; set; }
    }
    public class RNDPackaging
    {
        public long PIDFPBFRNDPackagingId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public int PackagingTypeId { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string PackagingDevelopment { get; set; }
        public double? RsPerUnit { get; set; }
        public int? Quantity { get; set; }
    }

    public class RNDApirequirement
    {
        public long ApirequirementId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string Prototype { get; set; }
        public string ScaleUp { get; set; }
        public string ExhibitBatch1 { get; set; }
        public string ExhibitBatch2 { get; set; }
        public string ExhibitBatch3 { get; set; }
        public double PrototypeCost { get; set; }
        public double ScaleUpCost { get; set; }
        public double ExhibitBatchCost { get; set; }
        public double TotalCost { get; set; }

    }
    public class RNDToolingChangepart
    {
        public long ToolingChangepartId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string ToolingChangepartDevelopment { get; set; }
        public string PrototypeDevelopment { get; set; }
        public double? TotalCost { get; set; }
        public double? Cost { get; set; }
        public string Prototype { get; set; }
        public string ScaleUpExhibitBatch { get; set; }
        public double? TotalScaleUpExhibitBatch { get; set; }
        public double? FinalCost { get; set; }

    }
    public class RNDCapexMiscellaneousExpense
    {
        public long CapexMiscellaneousExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string CapexMiscellaneousExpensesDevelopment { get; set; }
        public string MiscellaneousDevelopment { get; set; }
        public string Licensing { get; set; }
        public string Capex1 { get; set; }
        public string Capex2 { get; set; }
        public string Capex3 { get; set; }
        public double? TotalCost { get; set; }
    }
    public class RNDPlantSupportCost
    {
        public long PlantSupportCostId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string PlantSupportDevelopment { get; set; }
        public string ScaleUp { get; set; }
        public double? ExhibitBatch { get; set; }
        public double? TotalCost { get; set; }
    }
    public class RNDReferenceProductDetail
    {
        public long ReferenceProductDetailId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public string UnitCostOfReferenceProduct { get; set; }
        public string FormulationDevelopment { get; set; }
        public string PilotBe { get; set; }
        public string PharmasuiticalEquivalence { get; set; }
        public string PivotalBio { get; set; }
        public double? TotalCost { get; set; }
    }

    public class RNDFillingExpense
    {
        public long FillingExpensesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int BusinessUnitId { get; set; }
        public bool? IsChecked { get; set; }

    }

    public class RNDManPowerCost
    {
        public int ManPowerCostId { get; set; }
        public int ProjectActivitiesId { get; set; }
        public long PbfgeneralId { get; set; }
        public long StrengthId { get; set; }
        public int? DurationInDays { get; set; }
        public int? ManPowerInDays { get; set; }
    }
    #endregion
}
