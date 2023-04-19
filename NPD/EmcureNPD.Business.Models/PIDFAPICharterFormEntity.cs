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
        public string BusinessUnitId { get; set; }        
        public string SaveType { get; set; }
        public int? StatusId { get; set; }
        public string ProjectName { get; set; }
        public string Market { get; set; }
        public string ProjectInitiationDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string ManHourRates { get; set; }
        public string APIGroupLeader { get; set; }
        public int ProjectComplexityId { get; set; }
        public string ProjectComplexity { get; set; }
        public int LoggedInUserId { get; set; }
        //public List<PIDF_IPD_PatentDetailsEntity> IPD_PatentDetailsList { get; set; }
        [Required]
        public string IsModelValid { get; set; }
        //-------------------Child Table-------------------------- 
        public List<ManagmentTeam> ManagmentTeams { get; set; }
        public List<TimelineInMonths> TimelineInMonths { get; set; }
        public List<AnalyticalDepartment> AnalyticalDepartment { get; set; }
        public List<PRDDepartment> PRDDepartment { get; set; }
        public List<CapitalOtherExpenditure> CapitalOtherExpenditure { get; set; }
        public List<ManhourEstimates> ManhourEstimates { get; set; }
        public List<HeadwiseBudget> HeadwiseBudget { get; set; }
    }

    #region Child Table Class

    public class ManagmentTeam
    {
        public string FullName { get; set; }
        public string DesignationName { get; set; }
    }
    public class TimelineInMonths
    {
        public long PidfApiCharterId { get; set; }
        public int? TimelineInMonthsId { get; set; }
        public string Name { get; set; }
        public string TimelineInMonthsValue { get; set; }
    }
    public class AnalyticalDepartment
    {
        public long PIDFAPICharterID { get; set; }
        public int? AnalyticalDepartmentId { get; set; }
        public string Name { get; set; }
        public string AnalyticalDepartmentARDValue { get; set; }
        public string AnalyticalDepartmentImpurityValue { get; set; }
        public string AnalyticalDepartmentStabilityValue { get; set; }
        public string AnalyticalDepartmentAMVValue { get; set; }
        public string AnalyticalDepartmentAMTValue { get; set; }
    }
    public class PRDDepartment
    {
        public long PIDFAPICharterID { get; set; }
        public int? PRDDepartmentId { get; set; }
        public string Name { get; set; }
        public string PRDDepartmentRawMaterialValue { get; set; }
    }
    public class CapitalOtherExpenditure
    {
        public long PIDFAPICharterID { get; set; }
        public int? CapitalOtherExpenditureId { get; set; }        
        public string Name { get; set; }
        public string CapitalOtherExpenditureAmountValue { get; set; }
        [StringLength(50)]
        public string CapitalOtherExpenditureRemarkValue { get; set; }      
    }
    public class ManhourEstimates
    {
        public long PIDFAPICharterID { get; set; }
        public int? ManhourEstimatesId { get; set; }
        public string Name { get; set; }
        public string ManhourEstimatesNoOfEmployeeValue { get; set; }
        public string ManhourEstimatesMonthsValue { get; set; }
        public string ManhourEstimatesHoursValue { get; set; }
        public string ManhourEstimatesCostValue { get; set; }
    }
    public class HeadwiseBudget
    {
        public long PIDFAPICharterID { get; set; }
        public int? HeadwiseBudgetId { get; set; }
        public string Name { get; set; }
        public string HeadwiseBudgetValue { get; set; }
    }
   
   
    
    public class CharterObject
    {
        public long PIDF_API_CharterId { get; set; }
        public int ProjectComplexityId { get; set; }
        public string ProjectComplexity { get; set; }
        public string APIGroupLeader { get; set; }
        public string ManHourRates { get; set; }
        public string ProjectName { get; set; }
        public string Market { get; set; }
        public string ProjectInitiationDate { get; set; }
        public string ProjectEndDate { get; set; }
    }
    #endregion
}
