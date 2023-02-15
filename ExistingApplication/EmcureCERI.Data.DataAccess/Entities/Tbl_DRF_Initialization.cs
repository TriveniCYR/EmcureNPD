using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_Initialization
    {
        [Key]
        public int InitializationID { get; set; }
        public int CompanyID { get; set; }
        public string DRFNo { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public Nullable<bool> TreadmarkApprovedInternal { get; set; }
        public string TreadmarkSuggestedInternal { get; set; }
        public string TreadmarkOwnerInternal { get; set; }
        public int Form { get; set; }
        public int Strength { get; set; }
        public int PackSize { get; set; }
        public int PackStyle { get; set; }
        public int? Plant { get; set; }
        public int ProductTypeId { get; set; }
        public int DossierTemplateID { get; set; }
        public string DossierTemplate { get; set; }

        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public decimal? RegistrationFees { get; set; }        
        public int? FeesToBePaidByID { get; set; }
        public string FeesToBePaidBy { get; set; }
        public int ModeOfFeesPayment { get; set; } 
        public int MAHolderID { get; set; }
        public string MAHolder { get; set; } 
        public int ProposedMarketingStatusID { get; set; }
        public string ProposedMarketingStatus { get; set; }
        public string ShippingPort { get; set; }
        public int ModeOfShipment { get; set; }
        public int Incoterms { get; set; }
        public string DossierSubmittedToMOHBy { get; set; }
        public string OwnerOfRegistration { get; set; }
        public bool AvailabilityofCDA { get; set; }
        public bool DAExcecuted { get; set; }
        public bool TSExcecuted { get; set; }
        public int OrderFrequencyID { get; set; }
        public string EmailDossierSend { get; set; }
        public string PhoneDossierSend { get; set; }
        public decimal MarketSize { get; set; }
        public string ThreeYearCAGR { get; set; }
        public Nullable<int> NumberOfCurrentPlayer { get; set; }
        public string InnovatorBrand { get; set; }
        public string FirstBrand { get; set; }
        public string SecondBrand { get; set; }
        public string ThirdBrand { get; set; }
        public decimal ExpectedMarketValueGrowth { get; set; }
        public string InnavotorName { get; set; }
        public string MSFirstBrand { get; set; }
        public string MSSecondBrand { get; set; }
        public string MSThirdBrand { get; set; }
        public string Partner { get; set; }
        public Nullable<decimal> FirstYearForecastUnitsPacks { get; set; }
        public Nullable<decimal> SecondYearForecastUnitsPacks { get; set; }
        public Nullable<decimal> ThirdYearForecastUnitsPacks { get; set; }
       public Nullable<decimal> FirstYearForecastPriceToPatient { get; set; }
        public Nullable<decimal> SecondYearForecastPriceToPatient { get; set; }
        public Nullable<decimal> ThirdYearForecastPriceToPatient { get; set; } 
        public Nullable<decimal> FirstYearAPIQuantity { get; set; }
        public Nullable<decimal> SecondYearAPIQuantity { get; set; }
        public Nullable<decimal> ThirdYearAPIQuantity { get; set; }
        public Nullable<decimal> FirstYearForecastCIF { get; set; }
        public Nullable<decimal> SecondYearForecastCIF { get; set; }
        public Nullable<decimal> ThirdYearForecastCIF { get; set; }
        public Nullable<decimal> FirstYearForecastValue { get; set; }
         public Nullable<decimal> SecondYearForecastValue { get; set; }
        public Nullable<decimal> ThirdYearForecastValue { get; set; }
        public string OrderFrequency { get; set; }
        public string NameDossierSend { get; set; }
        public string AddressDossierSend { get; set; }
        public Nullable<bool> StrategyAlignment { get; set; }
        public string ExceptionExplained { get; set; }

        public int StatusID { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CountryName { get; set; }
        public string InitialApproveRejectComment { get; set; }

        public string FormName { get; set; }
        public string StrengthName { get; set; }
        public string PackSizeName { get; set; }
        public string PackStyleName { get; set; }
        public string PlantName { get; set; }
        public string ModeofFeesPaymentName { get; set; }
        public string ModeofshipmentName { get; set; }
        public string IncotermsName { get; set; }
        public string ProjectName { get; set; }
        public bool? Overallbusinesscase { get; set; }
        public string Continent { get; set; }

        public Int64 SrNo { get; set; } 
        public string Pharmaceutical_Form { get; set; }
        public string Strength_Name { get; set; } 
        public string Country { get; set; }
       
        // Added by Nitin
        public string IPFilled { get; set; }
        public string ManufacturingFilled { get; set; }
        public string SCMFilled { get; set; }
        public string RAFilled { get; set; }
        public string MedicalFilled { get; set; }
        public string HOD_Approved_Before { get; set; }
        public bool? IsSamples_Required { get; set; }
        public int? Samples_Required { get; set; }
        public string Remark { get; set; }
        public int? NoofShipmnets { get; set; }
        public string UpdateRemark { get; set; }



    }
}
