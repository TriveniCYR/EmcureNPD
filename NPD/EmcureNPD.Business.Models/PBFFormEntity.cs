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
        
        public int FillingTypeId { get; set; }
        public string ScopeObjectives { get; set; }

        public int FormRnDdivisionId { get; set; }
        public int TransferFormRnDdivisionId { get; set; }
        
        public DateTime? ProjectInitiationDate { get; set; }
        public string PreviousProjectCode { get; set; }
        public string SinkCost { get; set; }
        public string RnDhead { get; set; }
        public string ProjectManager { get; set; }
        public string DosageFormulationDetail { get; set; }
        public int? PackagingTypeId { get; set; }

        //General section 

         public int BusinessUnitId { get; set; }
        //public string ProjectName { get; set; }

        public string SAPProjectProjectCode { get; set; }

        public string ImprintingEmbossingCodes { get; set; }

        public double? TotalExpense { get; set; }

        public string ProjectComplexity { get; set; }

        //public int ProductTypeId { get; set; }
        public int StrengthId { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public int FormulationId { get; set; }
        public int AnalyticalId { get; set; }
        public string TestLicenseAvailability { get; set; }        
        public DateTime? ProjectDevelopmentInitialDate { get; set; }

        //RFD sectipn
        public string BrandName { get; set; }
        public string RFDApplicant { get; set; }
        public int RFDCountryId { get; set; }
        public string RFDIndication { get; set; }
    }
}
