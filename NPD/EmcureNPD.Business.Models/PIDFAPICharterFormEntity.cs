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
    
    public class PIDFAPICharterFormEntity
    {
        public long PIDFAPICharterFormID { get; set; }        
        public string Pidfid { get; set; }
        public string SaveType { get; set; }
        public string ProjectName { get; set; }
        public string ManHourRates { get; set; }
        public string APIGroupLeader { get; set; }
        public int ProjectComplexityId { get; set; }        
        public int LoggedInUserId { get; set; }
        //public List<PIDF_IPD_PatentDetailsEntity> IPD_PatentDetailsList { get; set; }
        [Required]
        public string IsModelValid { get; set; }
              
    }
    //-------------------Child Table--------------------------  
    public class CapitalOtherExpenditure
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }      
    }
    public class AnalyticalDepartment
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    }
    public class HeadwiseBudget
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    }
    public class ManhourEstimates
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    }
    public class Charter_PRDDepartment
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    }
    public class TimelineInMonths
    {
        public long PIDFAPICharterID { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Remark { get; set; }
    } 
}
