using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfAnalyticalFormEntity
    {
        public long PIDFID { get; set; }
        public string PIDFNO { get; set; }

        public string ProjectName { get; set; }

        public string ProductStrength { get; set; }

        public string SAPProjectProjectCode { get; set; }

        public string ImprintingEmbossingCodes { get; set; }

        public string TotalExpense { get; set; }

        public string ProjectComplexity { get; set; }

        public int? ProductTypeId { get; set; }
        public int StrengthId { get; set; }

        public int FormulationId { get; set; }
        public int AnalyticalId { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int StatusId { get; set; }

        public int LastStatusId { get; set; }
        public string SaveType { get; set; }
        public int? LogInId { get; set; }
    }
}
