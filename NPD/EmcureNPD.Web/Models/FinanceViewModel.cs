using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;

namespace EmcureNPD.Web.Models
{
    public class FinanceViewModel
    {
        public string Entity { get; set; }
        public string Product { get; set; }
        public DateTime? ForecastDate { get; set; }
        public int Currencyid { get; set; }
        public int DosageFrom { get; set; }
        public string ManufacturingSiteOrPartner { get; set; }
        public string SKUs { get; set; }
        public int MSPersentage { get; set; }
        public int TargetPriceScenario { get; set; }

        // ProjectTimelinesViewModel
        public DateTime ProjectStartDate { get; set; }

        public string BatchManufacturing { get; set; }
        public string ExpectedFilling { get; set; }
        public string ApprovalPeriodinDays { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ProductLaunchDate { get; set; }
        public int GestationPeriodinYears { get; set; }

        //CommercialsViewModel
        public decimal MarketShareErosionrate { get; set; }

        public decimal PriceErosion { get; set; }
        public string EscalationinCOGS { get; set; }
        // FinancialAssumptionsViewModel

        public decimal DiscountRate { get; set; }
        public decimal Incometaxrate { get; set; }
        public int Opexasapercenttosale { get; set; }
        public double? ExternalProfitSharepercent { get; set; }
        public double? CollectioninDays { get; set; }
        public double? InventoryinDays { get; set; }
        public double? CreditorinDays { get; set; }
        public decimal MarketingAllowance { get; set; }
        public decimal RegulatoryMaintenanceCost { get; set; }
        public decimal GrosstoNet { get; set; }

        //DealTermsViewModel
        public double? Noofbatchestobemanufactured { get; set; }

        public DateTime NoofbatchestobemanufacturedPhaseEndDate { get; set; }
        public double? NoSKUs { get; set; }
        public DateTime NoSKUsPhaseEndDate { get; set; }
        public decimal RandDAnalyticalcost { get; set; }
        public DateTime RandDAnalyticalcostPhaseEndDate { get; set; }
        public decimal RLDsamplecost { get; set; }
        public DateTime RLDsamplecostPhaseEndDate { get; set; }
        public decimal BatchmanufacturingcostOrAPIActualsEst { get; set; }
        public DateTime BatchmanufacturingcostOrAPIActualsEstPhaseEndDate { get; set; }
        public decimal Sixmonthsstabilitycost { get; set; }
        public DateTime SixmonthsstabilitycostPhaseEndDate { get; set; }
        public decimal TechTransfer { get; set; }
        public DateTime TechTransferPhaseEndDate { get; set; }
        public decimal BEstudies { get; set; }
        public DateTime BEstudiesPhaseEndDate { get; set; }
        public decimal Filingfees { get; set; }
        public DateTime FilingfeesPhaseEndDate { get; set; }
        public decimal BioStuddyCost { get; set; }
        public DateTime BioStuddyCostPhaseEndDate { get; set; }
        public decimal Capex { get; set; }
        public DateTime CapexPhaseEndDate { get; set; }
        public decimal ToolingAndChangeParts { get; set; }
        public DateTime ToolingAndChangePartsPhaseEndDate { get; set; }
        public decimal Total { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatdBy { get; set; }
        public List<PidfFinanceBatchSizeCoating> BatchSizeCoatingViewModelList { get; set; }
    }

    public class BatchSizeCoatingViewModel
    {
        public double? Batchsize { get; set; }
        public double? Yield { get; set; }
        public double? Batchoutput { get; set; }
        public double? API_CAD { get; set; }
        public double? Excipients_CAD { get; set; }
        public double? PM_CAD { get; set; }
        public double? CCPC_CAD { get; set; }
        public double? Freight_CAD { get; set; }
        public double? EmcureCOGs_pack { get; set; }
    }

    public class Root
    {
        public List<Table> table { get; set; }
    }

    public class RootPWB
    {
        public List<PWB> table { get; set; }
    }

    public class PWB
    { 
        public DateTime? endDate { get; set; }
        public double? cost { get; set; }
        public string? terms { get; set; }
		public bool? include { get; set; }
	}

    public class Table
    {
        public int pidffinaceId { get; set; }
        public int pidfid { get; set; }
        public string entity { get; set; }
        public string product { get; set; }
        public DateTime? forecastDate { get; set; }
        public string Currencyid { get; set; }
        public int dosageFrom { get; set; }
        public string manufacturingSiteOrPartner { get; set; }
        public string skus { get; set; }
        public int mspersentage { get; set; }
        public int targetPriceScenario { get; set; }
        public DateTime ?projectStartDate { get; set; }
        public DateTime ?batchManufacturing { get; set; }
        public DateTime ?expectedFilling { get; set; }
        //---------------------------------------------------------

        public DateTime? projectInitiationDate { get; set; }
        public DateTime? batchManifacturingDate { get; set; }
        public DateTime? fillingDateDate { get; set; }

        //---------------------------

        public string approvalPeriodinDays { get; set; }
        public DateTime ?approvalDate { get; set; }
        public DateTime ?productLaunchDate { get; set; }
        public double gestationPeriodinYears { get; set; }
        public double ?marketShareErosionrate { get; set; }
        public double ?priceErosion { get; set; }
        public string EscalationinCOGS { get; set; }
        public double ?discountRate { get; set; }
        public double ?incometaxrate { get; set; }
        public double? opexasapercenttosale { get; set; }
        public double? externalProfitSharepercent { get; set; }
        public double ?collectioninDays { get; set; }
        public double ?inventoryinDays { get; set; }
        public double ?creditorinDays { get; set; }
        public double ?marketingAllowance { get; set; }
        public double ?regulatoryMaintenanceCost { get; set; }
        public double ?grosstoNet { get; set; }
        public double ?noofbatchestobemanufactured { get; set; }
        public DateTime ?noofbatchestobemanufacturedPhaseEndDate { get; set; }
        public double ?noSkus { get; set; }
        public DateTime ?noSkusPhaseEndDate { get; set; }
        public double ?randDanalyticalcost { get; set; }
        public DateTime ?randDanalyticalcostPhaseEndDate { get; set; }
        public double ?rldsamplecost { get; set; }
        public DateTime ?rldsamplecostPhaseEndDate { get; set; }
        public double ?batchmanufacturingcostOrApiactualsEst { get; set; }
        public DateTime ?batchmanufacturingcostOrApiactualsEstPhaseEndDate { get; set; }
        public double ?sixmonthsstabilitycost { get; set; }
        public DateTime ?sixmonthsstabilitycostPhaseEndDate { get; set; }
        public double ?techTransfer { get; set; }
        public DateTime ?techTransferPhaseEndDate { get; set; }
        public double ?bestudies { get; set; }
        public DateTime ?bestudiesPhaseEndDate { get; set; }
        public double ?filingfees { get; set; }
        public DateTime ?filingfeesPhaseEndDate { get; set; }
        public double ?bioStuddyCost { get; set; }
        public DateTime ?bioStuddyCostPhaseEndDate { get; set; }
        public double ?capex { get; set; }
        public DateTime ?capexPhaseEndDate { get; set; }
        public double ?toolingAndChangeParts { get; set; }
        public DateTime ?toolingAndChangePartsPhaseEndDate { get; set; }
        public object ?total { get; set; }
        public int PIDFStatusId { get; set; }

        public DateTime? createdDate { get; set; }
        public int createdBy { get; set; } 
    }

    public class ChildRoot
    {
        public List<ChildTable> table { get; set; }
    }
    public class ProjectionYearRoot
    {
        public List<ProjectionYaerTable> table { get; set; }
    }
    public class ProjectionYaerTable
    {
        public string years { get; set; }
    }
    public class ChildTable
    {
        public int pidfFinaceBatchSizeCoatingId { get; set; }
        public int pidfFinaceId { get; set; }
        public int BusinessUnitId { get; set; }
        public double batchsize { get; set; }
        public double Yield { get; set; }
        public double batchoutput { get; set; }
        public double apI_CAD { get; set; }
        public double excipients_CAD { get; set; }
        public double pM_CAD { get; set; }
        public double ccpC_CAD { get; set; }
        public double freight_CAD { get; set; }
        public double? emcureCOGs_pack { get; set; }
        public DateTime createdDate { get; set; }
        public int createdBy { get; set; }
        public long? Skus { get; set; }
		//public int PackSizeId { get; set; }
		public int PackSizeValue { get; set; }
		public double? PakeSize { get; set; }
        public string SkusName { get; set; }
        public string PakeSizeName { get; set; }
        public double? BrandPrice { get; set; }
        public double? NetRealisation { get; set; }
        public double? GenericListprice { get; set; }
        public double? EstMat2016By12units { get; set; }
        public double? EstMat2020By12units { get; set; }
        public double? Cagrover2016By12estMatunits { get; set; }
        public double? Marketinpacks { get; set; }
        public double? BatchsizeinLtrTabs { get; set; }
    }
}