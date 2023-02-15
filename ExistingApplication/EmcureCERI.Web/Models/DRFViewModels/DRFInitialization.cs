using EmcureCERI.Web.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace EmcureCERI.Web.Models.DRFViewModels
{
    public class DRFInitialization
    {
        public int? InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Generic Name")]
        public string GenericName { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Trademark Approved (Internal)")]
        public bool TreadmarkApprovedInternal { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Trademark Suggested (Internal)")]
        public string TreadmarkSuggestedInternal { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Trademark Owner (Internal)")]
        public string TreadmarkOwnerInternal { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Form")]
        public int Form { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strength")]
        public int Strength { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Size/s")]
        public int PackSize { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack Style")]
        public int PackStyle { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Plant")]
        public int? Plant { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Type")]
        public int ProductTypeID { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Dossier Template")]
        public int? DossierTemplateID { get; set; }

      //  [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Registration Fee ($)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
       // [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal? RegistrationFees { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Fees to be Paid by")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int? FeesToBePaidByID { get; set; }
        public string FeesToBePaidBy { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Mode of Fees Payment")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int ModeOfFeesPayment { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        //[ConditionalValidation(nameof(ProductTypeID), 2)]
        //[RequiredIfTrue(nameof(ProductTypeID))]
        [Display(Name = "MA Holder")]
        //public int MAHolder { get; set; }
        
        public string MAHolder { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Business Type")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int ProposedMarketingStatusID { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Order frequency")]
        //public int OrderFrequency { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Destination Port")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string ShippingPort { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Mode of Shipment")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int ModeOfShipment { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Incoterms")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int Incoterms { get; set; }

      //  [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Dossier Submitted to MoH by")]
        public string DossierSubmittedToMOHBy { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Owner of Registration")]
        public string OwnerOfRegistration { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //  [Display(Name = "Availability of CDA")]
        [Display(Name = "CDA Excecuted")]
        public bool AvailabilityofCDA { get; set; }

        [Display(Name = "TS Excecuted")]
        public bool TSExcecuted { get; set; }

        [Display(Name = "DA Excecuted")]
        public bool DAExcecuted { get; set; }


        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Current / Proposed Market Size($)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal MarketSize { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "3 Yr. CAGR")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string ThreeYearCAGR { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Number of Current Players")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "The '{0}' must be numeric")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int NumberOfCurrentPlayer { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Innovator Brand")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string InnovatorBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Top Brand")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string FirstBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "2nd Brand")]
        public string SecondBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "3rd Brand")]
        public string ThirdBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Expected Mkt. Value Growth(%)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal ExpectedMarketValueGrowth { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Innovator Name")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string InnavotorName { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "MS Top Brand")]
        public string MSFirstBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "MS 2nd Brand")]
        public string MSSecondBrand { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "MS 3rd Brand")]
        public string MSThirdBrand { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Partner or Potential Partner")]
        public string Partner { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal FirstYearForecastUnitsPacks { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //// [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal SecondYearForecastUnitsPacks { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal ThirdYearForecastUnitsPacks { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
       // [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FirstYearForecastPriceToPatient { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
       // [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal SecondYearForecastPriceToPatient { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
       // [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ThirdYearForecastPriceToPatient { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal FirstYearForecastCIF { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal SecondYearForecastCIF { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal ThirdYearForecastCIF { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal FirstYearAPIQuantity { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal SecondYearAPIQuantity { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal ThirdYearAPIQuantity { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal FirstYearForecastValue { get; set; }


        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal SecondYearForecastValue { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        ////[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public decimal ThirdYearForecastValue { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Order Frequency")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public int OrderFrequency { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Name to Whom Dossier to be Sent")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string NameDossierSend { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Address to Whom Dossier to be Sent")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string AddressDossierSend { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Strategy Alignment")]
        public bool? StrategyAlignment { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Exception Explained")]
        public string ExceptionExplained { get; set; }

        public int? PIDFID { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Currency")]
        [RequiredIf (ErrorMessage = "The '{0}' field is required.")]
        public int CurrencyID { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //// [EmailAddress(ErrorMessage = "The '{0}' field is not a valid email address.")]
        [RegularExpression(@"^(([^<>()\[\]\.,;:\s@\']+(\.[^<>()\[\]\.,;:\s@\']+)*)|(\'.+\'))@(([^<>()[\]\.,;:\s@\']+\.)+[^<>()[\]\.,;:\s@\']{2,})$", ErrorMessage = "The '{0}' field is not a valid email address")]
        [Display(Name = "Email")]
        [RequiredIf(ErrorMessage = "The '{0}' field is required.")]
        public string EmailDossierSend { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The '{0}' must be numeric")]
        
        public string PhoneDossierSend { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Code")]
        public string ConuntryMobileCode { get; set; }

        [Display(Name = "Required Samples?")]
        public bool IsSamples_Required { get; set; }

        [Display(Name = "Samples")]
        public int? Samples_Required { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        //[DefaultValue(2)]
        [Display(Name = "No. of Shipments")]
        public int? NoofShipmnets { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Update Reason")]
        public string UpdateRemark { get; set; }
    }

    public class DRFInitialApproval
    {
       
        public int? InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Trademark Approved (Internal)")]
        public bool InitialApproval { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }


    }

    public class DRFFormApproval
    {

        public int? InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Trademark Approved (Internal)")]
        public bool DRFApproval { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public string UserRole { get; set; }

    }


    public class MultipleDRF
    {
       public  MultipleDRFInitialApproval MultipleDRFInitialApproval = new MultipleDRFInitialApproval();
       public  MultipleDRFFinalApproval MultipleDRFFinalApproval = new MultipleDRFFinalApproval();
    }
    public class MultipleDRFInitialApproval
    {

        public string InitializationIDList { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public bool InitialApproval { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }


    }


    public class MultipleDRFFinalApproval
    {
        public string InitializationIDList { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public bool ApprovedReject { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        //public int? DossierTemplateID { get; set; }
        //public int? CountryID { get; set; }

        //public string ProjectName { get; set; }
    }
    public class DRFAddDetailsModel
    {
        public DRFManufacturingModel DRFManufacturingModel = new DRFManufacturingModel();

        public DRFSCMModel DRFSCMModel = new DRFSCMModel();

        public DRFRAModel DRFRAModel = new DRFRAModel();

        public DRFIPModel DRFIPModel = new DRFIPModel();

        public DRFMedicalModel DRFMedicalModel = new DRFMedicalModel();

        public DRFFinanceApproval DRFFinanceApproval = new DRFFinanceApproval();
    }

    public class DRFRAModel
    {
        public int? Id { get; set; }

        [Display(Name = "ACC")]
        public bool ACC { get; set; }

        [Display(Name = "ZoneII")]
        public bool ZoneII { get; set; }

        [Display(Name = "Ivb Data")]
        public bool Ivbdata { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Process Validtion Report and Protocol availability")]
        public bool ProtocolAvailability { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COPP availability")]
        public bool COPPAvailability { get; set; }


        // public int GMPAvailabilityId { get; set; }


        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "GMPAvailability")]
        public int GMPAvailabilityId { get; set; }

        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "GMPAvailability")]
        public string GMPAvailability { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Mfg. License")]
        public bool MfgLicense { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Plant Inspection required")]
        public bool PlantInspection { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Validation Batches Undertaken")]
        public int ValidationBatches { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "COA Availability")]
        public bool COAAvailability { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "BE Availabililty ")]
        public bool BEAvailability { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API DMF Status and Requirement")]
        public bool APIDMFstatus { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Plant Approval Required ")]
        public bool PlantApproval { get; set; }

        [Display(Name = "Plant Approval Required? if Yes, How")]
       
        public string PlantApprovalIfYes { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Registration Validity")]
        public string RegistrationValidity { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Time for Dossier Preparation")]
        public string Timefordossierpreparation { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "AMV")]
        public string AMV { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "PDR")]
        public bool PDR { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Samples Availability")]
        public bool SamplesAvailability { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Import Permit")]
        public bool ImportPermit { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Brand Name Approval")]
        public string BrandNameApproval { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        // [Display(Name = "Availability of CDA")]
        [Display(Name = "CDA Excecuted")]
        public bool AvailabilityofCDA { get; set; }

        [Display(Name = "TS Excecuted")]
        public bool TSExcecuted { get; set; }

        [Display(Name = "DA Excecuted")]
        public bool DAExcecuted { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Currency")]
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Product Registration Fee (from Basics) ($)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ProductRegistrationFee { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comparative Dissolution Profile Data")]
        public string ComparativeDissolutionProfileData { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Additional Remarks")]
        public string Remarks { get; set; }
        public int InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Dossier Template")]
        public int? DossierTemplateID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Consultant Cost ($)")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ConsultantCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Legalization Cost ($)")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal LegalizationCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Translation Cost ($)")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal TranslationCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Other Cost ($)")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal OtherCost { get; set; }

        public List<GMPAvailabilityList> GMPAvailabilityList { get; set; }

        public List<CurrencyList> CurrencyList { get; set; }
        public List<DossierTemplateList> DossierTemplateList { get; set; }
    }

    public class DRFManufacturingModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public int InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Manufacturing Site")]
        public int ManufacturingSiteId { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Name of the Manufacturing site")]
        public string ManufacturingSiteName { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API")]
        public int? APIId { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "API Site")]
        public string APISiteName { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Batch Size for Drug Product")]
        public string Batchsize { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "FG Lead Time (days)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The '{0}' must be numeric")]
        public int Leadtime { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Cost/Unit EXW (Commercial) (USD)")]
        // [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal? UnitEXW { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Artwork Type")]
        public int ArtworkTypeId { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Tentative Schedule (DD/MM/YYYY)")]
        public DateTime TentativeSchedule { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Tentative Artwork Lead Time [days]")]
        public int Tentative_Artwork_Lead_Time { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Pack/Shipper")]
        public decimal? PackorShipper { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Gross Weight / Pack")]
        public decimal? GrossWeight { get; set; }

        [Display(Name = "Dimensions(W*H*L)")]
        public string Dimensions { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Width(in mm)")]
        public decimal? MWidth { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Height(in mm)")]
        public decimal? MHeight { get; set; }
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Length(in mm)")]
        public decimal? MLength { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public string ManufacturingAPISiteList { get; set; }

        public List<ManufacturingSiteList> ManufacturingSiteList { get; set; }
        public List<APISiteList> APISiteList { get; set; }
        public List<ArtWorkTypeList> ArtWorkTypeList { get; set; }


    }

    public class ManufacturingSiteList
    {
        public int ManufacturingSiteID { get; set; }
        public string ManufacturingSite { get; set; }

    }
    public class ArtWorkTypeList
    {
        public int ArtworkTypeId { get; set; }
        public string ArtworkTypeName { get; set; }

    }

    public class APISiteList
    {
        public int APIID { get; set; }
        public string APISite { get; set; }

    }


    public class DRFSCMModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public int InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Freight Cost per Pack (Basic form) ($)")]
        // [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FreightCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Tentative Shipment (DD/MM/YYYY)")]
        public DateTime TentativeShipmente { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Tentative Destination (DD/MM/YYYY)")]
        public DateTime TentativeDestination { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }


    }

    public class DRFMedicalModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public int InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "BE/CT/In-Vitro BE Available")]
        public bool BeCtVitroAvailable{ get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Possibility of Bio Waiver")]
        public bool BioWaiver { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Possibility of CT Waiver")]
        public bool CTWaiver { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [Display(Name = "BE Cost ($)")]
        public decimal? BECost { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [Display(Name = "Bio Cost ($)")]
        public decimal? BioCost { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [Display(Name = "CT Cost ($)")]
        public decimal? CTCost { get; set; }
        [Display(Name = "Remark")]
        public string Remark1 { get; set; }
        [Display(Name = "Remark")]
        public string Remark2 { get; set; }
        [Display(Name = "Remark")]
        public string Remark3 { get; set; }

    }




    public class GMPAvailabilityList
    {
        public int GMPAvailabilityID { get; set; }
        public string GMPAvailability { get; set; }

    }


    public class CurrencyList
    {
        public int CurrencyID { get; set; }
        public string Currency { get; set; }

    }

    public class DossierTemplateList
    {
        public int DossierTemplateID { get; set; }
        public string DossierTemplate { get; set; }

    }

    public class DRFIPModel
    {
        public int? IPID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        public int InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Markets")]
        public string Markets { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "No. of Approved ANDAs")]
        public string NumbersOfApprovedANDA { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Patent Status")]
        public string PatentStatus { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Legal Status")]
        public string LegalStatus { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "IPD Comments")]
        public string IPDComments { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "No. of Approved Generics")]
        public string NumbersOfApprovedGeneric { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Type of Filing")]
        public string TypeOfFiling { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        [Display(Name = "Cost of Litigation ($)")]
        public string CostofLitigation { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        public string DRFIPModelDetailsList { get; set;}

        //public List<DRFIPModelDetails> DRFIPModelDetails { get; set; }
    }
    public class GetDRFFilledDetailsByID
    { 
        public int? InitializationID { get; set; }
    }


    public class DRFFinanceApproval
    {

        public int? InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Is Overall Business Case Fine")]
        public bool IsOverallBusinessCaseFine { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal Exworks { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ExworksYearTwo { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ExworksYearThree { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal GCMinimum { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal GCMinimumYearTwo { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal GCMinimumYearThree { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal Expenses { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FilingCost { get; set; }

        //[Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "GC Minimum (%age)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        //[RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal? TotalContribution { get; set; } 

        public decimal? TotalPercentage { get; set; }
        public decimal? NetContribution { get; set; } 
        public decimal? NetPercentage { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal Freight { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FreightYearTwo { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Ex Works (USD)")]
        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FreightYearThree { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]        
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal LitigationCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal FreightCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal RegistrationCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal ConsultantCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal LegalizationCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal TranslationCost { get; set; }
        public decimal OtherCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal? BECost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal BioCost { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        [RegularExpression(@"[-+]?[0-9]*\.[0-9][0-9]", ErrorMessage = "{0} must be a valid decimal with 2 decimal places eg. 0.00!")]
        public decimal CTCost { get; set; }

    }

    public class DRFFinalApproval
    {
        public int? InitializationID { get; set; }

        [Required(ErrorMessage = "The '{0}' field is required.")]
        //[Display(Name = "Is Overall business case fine")]
        public bool ApprovedReject { get; set; }

       // [Required(ErrorMessage = "The '{0}' field is required.")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public int? DossierTemplateID { get; set; }
        public int? CountryID { get; set; }

        public string ProjectName { get; set; }
    }

}
