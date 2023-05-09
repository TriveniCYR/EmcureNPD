using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class ProjectsModel
    {
        public List<ProjectNameModel> lsProjectName { get; set; }
         public List<ProjectStrength> lsProjectStrength { get; set; }
        public List<Manager> lsManager { get; set; }
        public List<HeadWiseBudget> lsHeadWiseBudget { get; set; } 
        public List<ProjectDetails> lsProjectDetails { get; set; }
        public List<CumulativePhaseWiseBudget> lsCumulativePhaseWiseBudget { get; set; }
        public List<AdditionalCost> lsAdditionalCost { get; set; }
    }
    public class ProjectNameModel
    {
        public string ProjectName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class ProjectStrength
    {
        public string Strength { get; set; }
        public string ProjectCode { get; set; }
    }
    public class Manager
    {
        public long UserId { get; set; }
        public string ManagerName { get; set; }
        public string DesignationName { get; set; } 
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class HeadWiseBudget
    {
        public string BudgetsHeades { get; set; }
        public string Prototype { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string TOTAL { get; set; }
    }
    public class ProjectDetails
    {
        //public string Market { get; set; }
        //public string Row { get; set; }
        //public string colspan { get; set; }
        public string Market { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public string GroupLeader { get; set; }
        public string ProjectComplexity { get; set; }
        public string TotalProjectDuration { get; set; }
        public string API { get; set; }
        public string APISource { get; set; }
        public string APICommercialQuantity { get; set; }
        public string APIPrice { get; set; }
        public string APIRequirement { get; set; }
        public string Prototype { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string ProjectBudget { get; set; }
        public DateTime ProjectCompletionFilingDate { get; set; }
        public string BEStudies { get; set; }
    }
    public class CumulativePhaseWiseBudget
    {
        public float Feasability { get; set; }
        public float Prototype { get; set; }
        public float ScaleUp { get; set; }
        public float AMV { get; set; }
        public float Exhibit { get; set; }
        public float Filing { get; set; }
        public float Total { get; set; }
    }
    public class AdditionalCost
    {
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public float ReferenceProductCost { get; set; }
        public float BioStudyCost { get; set; }
        public float CapexMiscCost { get; set; }
        public float FillingCost { get; set; }
        public float Total { get; set; }
    }
}
