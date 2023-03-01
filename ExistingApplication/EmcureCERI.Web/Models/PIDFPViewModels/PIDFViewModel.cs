using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models.PIDFPViewModels
{
    public class PIDFViewModel
    {

        public int Id { get; set; }
        public string ProductId { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")] 
        [Display(Name = "Product Name ")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strengths")]
        public string Strengths { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country ")]
        public string Country { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Patent Status")]
        public string PatentStatus { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Packing")]
        public string Packing { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Batch Size")]
        public string BatchSize { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COGS")]
        public Nullable<decimal> COGS { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Freight")]
        public Nullable<decimal> Freight { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Total CIF cost")]
        public Nullable<decimal> TotalCIFcost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "CIF Price Per Unit  ")]
        public Nullable<decimal> CIFPricePerUnit { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "CIF Price Per Pack ")]
        public Nullable<decimal> CIFPricePerPack { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Profit Per pack ")]
        public Nullable<decimal> ProfitPerpack { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Percent Cont")]
        public Nullable<decimal> PercentCont { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Quantity")]
        public Nullable<decimal> QtyOneyear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Quantity")]
        public Nullable<decimal> QtyTwoyear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Quantity")]
        public Nullable<decimal> QtyThreeyear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Volume")]
        public Nullable<decimal> VolOneyear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Volume")]
        public Nullable<decimal> VolTwoyear { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Volume ")]
        public Nullable<decimal> VolThreeyear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Contri")]
        public Nullable<decimal> ContriOne { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Contri")]
        public Nullable<decimal> ContriTwo { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Contri")]
        public Nullable<decimal> ContriThree { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Contribution 3 Year ")]
        public Nullable<decimal> ContributionThreeYear { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Cost of 3 Batches ")]
        public Nullable<decimal> CostofThreeBatches { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "R and D Cost ")]
        public Nullable<decimal> R_Dcost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Filing Cost ")]
        public Nullable<decimal> Filingcost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Stability Cost")]
        public Nullable<decimal> Stabilitycost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Total Invest ")]
        public Nullable<decimal> TotalInvest { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "ROI")]
        public Nullable<decimal> ROI { get; set; }
        
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsStatus { get; set; }

       
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Rejection Reason")]
        public string RejectionReason { get; set; }
        
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COGS")]
        public Nullable<decimal> COGS1 { get; set; }
        
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COGS ")]
        public Nullable<decimal> COGS2 { get; set; }
       
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COGS ")]
        public Nullable<decimal> COGS3 { get; set; }
       
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Analytical Cost ")]
        public Nullable<decimal> AnalyticalCost { get; set; }


        
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "BE Cost ")]
        public Nullable<decimal> BECost { get; set; }
        
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "RLD cost ")]
        public Nullable<decimal> RLDcost { get; set; }

        public string RegionId { get; set; }
        public int CountryId { get; set; }
        public int PatentStatusId { get; set; }
        public int PackingId { get; set; }


    }
}
