using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_PIDF_HeaderNew
    {
        [Key]
        public Int64 PidfID { get; set; }
        public string PIDFNo { get; set; }
        public DateTime? PidfDate { get; set; }
        
        public Nullable<int> ProjectorProductID { get; set; }
        
        public string ProjectorProductName { get; set; }
        
        public Nullable<int> ProductID { get; set; }
        
        public string ProductName { get; set; }
        
        public Nullable<int> PlantID { get; set; }
        
        public string PlantName { get; set; }
        
        public Nullable<int> StrengthID { get; set; }
        
        public string StrengthName { get; set; }
        
        public Nullable<int> UnitID { get; set; }
        
        public string UnitName { get; set; }
        
        public Nullable<int> FormulationID { get; set; }
        
        public string FormulationName { get; set; }
        public Nullable<int> WorkflowID { get; set; }

        public string WorkflowName { get; set; }

        public Nullable<int> ProductManuID { get; set; }
        
        public string ProductManuName { get; set; }
        public string Strengths { get; set; }
        public Nullable<int> PidfStatusID { get; set; }
        public string PidfStatus { get; set; }
        
        public string BatchSize { get; set; }
        
        public string PackSize { get; set; }
        
        public Nullable<int> PackSizeID { get; set; }
        
        public string PackSizeName { get; set; }
        
        public Nullable<int> PackingID { get; set; }
        
        public string PackingName { get; set; }
        
        public Nullable<int> CurrencyID { get; set; }
        
        public string CurrencyName { get; set; }
        
        public Nullable<decimal> COGS { get; set; }
        
        public Nullable<decimal> Freight { get; set; }
        
        public Nullable<decimal> TotalCIFCost { get; set; }
        
        public Nullable<decimal> CIFPricePerUnit { get; set; }
        
        public Nullable<decimal> CIFPricePerPack { get; set; }
        
        public Nullable<decimal> ProfitPerPack { get; set; }
        
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
        
        public Nullable<decimal> COGS1 { get; set; }
        
        public Nullable<decimal> COGS2 { get; set; }
        
        public Nullable<decimal> COGS3 { get; set; }
        
        public Nullable<decimal> ContributionThreeYear { get; set; }
        
        public Nullable<decimal> CostofThreeBatches { get; set; }
        
        public Nullable<decimal> RandDCost { get; set; }
        
        public Nullable<decimal> FilingCost { get; set; }
        
        public Nullable<decimal> StabilityCost { get; set; }
        
        public Nullable<decimal> TotalInvest { get; set; }
        
        public Nullable<decimal> AnalyticalCost { get; set; }
        
        public Nullable<decimal> BECost { get; set; }
        public Nullable<decimal> RLDCost { get; set; }
        public Nullable<decimal> OtherCost { get; set; }
        public string APISource { get; set; }
        public Nullable<decimal> ROI { get; set; }
        public string RejectionReason { get; set; }
        public Nullable<int> ApprovedById1 { get; set; }
        public DateTime? ApprovedDate1 { get; set; }
        public Nullable<int> ApprovedByID1StatusID { get; set; }
        public string ApprovedByID1Remark { get; set; }
        public Nullable<int> ApprovedById2 { get; set; }
        public DateTime? ApprovedByDate2 { get; set; }
        public Nullable<int> ApprovedByID2StatusID { get; set; }
        public string ApprovedByID2Remark { get; set; }
        public Nullable<int> ApprovedById3 { get; set; }
        public DateTime? ApprovedByDate3 { get; set; }
        public Nullable<int> ApprovedByID3StatusID { get; set; }
        public string ApprovedByID3Remark { get; set; }
        public Nullable<int> ApprovedById4 { get; set; }
        public DateTime? ApprovedByDate4 { get; set; }
        public Nullable<int> ApprovedByID4StatusID { get; set; }
        public string ApprovedByID4Remark { get; set; }
        public Nullable<int> PidfLastStatusID { get; set; }
        public string PidfLastStatus { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
