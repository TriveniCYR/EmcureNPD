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
        public List<Deliverables> lsDeliverables { get; set; }
    }
    public class ProjectNameModel
    {
        public string ProjectName { get; set; }
    }
    public class ProjectStrength
    {
        public string Strength { get; set; }
    }
    public class Manager
    {
        public long UserId { get; set; }
        public string ManagerName { get; set; }
        public string DesignationName { get; set; } 
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
        public string Market { get; set; }
        public string Row { get; set; }
        public string colspan { get; set; }
    }
    public class CumulativePhaseWiseBudget
    {
        public string CostHeads { get; set; }
        public string PercentOfTotal { get; set; }
        public string CostRsLacs { get; set; }
    }
    public class Deliverables
    {
        public string PharmacoepialStandardsonQuality { get; set; }
        public string Row { get; set; }
    }
}
