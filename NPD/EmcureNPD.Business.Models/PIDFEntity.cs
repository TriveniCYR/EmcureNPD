using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class PIDFEntity
    {
        public long PIDFID { get; set; }        
        public string PIDFNO { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[Display(Name = "Oral", ResourceType = typeof(Master))]
        public int? OralId { get; set; }
        public int? UnitofMeasurementId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[Display(Name = "DosageForm", ResourceType = typeof(Master))]
        public int? DosageFormId { get; set; }

        public int? PackagingTypeId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[Display(Name = "BusinessUnit", ResourceType = typeof(Master))]
        public int? BusinessUnitId { get; set; }
        public int? TabBusinessUnitId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "MoleculeName", ResourceType = typeof(Master))]
        public string MoleculeName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BrandName", ResourceType = typeof(Master))]
        public string BrandName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "ApprovedGenerics", ResourceType = typeof(Master))]
        public string ApprovedGenerics { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "LaunchedGenerics", ResourceType = typeof(Master))]
        public string LaunchedGenerics { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDBrand", ResourceType = typeof(Master))]
        public string RFDBrand { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDApplicant", ResourceType = typeof(Master))]
        public string RFDApplicant { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[Display(Name = "RFDCountry", ResourceType = typeof(Master))]
        public int? RFDCountryId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDIndication", ResourceType = typeof(Master))]
        public string RFDIndication { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDInnovators", ResourceType = typeof(Master))]
        public string RFDInnovators { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDInitialRevenuePotential", ResourceType = typeof(Master))]
        public string RFDInitialRevenuePotential { get; set; }

        //[Required(ErrorMessage = "This field required with value within 0-100")]
        [Display(Name = "RFDPriceDiscounting", ResourceType = typeof(Master))]
        public string RFDPriceDiscounting { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDCommercialBatchSize", ResourceType = typeof(Master))]
        public string RFDCommercialBatchSize { get; set; }

        [Display(Name = "Trade Name Required")]
        public bool TradeNameRequired { get; set; }
        public DateTime? TradeNameDate { get; set; }
        public bool IsActive { get; set; }
        //      public DateTime CreatedDate { get; set; }
        //      public int? CreatedBy { get; set; }
        //      public DateTime? ModifyDate { get; set; }
        //public int? ModifyBy { get; set; }

        public string BussinessUnitByUserIDPIDF { get; set; }
        public int StatusId { get; set; }
        
        public int? Diaid { get; set; }
        
        public bool IsAllowToApprove { get; set; }
        public int LastStatusId { get; set; }
		public bool InHouses { get; set; }
        public bool IsPIDFApproved { get; set; }
        public bool _Partial { get; set; }
        public bool IsViewMode { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        //[Display(Name = "MarketExtension", ResourceType = typeof(Master))]
        public int? MarketExtenstionId { get; set; }

        public string BusinessUnitName { get; set; }
        public string DosageFormName { get; set; }
        public bool IsAllBUFilled { get; set; }
        public string SaveType { get; set; }
		public int? LogInId { get; set; }
		public List<PIDFEntity> PIDFEntities { get; set; }
        public List<PidfApiDetailEntity> pidfApiDetailEntities { get; set; }
        public List<PidfProductStregthEntity> pidfProductStregthEntities { get; set; }
        public List<IMSDataEntity> IMSDataEntities { get; set; }

        public PIDFInterested pidfInterested { get; set; }
        public int[] InterestedCountries { get; set; }

        public int SelectedBusinessUnitId { get; set; }

        public string PIDFIsInterested { get; set; }
    }

    public class PIDFInterested
    {
        public bool IsInterested { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
    }

}
