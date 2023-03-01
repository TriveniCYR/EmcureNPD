using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class PIDFDetailsNew
    {
        public Int64 PidfDetailID { get; set; }
        public Int64 PidfID { get; set; }
        public string PidfNo { get; set; }
        public Nullable<DateTime> PidfDate { get; set; }
        public Nullable<int> ProjectorProductID { get; set; }
        public string ProjectorProductName { get; set; }
        public string Strengths { get; set; }
        public Nullable<int> PidfStatusID { get; set; }
        public string PidfStatus { get; set; }
        public Nullable<int> ApprovedById1 { get; set; }
        public Nullable<DateTime> ApprovedByDate1 { get; set; }
        public Nullable<int> ApprovedById2 { get; set; }
        public Nullable<DateTime> ApprovedByDate2 { get; set; }
        public Nullable<int> ApprovedById3 { get; set; }
        public Nullable<DateTime> ApprovedByDate3 { get; set; }
        public Nullable<int> ApprovedById4 { get; set; }
        public Nullable<DateTime> ApprovedByDate4 { get; set; }
        public Nullable<int> ContinentID { get; set; }
        public string ContinentName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CountryName { get; set; }
        public Nullable<int> PatentStatusID { get; set; }
        public string PatentStatus { get; set; }
        public string Packing { get; set; }
        public string BatchSize { get; set; }
        public string PackSize { get; set; }
        public decimal Cogs { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalCIFCost { get; set; }
        public decimal CIFPricePerUnit { get; set; }
        public decimal CIFPricePerPack { get; set; }
        public decimal ProfitPerPack { get; set; }
        public decimal PercentCont { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal VolThreeyear { get; set; }
        public decimal ContriOne { get; set; }
        public decimal ContriTwo { get; set; }
        public decimal ContriThree { get; set; }
        public decimal Cogs1 { get; set; }
        public decimal Cogs2 { get; set; }
        public decimal Cogs3 { get; set; }
        public decimal ContributionThreeYear { get; set; }
        public decimal CostofThreeBatches { get; set; }
        public decimal RandDCost { get; set; }
        public decimal FilingCost { get; set; }
        public decimal StabilityCost { get; set; }
        public decimal TotalInvest { get; set; }
        public decimal Roi { get; set; }
        public string RejectionReason { get; set; }
        public decimal AnalyticalCost { get; set; }
        public decimal BECost { get; set; }
        public decimal RLDCost { get; set; }
        public Nullable<bool> IsActive { get; set; }
        //public Nullable<int> Createdby { get; set; }
        //public Nullable<DateTime> CreatedDate { get; set; }
        //public Nullable<int> Modifiedby { get; set; }
        //public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
