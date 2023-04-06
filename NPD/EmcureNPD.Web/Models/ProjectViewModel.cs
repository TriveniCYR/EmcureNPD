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
        public List<DeliverablesView> table6 { get; set; }
    }

    public class Projects
    {
        public string projectName { get; set; }
    }

    public class StrengthView
    {
        public string strength { get; set; }
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
        public string Market { get; set; }
        public string Row { get; set; }
        public string colspan { get; set; }
    }
    public class CumulativePhaseWiseBudgetView
    {
        public string CostHeads { get; set; }
        public string PercentOfTotal { get; set; }
        public string CostRsLacs { get; set; }
    }
    public class DeliverablesView
    {
        public string PharmacoepialStandardsonQuality { get; set; }
        public string Row { get; set; }
    }
}
