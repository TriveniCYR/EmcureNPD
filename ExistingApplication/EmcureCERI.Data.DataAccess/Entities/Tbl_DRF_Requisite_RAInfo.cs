using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_Requisite_RAInfo
    {
        public int Id { get; set; }
        public bool ACC { get; set; }
        public bool ZoneII { get; set; }
        public bool Ivbdata { get; set; }
        public bool ProtocolAvailability { get; set; }
        public bool COPPAvailability { get; set; }
        public int GMPAvailabilityId { get; set; }
        public string GMPAvailability { get; set; }
        public bool MfgLicense { get; set; }
        public bool PlantInspection { get; set; }
        public int ValidationBatches { get; set; }
        public bool COAAvailability { get; set; }
        public bool BEAvailability { get; set; }
        public bool APIDMFstatus { get; set; }
        public bool PlantApproval { get; set; }
        public string PlantApprovalIfYes { get; set; }
        public string RegistrationValidity { get; set; }
        public string Timefordossierpreparation { get; set; }
        public string AMV { get; set; }
        public bool PDR { get; set; }
        public bool SamplesAvailability { get; set; }
        public bool ImportPermit { get; set; }
        public string BrandNameApproval { get; set; }
        public bool AvailabilityofCDA { get; set; }
        public decimal ProductRegistrationFee { get; set; }
        public string ComparativeDissolutionProfileData { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InitializationId { get; set; }
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public decimal ConsultantCost { get; set; }
        public decimal LegalizationCost { get; set; }
        public decimal TranslationCost { get; set; }
        public decimal OtherCost { get; set; }

    }
}
