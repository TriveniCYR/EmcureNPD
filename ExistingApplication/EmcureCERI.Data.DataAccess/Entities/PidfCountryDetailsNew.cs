using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class PidfCountryDetailsNew
    {
        public Int64 PidfDetailID { get; set; }
        public Int64 PidfID { get; set; }
        public string PidfNo { get; set; }
        public int? ContinentID { get; set; }
        public string ContinentName { get; set; }        
        public int? CountryID { get; set; }        
        public string CountryName { get; set; }        
        public Int64 StrengthID { get; set; }        
        public string PidfStrength { get; set; }
        public int? PatentStatusID { get; set; }
        public string PatentStatus { get; set; }        
        public int? PackSizeID { get; set; }        
        public string PackSizeName { get; set; }        
        public int? PackingID { get; set; }        
        public string PackingName { get; set; }       
        public decimal? CIFPricePerPack { get; set; }
        public decimal? CIFPricePerPack1 { get; set; }
        public decimal? CIFPricePerPack2 { get; set; }
        public decimal? CIFPricePerPack3 { get; set; }
        public decimal? QtyOneyear { get; set; }
        public decimal? QtyTwoyear { get; set; }
        public decimal? QtyThreeyear { get; set; }
        public decimal? VolOneyear { get; set; }
        public decimal? VolTwoyear { get; set; }
        public decimal? VolThreeyear { get; set; }
        public decimal? ContriOne { get; set; }
        public decimal? ContriTwo { get; set; }
        public decimal? ContriThree { get; set; }
        public decimal? COGS1 { get; set; }
        public decimal? COGS2 { get; set; }
        public decimal? COGS3 { get; set; }
        public string BatchSize { get; set; }
        public string PackSize { get; set; }
        public int? CurrencyID { get; set; }
        public string CurrencyName { get; set; }
        public decimal? COGS { get; set; }
        public decimal? Freight { get; set; }
        public decimal? FreightCost { get; set; }
        public decimal? TotalCIFCost { get; set; }
        public decimal? CIFPricePerUnit { get; set; }
        public decimal? ProfitPerPack { get; set; }
        public decimal? PercentCont { get; set; }
        public decimal? ContributionThreeYear { get; set; }
        public decimal? CostofThreeBatches { get; set; }
        public decimal? RandDCost { get; set; }
        public decimal? FilingCost { get; set; }
        public decimal? StabilityCost { get; set; }
        public decimal? TotalInvest { get; set; }
        public string RejectionReason { get; set; }
        public decimal? AnalyticalCost { get; set; }
        public decimal? BECost { get; set; }
        public decimal? RLDCost { get; set; }
        public decimal? OtherCost { get; set; }
        public string APISource { get; set; }
        public decimal? ROI { get; set; }
        public bool IsActive { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
