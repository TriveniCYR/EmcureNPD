using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfGeneralEntity
    {
        public long PBFGeneralID { get; set; }
        
        public long PIDFID { get; set; }
        public string PIDFNO { get; set; }
        public int BusinessUnitId { get; set; }
        public string ProjectName { get; set; }

        public string SAPProjectProjectCode { get; set; }

        public string ImprintingEmbossingCodes { get; set; }

        public double? TotalExpense { get; set; }

        public string ProjectComplexity { get; set; }

        public int ProductTypeId { get; set; }
        public int StrengthId { get; set; }
        public DateTime? BudgetTimelineSubmissionDate { get; set; }
        public int FormulationId { get; set; }
        public int AnalyticalId { get; set; }
        public string TestLicenseAvailability { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int StatusId { get; set; }

        public int LastStatusId { get; set; }
        public string SaveType { get; set; }
        public int? LogInId { get; set; }
        public List<PidfPbfClinicalPilotBioFastingEntity> pidfpbfClinicalpilotBioFastingEntity { get; set; }
        public List<PidfPbfClinicalPilotBioFedEntity> pidfpbfClinicalPilotBioFedEntity { get; set; }
        public List<PidfPbfClinicalPivotalBioFastingEntity> pidfpbfClinicalPivotalBioFastingEntity { get; set; }
        public List<PidfPbfClinicalPivotalBioFedEntity> pidfpbfClinicalPivotalBioFedEntity { get; set; }
        public PidfPbfClinicalCostEntity pidfPbfClinicalCost { get; set; }
        public List<PidfProductStregthEntity> ProductStrength { get; set; }
    }
}
