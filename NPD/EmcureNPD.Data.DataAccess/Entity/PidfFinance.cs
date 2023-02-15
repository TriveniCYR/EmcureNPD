﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfFinance
    {
        public int PidffinaceId { get; set; }
        public int? Pidfid { get; set; }
        public string Entity { get; set; }
        public string Product { get; set; }
        public DateTime? ForecastDate { get; set; }
        public string Currency { get; set; }
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
        public decimal? DiscountRate { get; set; }
        public decimal? Incometaxrate { get; set; }
        public int? Opexasapercenttosale { get; set; }
        public int? ExternalProfitSharepercent { get; set; }
        public int? CollectioninDays { get; set; }
        public int? InventoryinDays { get; set; }
        public int? CreditorinDays { get; set; }
        public decimal? MarketingAllowance { get; set; }
        public decimal? RegulatoryMaintenanceCost { get; set; }
        public decimal? GrosstoNet { get; set; }
        public int? Noofbatchestobemanufactured { get; set; }
        public DateTime? NoofbatchestobemanufacturedPhaseEndDate { get; set; }
        public int? NoSkus { get; set; }
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
    }
}
