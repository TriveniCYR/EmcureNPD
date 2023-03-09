using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfAnalyticalEntity
    {
        public long PBFAnalyticalID { get; set; }
        public long PIDFID { get; set; }       
        public string PIDFNO { get; set; }
        public int BusinessUnitId { get; set; }
        public string ProjectName { get; set; }

        public string SAPProjectProjectCode { get; set; }

        public string ImprintingEmbossingCodes { get; set; }

        public double? TotalExpense { get; set; }

        public string ProjectComplexity { get; set; }

        public int ProductTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]      
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
      
        public int? LogInId { get; set; }
        public List<PidfPbfAnalyticalExhibitEntity> PidfPbfAnalyticalExhibits { get; set; }
        public List<PidfPbfAnalyticalPrototypeEntity> PidfPbfAnalyticalPrototypes { get; set; }
        public List<PidfPbfAnalyticalScaleUpEntity> PidfPbfAnalyticalScaleUps { get; set; }  
        public PidfPbfAnalyticalCostEntity PidfPbfAnalyticalCosts{ get; set; }
        public List<PidfProductStregthEntity> ProductStrength { get; set; }
    }
    
}
