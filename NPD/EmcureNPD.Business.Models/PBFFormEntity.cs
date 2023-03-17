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
        public string DosageFormulationDetail { get; set; }
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
        public int FormulationGlId { get; set; }
        public int AnalyticalGlId { get; set; }
        public int StrengthId { get; set; }
        public string SaveType { get; set; }
        public string BusinessUnitsByUser { get; set; }
        public List<GeneralStrengthEntity> GeneralStrengthEntities { get; set; }       
    }
    public class GeneralStrengthEntity {
        public long PBFGeneralId { get; set; }
        public int StrengthId { get; set; }
        public string ProjectCode { get; set; }
        public string ImprintingEmbossingCodes { get; set; }       
    }
}
