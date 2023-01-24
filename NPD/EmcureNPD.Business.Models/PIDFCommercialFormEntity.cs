using EmcureNPD.Resource.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class PIDFCommercialFormEntity
    {
        public long PIDFCommercialID { get; set; }
        public long PIDFID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BusinessUnitName", ResourceType = typeof(Master))]
        public int BusinessUnitId { get; set; }
      
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public List<PIDF_IPD_PatentDetailsEntity> pidf_IPD_PatentDetailsEntities { get; set; }


        public string ProjectName { get; set; }
        public List<MasterBusinessUnitEntity> MasterBusinessUnitEntities { get; set; }
        public List<MasterProductStrengthEntity> MasterStrengthEntities { get; set; }
        public List<RegionEntity> Regionies { get; set; }
        public List<MasterCountryEntity> MasterCountries { get; set; }
        public int? TotalParent { get; set; } = 0;
        public string SaveType { get; set; }
        public int? LogInId { get; set; }
        public int? StatusId { get; set; }
        public int? LastStatusId { get; set; }



        public int MarketSizeinUnitsasLaunch { get; set; }
        public string ShelfLife { get; set; }
        //Add year Properties
        public string ProductPackagingType { get; set; }
        public List<MasterCurrencyEntity> listCurrency { get; set; }
        public string CommercialBatchSize { get; set; }
        public string PriceDiscounting { get; set; }
        public string TotalAPIReq { get; set; }
        public string APIReq { get; set; }
        public string SU_IMS { get; set; }
        public string FreeofCost { get; set; }
        public string MarketGrowth { get; set; }
        public string MarketSize { get; set; }
        public string PriceErosion { get; set; }
        public List<string> FinalSelection { get; set; }

        public PIDFEntity pidfEntity { get; set; }
	}
}