using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public partial class FinanceModel
	{
		public int PidffinaceId { get; set; }
		public string Pidfid { get; set; }
		public int dycrPidfid { get; set; }
        public string Entity { get; set; }
		public string Product { get; set; }
		public DateTime ForecastDate { get; set; }
		public string Currencyid { get; set; }
		public int? DosageFrom { get; set; }
		public string ManufacturingSiteOrPartner { get; set; }
		public string Skus { get; set; }
		public int? Mspersentage { get; set; }
		public int? TargetPriceScenario { get; set; }
		public DateTime ProjectStartDate { get; set; }
		public string BatchManufacturing { get; set; }
		public string ExpectedFilling { get; set; }
		public string ApprovalPeriodinDays { get; set; }
		public DateTime ApprovalDate { get; set; }
		public DateTime? ProductLaunchDate { get; set; }
		public int? GestationPeriodinYears { get; set; }
		public decimal? MarketShareErosionrate { get; set; }
		public decimal? PriceErosion { get; set; }
		public string EscalationinCOGS { get; set; }
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
		public DateTime NoofbatchestobemanufacturedPhaseEndDate { get; set; }
		public double? NoSkus { get; set; }
		public DateTime NoSkusPhaseEndDate { get; set; }
		public decimal? RandDanalyticalcost { get; set; }
		public DateTime RandDanalyticalcostPhaseEndDate { get; set; }
		public decimal? Rldsamplecost { get; set; }
		public DateTime RldsamplecostPhaseEndDate { get; set; }
		public decimal? BatchmanufacturingcostOrApiactualsEst { get; set; }
		public DateTime BatchmanufacturingcostOrApiactualsEstPhaseEndDate { get; set; }
		public decimal? Sixmonthsstabilitycost { get; set; }
		public DateTime SixmonthsstabilitycostPhaseEndDate { get; set; }
		public decimal? TechTransfer { get; set; }
		public DateTime TechTransferPhaseEndDate { get; set; }
		public decimal? Bestudies { get; set; }
		public DateTime BestudiesPhaseEndDate { get; set; }
		public decimal? Filingfees { get; set; }
		public DateTime FilingfeesPhaseEndDate { get; set; }
		public decimal? BioStuddyCost { get; set; }
		public DateTime BioStuddyCostPhaseEndDate { get; set; }
		public decimal? Capex { get; set; }
		public DateTime CapexPhaseEndDate { get; set; }
		public decimal? ToolingAndChangeParts { get; set; }
		public DateTime ToolingAndChangePartsPhaseEndDate { get; set; }
		public decimal? Total { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? CreatedBy { get; set; }
		public string SaveType { get; set; }
		public int PIDFStatusId { get; set; }
		public string StatusRemark { get; set; }
		public List<ChildPidfFinanceBatchSizeCoating> lsPidfFinanceBatchSizeCoating { get; set; }
		public int BussinessUnitId { get; set; }
        public List<FinaceProjectionYear> lsFinaceProjectionYear { get; set; }
    }
	public partial class ChildPidfFinanceBatchSizeCoating
	{
		public int PidffinaceBatchSizeCoatingId { get; set; }
		public int? PidffinaceId { get; set; }
		public int? BusinessUnitId { get; set; }
		public double? Batchsize { get; set; }
		public double? Yield { get; set; }
		public double? Batchoutput { get; set; }
		public double? ApiCad { get; set; }
		public double? ExcipientsCad { get; set; }
		public double? PmCad { get; set; }
		public double? CcpcCad { get; set; }
		public double? FreightCad { get; set; }
		public double? EmcureCogsPack { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? CreatedBy { get; set; }
		public long? Skus { get; set; }
		public double? PakeSize { get; set; }
		public double? BrandPrice { get; set; }
		public double? NetRealisation { get; set; }
		public double? GenericListprice { get; set; }
		public double? EstMat2016By12units { get; set; }
		public double? EstMat2020By12units { get; set; }
		public double? Cagrover2016By12estMatunits { get; set; }
		public double? Marketinpacks { get; set; }
		public double? BatchsizeinLtrTabs { get; set; }
	}
	public class FinaceProjectionYear
	{
		public string Years { get; set; }
    }
}
