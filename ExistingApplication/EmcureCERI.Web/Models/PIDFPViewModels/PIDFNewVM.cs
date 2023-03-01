using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models.DRFViewModels;

namespace EmcureCERI.Web.Models.PIDFPViewModels
{

  
    public class PIDFNewVM
    {       

        public PidfHeaderNew clsPidfHeaderNew = new PidfHeaderNew();

        public clsPidfCountryDetailsNew clsPidfCountryDetailsNew = new clsPidfCountryDetailsNew();
        public clsPidfCountryDetailsNew LATAMRegionDetails = new clsPidfCountryDetailsNew();
        public clsPidfCountryDetailsNew ASIARegionDetails = new clsPidfCountryDetailsNew();
        public clsPidfCountryDetailsNew AFRICARegionDetails = new clsPidfCountryDetailsNew();
        public clsPidfCountryDetailsNew MENARegionDetails = new clsPidfCountryDetailsNew();

        public DRFTaskAddModel DRFTaskAddModel { get; set; }

        public DRFTaskSubTaskEditModel DRFTaskSubTaskEditModel { get; set; }

        public DRFSubTaskAddModel DRFSubTaskAddModel { get; set; }
    }
    public class clsProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    public class clsPlant
    {
        public int PlantID { get; set; }
        public string PlantName { get; set; }
    }
    public class clsFormulation
    {
        public int FormulationID { get; set; }
        public string FormulationName { get; set; }
    }
    public class clsUnits
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
    }
    public class clsStrength
    {
        public int StrengthID { get; set; }
        public string StrengthName { get; set; }
    }
    public class clsProductManufacturer
    {
        public int PManufacturerID { get; set; }
        public string PManufacturerName { get; set; }
    }

    public class clsCurrency
    {
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
    }
    public class clsPacking
    {
        public int PackingID { get; set; }
        public string PackingName { get; set; }
    }
    public class clsPackSize
    {
        public int PackSizeID { get; set; }
        public string PackSizeName { get; set; }
    }
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
    public class clsPidfWorkflow
    {
        public int WorkflowID { get; set; }
        public string WorkflowName { get; set; }
    }


    public class clsPidfCountryDetailsNew
    {
        public Int64? PidfDetailID {get;set;}
        public Int64? PidfID {get;set;}
        public string PidfNo { get; set; }
        public int? ContinentID { get; set; }
        public string ContinentName { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country")]
        public int? CountryID { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strength")]
        public Int64? StrengthID { get; set; }
        public string PidfStrength { get; set; }
        public int? PatentStatusID { get; set; }
        public string PatentStatus { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Size")]
        public int?  PackSizeID { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSizeName { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Packing")]
        public int? PackingID { get; set; }
        [Display(Name = "Packing")]
        public string PackingName { get; set; }
        [Display(Name = "CIF Price Per Pack")]
        public decimal? CIFPricePerPack { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        public decimal? CIFPricePerPack1 { get; set; }
        public decimal? CIFPricePerPack2 { get; set; }
        public decimal? CIFPricePerPack3 { get; set; }
        //[Required(ErrorMessage = "The '{0}' field is required.")]
        public decimal? QtyOneyear { get; set; }
        public decimal? QtyTwoyear { get; set; }
        public decimal? QtyThreeyear { get; set; }
        public decimal? VolOneyear { get;  set; }
        public decimal? VolTwoyear { get; set; }
        public decimal? VolThreeyear { get; set; }
        public decimal? ContriOne { get; set; }
        public decimal? ContriTwo { get; set; }
        public decimal? ContriThree { get; set; }
        public decimal? COGS1 { get; set; }
        public decimal? COGS2 { get; set; }
        public decimal? COGS3 { get; set; }
        [Display(Name = "Batch Size")]
        public string BatchSize { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }
        [Display(Name = "Currency")]
        public int?  CurrencyID { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
        [Display(Name = "COGS")]
        public decimal? COGS { get; set; }
        [Display(Name = "Freight")]
        public decimal? Freight { get; set; }
        public decimal? FreightCost { get; set; }
        [Display(Name = "Total CIF Cost")]
        public decimal? TotalCIFCost { get; set; }
        [Display(Name = "CIF Price/Unit")]
        public decimal? CIFPricePerUnit { get; set; }
        [Display(Name = "Profit Per Pack")]
        public decimal? ProfitPerPack { get; set; }
        [Display(Name = "% Cont")]
        public decimal? PercentCont { get; set; }
        [Display(Name = "Contribution 3 Year")]
        public decimal? ContributionThreeYear { get; set; }
        [Display(Name = "Cost of Three Batches")]
        public decimal? CostofThreeBatches { get; set; }
        [Display(Name = "R & D Cost")]
        public decimal? RandDCost { get; set; }
        [Display(Name = "Filing Cost")]
        public decimal? FilingCost { get; set; }
        [Display(Name = "Stability Cost")]
        public decimal? StabilityCost { get; set; }
        [Display(Name = "Total Invest")]
        public decimal? TotalInvest { get; set; }
        public string RejectionReason { get; set; }
        [Display(Name = "Analytical Cost")]
        public decimal? AnalyticalCost { get; set; }
        [Display(Name = "BE Cost")]
        public decimal? BECost { get; set; }
        [Display(Name = "RLD Cost")]
        public decimal? RLDCost { get; set; }
        [Display(Name = "Other Cost")]
        public decimal? OtherCost { get; set; }
        [Display(Name = "API Source")]
        public string APISource { get; set; }
        [Display(Name = "ROI")]
        public decimal? ROI { get; set; }
        public bool? IsActive { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }
                
        public string CountryDetailsList { get; set; }

        public List<clsCountry> clsCountries { get; set; }
        public List<clsPacking> clsPackings { get; set; }
        public List<clsPackSize> clsPackSizes { get; set; }

        //public IList<UploadedFileModel> uploadedfilesdetails { get; set; }
        public string uploadedfilesdetails { get; set; }
    }


    public class clsPidfCountryDetailsNewUpdateModel
    {
        public Int64? PidfDetailID { get; set; }
        public Int64? PidfID { get; set; }
        public string PidfNo { get; set; }
        public int? ContinentID { get; set; }
        public string ContinentName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country")]
        public int? CountryID { get; set; }
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strength")]
        public Int64? StrengthID { get; set; }
        public string PidfStrength { get; set; }
        public int? PatentStatusID { get; set; }
        public string PatentStatus { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Size")]
        public int? PackSizeID { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSizeName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Packing")]
        public int? PackingID { get; set; }
        [Display(Name = "Packing")]
        public string PackingName { get; set; }
        [Display(Name = "CIF Price Per Pack")]
        public decimal? CIFPricePerPack { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        public decimal? CIFPricePerPack1 { get; set; }
        public decimal? CIFPricePerPack2 { get; set; }
        public decimal? CIFPricePerPack3 { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
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
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Batch Size")]
        public string BatchSize { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }
        [Display(Name = "Currency")]
        public int? CurrencyID { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COGS")]
        public decimal? COGS { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Freight")]
        public decimal? Freight { get; set; }
        public decimal? FreightCost { get; set; }
        [Display(Name = "Total CIF Cost")]
        public decimal? TotalCIFCost { get; set; }
        [Display(Name = "CIF Price/Unit")]
        public decimal? CIFPricePerUnit { get; set; }
        [Display(Name = "Profit Per Pack")]
        public decimal? ProfitPerPack { get; set; }
        [Display(Name = "% Cont")]
        public decimal? PercentCont { get; set; }
        [Display(Name = "Contribution 3 Year")]
        public decimal? ContributionThreeYear { get; set; }
        [Display(Name = "Cost of Three Batches")]
        public decimal? CostofThreeBatches { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "R & D Cost")]
        public decimal? RandDCost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Filing Cost")]
        public decimal? FilingCost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Stability Cost")]
        public decimal? StabilityCost { get; set; }
        [Display(Name = "Total Invest")]
        public decimal? TotalInvest { get; set; }
        public string RejectionReason { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Analytical Cost")]
        public decimal? AnalyticalCost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "BE Cost")]
        public decimal? BECost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "RLD Cost")]
        public decimal? RLDCost { get; set; }
        [Display(Name = "Other Cost")]
        public decimal? OtherCost { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API Source")]
        public string APISource { get; set; }
        [Display(Name = "ROI")]
        public decimal? ROI { get; set; }
        public bool? IsActive { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string CountryDetailsList { get; set; }

        public List<clsCountry> clsCountries { get; set; }
        public List<clsPacking> clsPackings { get; set; }
        public List<clsPackSize> clsPackSizes { get; set; }
                
        public string uploadedfilesdetails { get; set; }
    }


    public class PidfHeaderNew
    {
        public Int64? PidfID {get;set;}
        public string PIDFNo {get;set;}
        public DateTime? PidfDate { get; set; }        
        [Display(Name = "Product Name")]
        public int? ProjectorProductID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Name")]
        public string ProjectorProductName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Type")]
        public int ProductID { get; set; }

        [Display(Name = "Product Type")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Plant Name")]
        public int PlantID { get; set; }

        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        [Display(Name = "Strengths")]
        public int? StrengthID { get; set; }
        [Display(Name = "Strengths")]
        public string StrengthName { get; set; }
        [Display(Name = "Unit Name")]
        public int? UnitID { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Formulation")]
        public int FormulationID { get; set; }
        [Display(Name = "Formulation")]
        public string FormulationName { get; set; }
        [Display(Name = "Plant Name")]
        public int? ProductManuID { get; set; }
        [Display(Name = "Plant Name")]
        public string ProductManuName { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Work Flow")]
        public int? WorkflowID { get; set; }
        [Display(Name = "Work Flow")]
        public string WorkflowName { get; set; }
        public string Strengths { get; set; }
        public int? PidfStatusID { get; set; }
        public string PidfStatus { get; set; }
        [Display(Name = "Batch Size(in pack)")]
        public string BatchSize { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSize { get; set; }
        [Display(Name = "Pack Size")]
        public int? PackSizeID { get; set; }
        [Display(Name = "Pack Size")]
        public string PackSizeName { get; set; }
        [Display(Name = "Packing")]
        public int? PackingID { get; set; }
        [Display(Name = "Packing")]
        public string PackingName { get; set; }
        [Display(Name = "Currency")]
        public int? CurrencyID { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }
        [Display(Name = "COGS")]
        public decimal? COGS { get; set; }
        [Display(Name = "Freight")]
        public decimal? Freight { get; set; }
        [Display(Name = "Total CIF Cost")]
        public decimal? TotalCIFCost { get; set; }
        [Display(Name = "CIF Price Per Unit")]
        public decimal? CIFPricePerUnit { get; set; }
        [Display(Name = "CIF Price Per Pack")]
        public decimal? CIFPricePerPack { get; set; }
        [Display(Name = "Profit Per Pack")]
        public decimal? ProfitPerPack { get; set; }
        [Display(Name = "Percent Cont")]
        public decimal? PercentCont { get; set; }
        [Display(Name = "Quantity(1 Year)")]
        public decimal? QtyOneyear { get; set; }
        [Display(Name = "Quantity(2 Year)")]
        public decimal? QtyTwoyear { get; set; }
        [Display(Name = "Quantity(3 Year)")]
        public decimal? QtyThreeyear { get; set; }
        [Display(Name = "Volume(1 Year)")]
        public decimal? VolOneyear { get; set; }
        [Display(Name = "Volume(2 Year)")]
        public decimal? VolTwoyear { get; set; }
        [Display(Name = "Volume(3 Year)")]
        public decimal? VolThreeyear { get; set; }
        [Display(Name = "Contri(1 Year)")]
        public decimal? ContriOne { get; set; }
        [Display(Name = "Contri(2 Year)")]
        public decimal? ContriTwo { get; set; }
        [Display(Name = "Contri(3 Year)")]
        public decimal? ContriThree { get; set; }
        [Display(Name = "COGS(1 Year)")]
        public decimal? COGS1 { get; set; }
        [Display(Name = "COGS(2 Year)")]
        public decimal? COGS2 { get; set; }
        [Display(Name = "COGS(3 Year)")]
        public decimal? COGS3 { get; set; }
        [Display(Name = "Contribution (3 yrs)")]
        public decimal? ContributionThreeYear { get; set; }
        [Display(Name = "Cost of 3 Batches")]
        public decimal? CostofThreeBatches { get; set; }
        [Display(Name = "R&D Cost")]
        public decimal? RandDCost { get; set; }
        [Display(Name = "Filing Cost")]
        public decimal? FilingCost { get; set; }
        [Display(Name = "Stability Cost")]
        public decimal? StabilityCost { get; set; }
        [Display(Name = "Total Invest")]
        public decimal? TotalInvest { get; set; }
        [Display(Name = "Analytical Cost")]
        public decimal? AnalyticalCost { get; set; }
        [Display(Name = "BE Cost")]
        public decimal? BECost { get; set; }
        [Display(Name = "RLD Cost")]
        public decimal? RLDCost { get; set; }
        [Display(Name = "Other Cost")]
        public decimal? OtherCost { get; set; }
        [Display(Name = "API Source")]
        public string APISource { get; set; }
        [Display(Name = "ROI")]
        public decimal? ROI { get; set; }
        [Display(Name = "Reject Reason")]
        public string RejectionReason { get; set; }
        public int? ApprovedById1 { get; set; }
        public DateTime? ApprovedDate1 { get; set; }
        public int? ApprovedByID1StatusID { get; set; }
        public string ApprovedByID1Remark { get; set; }
        public int? ApprovedById2 { get; set; }
        public DateTime? ApprovedByDate2 { get; set; }
        public int? ApprovedByID2StatusID { get; set; }
        public string ApprovedByID2Remark { get; set; }
        public int? ApprovedById3 { get; set; }
        public DateTime? ApprovedByDate3 { get; set; }
        public int? ApprovedByID3StatusID { get; set; }
        public string ApprovedByID3Remark { get; set; }
        public int? ApprovedById4 { get; set; }
        public DateTime? ApprovedByDate4 { get; set; }
        public int? ApprovedByID4StatusID { get; set; }
        public string ApprovedByID4Remark { get; set; }
        public int? PidfLastStatusID { get; set; }
        public string PidfLastStatus { get; set; }
        public bool? IsActive { get; set; }
        public int? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string PidfStrengthList { get; set; }
        public List<clsProduct> ProductList { get; set; }
        public List<clsPlant> PlantList { get; set; }
        public List<clsFormulation> FormulationList { get; set; }
        public List<clsUnits> UnitList { get; set; }
        public List<clsStrength> StrengthList { get; set; }
        public List<clsCurrency> CurrencyList { get; set; }
        public List<clsPidfWorkflow> WorkflowList { get; set; }
    }

    public class PidfNewStrength
    {
        public int CounterID { get; set; }
        public string Strength { get; set; }
        public string UOMid { get; set; }
        public string UOM { get; set; }
    }

    public class CountryDetailsNew
    {
        public Int64 PidfID { get; set; }
        public int ContinentID { get; set; }
        public int CountryID { get; set; }
        public int StrengthID { get; set; }
        public int PackSizeID { get; set; }
        public int PackingID { get; set; }
        public decimal CIFPricePerPack1 { get; set; }
        public decimal QtyOneyear { get; set; }
        public decimal VolOneyear { get; set; }
        public decimal CIFPricePerPack2 { get; set; }
        public decimal QtyTwoyear { get; set; }
        public decimal VolTwoyear { get; set; }
        public decimal CIFPricePerPack3 { get; set; }
        public decimal QtyThreeyear { get; set; }
        public decimal VolThreeyear { get; set; }
    }

    public class MilestoneModelNew
    {

        public DRFTaskAddModel DRFTaskAddModel { get; set; }

        public DRFTaskSubTaskEditModel DRFTaskSubTaskEditModel { get; set; }

        public DRFSubTaskAddModel DRFSubTaskAddModel { get; set; }
    }
}
