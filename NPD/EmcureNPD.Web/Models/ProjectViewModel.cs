using System;
using System.Collections.Generic;

namespace EmcureNPD.Web.Models
{
    public class ProjectViewModel
    {
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProjectsView
    {
        public List<Projects> table { get; set; }
        public List<StrengthView> table1 { get; set; }
        public List<ManagerView> table2 { get; set; }
        public List<HeadWiseBudgetView> table3 { get; set; }
        public List<ProjectDetailsView> table4 { get; set; }
        public List<CumulativePhaseWiseBudgetView> table5 { get; set; }
        public List<AdditionalCostView> table6 { get; set; }
        public List<PBFDetailsView> table7 { get; set; }

    }

    public class Projects
    {
        public string projectName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
    }

    public class StrengthView
    {
        public string strength { get; set; }
        public string projectCode { get; set; }
        public bool isInhouse { get; set; }
    }

    public class ManagerView
    {
        public long userId { get; set; }
        public string managerName { get; set; }
        public string designationName { get; set; }
        public int statusId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class HeadWiseBudgetView
    {
        public string BudgetsHeades { get; set; }
        public string Prototype { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string TOTAL { get; set; }
    }

    public class ProjectDetailsView
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
        public string Note_Remark { get; set; }
        public string PlantName { get; set; }
        public DateTime ProjectInitiationDate { get; set; }
    }

    public class CumulativePhaseWiseBudgetView
    {
        public double Feasability { get; set; }
        public double Prototype { get; set; }
        public double ScaleUp { get; set; }
        public double AMV { get; set; }
        public double Exhibit { get; set; }
        public double Filing { get; set; }
        public double Total { get; set; }
    }
    public class AdditionalCostView
    {
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public float ReferenceProductCost { get; set; }
        public float BioStudyCost { get; set; }
        public float CapexMiscCost { get; set; }
        public float FillingCost { get; set; }
        public float Total { get; set; }
    }
    public class PBFDetailsView
    {
        public float cost { get; set; }
        public string tentative { get; set; }
        public string pbfworkflowtaskname { get; set; }
        public string workflowname { get; set; }
        public string pbfworkflowname { get; set; }
    }

}