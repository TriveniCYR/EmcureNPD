using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DRFInitializationDraftModel
    {
        public int? DraftID { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public string GenericName { get; set; }
        public string BrandName { get; set; }
        public bool? TreadmarkApprovedInternal { get; set; }
        public string TreadmarkSuggestedInternal { get; set; }
        public string TreadmarkOwnerInternal { get; set; }
        public int? Form { get; set; }
        public string FormulationName { get; set; }
        public int? Strength { get; set; }
        public string StrengthName { get; set; }
        public int? PackSize { get; set; }
        public string PackSizeName { get; set; }
        public int? PackStyle { get; set; }
        public string PackStyleName { get; set; }
        public int? Plant { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CurrencyID { get; set; }        
        public decimal? RegistrationFees { get; set; }
        public int? FeesToBePaidByID { get; set; }
        public string FeesToBePaidBy { get; set; }
        public int? ModeOfFeesPayment { get; set; }
        public string MAHolder { get; set; }
        public int? ProposedMarketingStatusID { get; set; }        
        public string ShippingPort { get; set; }
        public int? ModeOfShipment { get; set; }
        public int? Incoterms { get; set; }
        public string DossierSubmittedToMOHBy { get; set; }
        public bool? AvailabilityofCDA { get; set; }
        public bool? TSExcecuted { get; set; }
        public bool? DAExcecuted { get; set; }
        public decimal? MarketSize { get; set; }
        public string ThreeYearCAGR { get; set; }
        public int NumberOfCurrentPlayer { get; set; }
        public string InnovatorBrand { get; set; }
        public string FirstBrand { get; set; }
        public string SecondBrand { get; set; }
        public string ThirdBrand { get; set; }
        public decimal? ExpectedMarketValueGrowth { get; set; }
        public string InnavotorName { get; set; }
        public string MSFirstBrand { get; set; }
        public string MSSecondBrand { get; set; }
        public string MSThirdBrand { get; set; }
        public string Partner { get; set; }
        public decimal? FirstYearForecastUnitsPacks { get; set; }
        public decimal? SecondYearForecastUnitsPacks { get; set; }
        public decimal? ThirdYearForecastUnitsPacks { get; set; }
        public decimal? FirstYearForecastPriceToPatient { get; set; }
        public decimal? SecondYearForecastPriceToPatient { get; set; }
        public decimal? ThirdYearForecastPriceToPatient { get; set; } 
        public decimal? FirstYearAPIQuantity { get; set; }
        public decimal? SecondYearAPIQuantity { get; set; }
        public decimal? ThirdYearAPIQuantity { get; set; }
        public decimal? FirstYearForecastCIF { get; set; }
        public decimal? SecondYearForecastCIF { get; set; }
        public decimal? ThirdYearForecastCIF { get; set; }
        public decimal? FirstYearForecastValue { get; set; }
        public decimal? SecondYearForecastValue { get; set; }
        public decimal? ThirdYearForecastValue { get; set; }
        public int? OrderFrequencyID { get; set; }        
        public string NameDossierSend { get; set; }
        public string AddressDossierSend { get; set; }
        public string EmailDossierSend { get; set; }
        public string PhoneDossierSend { get; set; }
        public bool? StrategyAlignment { get; set; }
        public string ExceptionExplained { get; set; }
        public bool? IsSamples_Required { get; set; }
        public int? Samples_Required { get; set; }
        public string Remark { get; set; }
        public int? NoofShipmnets { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
