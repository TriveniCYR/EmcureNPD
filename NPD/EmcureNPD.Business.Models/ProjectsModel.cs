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
        public List<PBFDetails> lsPBFDetails { get; set; }
        public FinanceModel financeModel { get; set; }

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
        public bool IsInhouse { get; set; }
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


        public string Note_Remark { get; set; }
        public string PlantName { get; set; }
        public DateTime ProjectInitiationDate { get; set; }
    }
    public class CumulativePhaseWiseBudget
    {
        public double Feasability { get; set; }
        public double Prototype { get; set; }
        public double ScaleUp { get; set; }
        public double AMV { get; set; }
        public double Exhibit { get; set; }
        public double Filing { get; set; }
        public double Total { get; set; }
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
    public class PBFDetails
    {
        public float cost { get; set; }
        public string tentative { get; set; }
        public string pbfworkflowtaskname { get; set; }
        public string workflowname { get; set; }
        public string pbfworkflowname { get; set; }
    }
    public class CommercialSummary
    {
        public string BusinessUnitName { get; set; }
        public string PidfStrengthPackSize { get; set; }
        public Dictionary<int, YearWiseDetails> YearDetails { get; } = new Dictionary<int, YearWiseDetails>();

        public void AddYearDetails(int yearIndex, int countryId, string countryName, string price, string units)
        {
            if (!YearDetails.ContainsKey(yearIndex))
            {
                YearDetails[yearIndex] = new YearWiseDetails
                {
                    CountryName = countryName,
                    Price = int.Parse(price),
                    Units = float.Parse(units),
                    Value = int.Parse(price) * float.Parse(units)
                };
            }
        }

    }
    public class YearWiseDetails
    {
        public string CountryName { get; set; }
        public int Price { get; set; }
        public float Units { get; set; }
        public float Value { get; set; }
    }
}
