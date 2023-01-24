using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class APIListEntity
    {
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "OralName", ResourceType = typeof(Master))]
        public List<MasterOralEntity> MasterOrals { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "UnitofMeasurements", ResourceType = typeof(Master))]
        public List<MasterUnitofMeasurementEntity> MasterUnitofMeasurements { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "DosageForms", ResourceType = typeof(Master))]
        public List<MasterDosageFormEntity> MasterDosageForms { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "PackagingTypes", ResourceType = typeof(Master))]
        public List<MasterPackagingTypeEntity> MasterPackagingTypes { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "InLicenses", ResourceType = typeof(Master))]
        public List<MasterBusinessUnitEntity> MasterBusinessUnits { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "Countrys", ResourceType = typeof(Master))]
        public List<MasterCountryEntity> MasterCountrys { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "MarketExtensions", ResourceType = typeof(Master))]
        public List<MarketExtensionEntity> MarketExtensions { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "InHouses", ResourceType = typeof(Master))]
        public List<InHouseEntity> InHouses { get; set; }

        public int PIDFID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "PIDFNO", ResourceType = typeof(Master))]
        public string PIDFNO { get; set; }

        public int OralId { get; set; }
        public int UnitofMeasurementId { get; set; }
        public int DosageFormId { get; set; }
        public int PackagingTypeId { get; set; }

        public int BusinessUnitId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "MoleculeName", ResourceType = typeof(Master))]
        public string MoleculeName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BrandName", ResourceType = typeof(Master))]
        public string BrandName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "ApprovedGenerics", ResourceType = typeof(Master))]
        public string ApprovedGenerics { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "LaunchedGenerics", ResourceType = typeof(Master))]
        public string LaunchedGenerics { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDBrand", ResourceType = typeof(Master))]
        public string RFDBrand { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDApplicant", ResourceType = typeof(Master))]
        public string RFDApplicant { get; set; }

        public int RFDCountryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDIndication", ResourceType = typeof(Master))]
        public string RFDIndication { get; set; }


        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDInnovators", ResourceType = typeof(Master))]
        public string RFDInnovators { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDInitialRevenuePotential", ResourceType = typeof(Master))]
        public string RFDInitialRevenuePotential { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDPriceDiscounting", ResourceType = typeof(Master))]
        public string RFDPriceDiscounting { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "RFDCommercialBatchSize", ResourceType = typeof(Master))]
        public string RFDCommercialBatchSize { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int StatusId { get; set; }

        public int LastStatusId { get; set; }
        public List<PIDFEntity> PIDFEntities { get; set; }

    }
}
