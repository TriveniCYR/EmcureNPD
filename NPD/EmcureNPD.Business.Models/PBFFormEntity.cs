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
        
        public List<GeneralStrengthEntity> GeneralStrengthEntities { get; set; }
        
        public List<ClinicalEntity> ClinicalEntities { get; set; }

        public string AnalyticalRawData { get; set; }
        public List<AnalyticalEntity> AnalyticalEntities { get; set; }
        
        public AMVCost AMVCosts { get; set; }
        
        public RNDEntity RNDEntities { get; set; }
        

    }
    public class GeneralStrengthEntity {
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
    public class AMVCost {
        public long PBFGeneralId { get; set; }
        public int? TotalAmvcost { get; set; }
        public string Remark { get; set; }
        public int[] StrengthId { get; set; }
    }
    public class RNDEntity
    {
        public long PBFRndId { get; set; }
        public long PBFGeneralId { get; set; }
        public List<RNDExcipient> RNDExcipients { get; set; }
        public List<RNDPackaging> RNDPackagings { get; set; }
    }
    public class RNDExcipient {
        public long PIDFPBFRNDExicipientId { get; set; }
        public long PBFGeneralId { get; set; }
        public long StrengthId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ExicipientPrototype { get; set; }
        public string PrototypeDevelopment { get; set; }
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
        public string PrototypeDevelopment { get; set; }
        public double? RsPerUnit { get; set; }
        public int? Quantity { get; set; }
    }
}
