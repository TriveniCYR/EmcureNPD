using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
	public partial class FinanceModel
	{
		public int PidffinaceId { get; set; }
		public string Pidfid { get; set; }
		public string Entity { get; set; }
		public string Product { get; set; }
		public DateTime ForecastDate { get; set; }
		public int Currencyid { get; set; }
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
		public int? Opexasapercenttosale { get; set; }
		public int? ExternalProfitSharepercent { get; set; }
		public int? CollectioninDays { get; set; }
		public int? InventoryinDays { get; set; }
		public int? CreditorinDays { get; set; }
		public decimal? MarketingAllowance { get; set; }
		public decimal? RegulatoryMaintenanceCost { get; set; }
		public decimal? GrosstoNet { get; set; }
		public int? Noofbatchestobemanufactured { get; set; }
		public DateTime NoofbatchestobemanufacturedPhaseEndDate { get; set; }
		public int? NoSkus { get; set; }
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
        public List<ChildPidfFinanceBatchSizeCoating> lsPidfFinanceBatchSizeCoating{ get; set; }
	}
	public partial class ChildPidfFinanceBatchSizeCoating
	{
		public int PidffinaceBatchSizeCoatingId { get; set; }
		public int? PidffinaceId { get; set; }
		public int? BusinessUnitId { get; set; }
		public int? Batchsize { get; set; }
		public int? Yield { get; set; }
		public int? Batchoutput { get; set; }
		public int? ApiCad { get; set; }
		public int? ExcipientsCad { get; set; }
		public int? PmCad { get; set; }
		public int? CcpcCad { get; set; }
		public int? FreightCad { get; set; }
		public int? EmcureCogsPack { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? CreatedBy { get; set; }
	}
}
