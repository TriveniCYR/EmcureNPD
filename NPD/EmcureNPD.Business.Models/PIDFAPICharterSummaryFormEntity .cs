using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    
    public class PIDFAPICharterSummaryFormEntity
    {
        public long PIDFAPICharterSummaryFormID { get; set; }        
        public string Pidfid { get; set; }
        public string BusinessUnitId { get; set; }        
        public string SaveType { get; set; }
        public string ProjectName { get; set; }
        public string Market { get; set; }
        public string ProjectInitiationDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string ManHourRates { get; set; }
        public string APIGroupLeader { get; set; }
        public int ProjectComplexityId { get; set; }        
        public int LoggedInUserId { get; set; }
        //public List<PIDF_IPD_PatentDetailsEntity> IPD_PatentDetailsList { get; set; }
        [Required]
        public string IsModelValid { get; set; }
        //-------------------Child Table-------------------------- 
       
        public List<TimelineInMonths> TimelineInMonths { get; set; }
        public List<AnalyticalDepartment> AnalyticalDepartment { get; set; }
        public List<PRDDepartment> PRDDepartment { get; set; }
        public List<CapitalOtherExpenditure> CapitalOtherExpenditure { get; set; }
        public List<ManhourEstimates> ManhourEstimates { get; set; }
        public List<HeadwiseBudget> HeadwiseBudget { get; set; }
    }

    
}
