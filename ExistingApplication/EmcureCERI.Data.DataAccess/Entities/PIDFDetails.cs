using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class PIDFDetails
    { 
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Strengths { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PatentStatus { get; set; }
        public string Packing { get; set; }
        public string BatchSize { get; set; }
        public string PackSize { get; set; }
        public Nullable<decimal> COGS { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public Nullable<decimal> TotalCIFcost { get; set; }
        public Nullable<decimal> CIFPricePerUnit { get; set; }
        public Nullable<decimal> CIFPricePerPack { get; set; }
        public Nullable<decimal> ProfitPerpack { get; set; }
        public Nullable<decimal> PercentCont { get; set; }
        public Nullable<decimal> QtyOneyear { get; set; }
        public Nullable<decimal> QtyTwoyear { get; set; }
        public Nullable<decimal> QtyThreeyear { get; set; }
        public Nullable<decimal> VolOneyear { get; set; }
        public Nullable<decimal> VolTwoyear { get; set; }
        public Nullable<decimal> VolThreeyear { get; set; }
        public Nullable<decimal> ContriOne { get; set; }
        public Nullable<decimal> ContriTwo { get; set; }
        public Nullable<decimal> ContriThree { get; set; }
        public Nullable<decimal> ContributionThreeYear { get; set; }
        public Nullable<decimal> CostofThreeBatches { get; set; }
        public Nullable<decimal> RandDcost { get; set; }
        public Nullable<decimal> Filingcost { get; set; }
        public Nullable<decimal> Stabilitycost { get; set; }
        public Nullable<decimal> TotalInvest { get; set; }
        public Nullable<decimal> ROI { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsStatus { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<int> CountryId { get; set; }


    }
}
