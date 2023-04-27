using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfFinance
    {
        public PidfFinance()
        {
            PidfFinanceBatchSizeCoatings = new HashSet<PidfFinanceBatchSizeCoating>();
        }

        public int PidffinaceId { get; set; }
        public long Pidfid { get; set; }
        public string Entity { get; set; }
        public string Product { get; set; }
        public DateTime? ForecastDate { get; set; }
        public string Currencyid { get; set; }
        public int? DosageFrom { get; set; }
        public string ManufacturingSiteOrPartner { get; set; }
        public string Skus { get; set; }
        public int? Mspersentage { get; set; }
        public int? TargetPriceScenario { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public string BatchManufacturing { get; set; }
        public string ExpectedFilling { get; set; }
        public string ApprovalPeriodinDays { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ProductLaunchDate { get; set; }
        public int? GestationPeriodinYears { get; set; }
        public decimal? MarketShareErosionrate { get; set; }
        public decimal? PriceErosion { get; set; }
        public string EscalationinCogs { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Incometaxrate { get; set; }
        public double? Opexasapercenttosale { get; set; }
        public double? ExternalProfitSharepercent { get; set; }
        public double? CollectioninDays { get; set; }
        public double? InventoryinDays { get; set; }
        public double? CreditorinDays { get; set; }
        public decimal? MarketingAllowance { get; set; }
        public decimal? RegulatoryMaintenanceCost { get; set; }
        public decimal? GrosstoNet { get; set; }
        public double? Noofbatchestobemanufactured { get; set; }
        public DateTime? NoofbatchestobemanufacturedPhaseEndDate { get; set; }
        public double? NoSkus { get; set; }
        public DateTime? NoSkusPhaseEndDate { get; set; }
        public decimal? RandDanalyticalcost { get; set; }
        public DateTime? RandDanalyticalcostPhaseEndDate { get; set; }
        public decimal? Rldsamplecost { get; set; }
        public DateTime? RldsamplecostPhaseEndDate { get; set; }
        public decimal? BatchmanufacturingcostOrApiactualsEst { get; set; }
        public DateTime? BatchmanufacturingcostOrApiactualsEstPhaseEndDate { get; set; }
        public decimal? Sixmonthsstabilitycost { get; set; }
        public DateTime? SixmonthsstabilitycostPhaseEndDate { get; set; }
        public decimal? TechTransfer { get; set; }
        public DateTime? TechTransferPhaseEndDate { get; set; }
        public decimal? Bestudies { get; set; }
        public DateTime? BestudiesPhaseEndDate { get; set; }
        public decimal? Filingfees { get; set; }
        public DateTime? FilingfeesPhaseEndDate { get; set; }
        public decimal? BioStuddyCost { get; set; }
        public DateTime? BioStuddyCostPhaseEndDate { get; set; }
        public decimal? Capex { get; set; }
        public DateTime? CapexPhaseEndDate { get; set; }
        public decimal? ToolingAndChangeParts { get; set; }
        public DateTime? ToolingAndChangePartsPhaseEndDate { get; set; }
        public decimal? Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<PidfFinanceBatchSizeCoating> PidfFinanceBatchSizeCoatings { get; set; }
    }
}
