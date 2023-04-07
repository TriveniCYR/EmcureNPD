﻿using EmcureNPD.Resource.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class IPDEntity
    {
        public long IPDID { get; set; }
        public long PIDFID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "BusinessUnitName", ResourceType = typeof(Master))]
        public int BusinessUnitId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "Market Name")]
        public string MarketName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string DataExclusivity { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string FillingType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string ApprovedGenetics { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string LaunchedGenetics { get; set; }

        [Display(Name = "Region")]
        public string RegionId { get; set; }
        public string BusinessUnitsByUser { get; set; }
        public int SelectedTabBusinessUnit { get; set; }
        public bool _Partial { get; set; }
        public bool IsViewMode { get; set; }
        public string RegionIds { get; set; }
        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public string CountryIds { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string Innovators { get; set; }
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string PatentStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string LegalStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public int CostOfLitication { get; set; }
        public bool IsComment { get; set; } = true;
        public string Comments { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public List<PIDF_IPD_PatentDetailsEntity> pidf_IPD_PatentDetailsEntities { get; set; }


        public string ProjectName { get; set; }
        public List<MasterBusinessUnitEntity> MasterBusinessUnitEntities { get; set; }
        public List<RegionEntity> Regionies { get; set; }
        public List<MasterCountryEntity> MasterCountries { get; set; }
        public int? TotalParent { get; set; } = 0;
        public string SaveType { get; set; }
        public int? LogInId { get; set; }
        public int? StatusId { get; set; }
        //public int? LastStatusId { get; set; }
    }
    public partial class PIDF_IPD_PatentDetailsEntity
    {
        public long? PatentDetailsID { get; set; }
        public long? IPDID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessage = "Original Expiry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? OriginalExpiryDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessage = "Extension Expiry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExtensionExpiryDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string Comments { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string Strategy { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Required")]
        public string PatentNumber { get; set; }
        
    }
    public partial class IPDPIDFListEntity
    {
        public int PIDFID { get; set; }
        public int BusinessUnitId { get; set; }
        public string PIDFNO { get; set; }
        public string DosageFormName { get; set; }
        public string PackagingTypeName { get; set; }
        public string MoleculeName { get; set; }
        public string BrandName { get; set; }
        public string ApprovedGenerics { get; set; }
        public string LaunchedGenerics { get; set; }
        public string MarketExtension { get; set; }
        public string Applicant { get; set; }
        public string Inidication { get; set; }
        public string DiaName { get; set; }
        public string TransformFormRandDDivision { get; set; }
        public string PreviousProjectCode { get; set; }
        public string SinkCost { get; set; }
        public string CreatedBy { get; set; }
        public string encpidfid { get; set; }
        public string encbud { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }

    }
    public class RegionEntity
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
    public class UserRegionMappingEntity
    {
        public int UserRegionId { get; set; }
        public int RegionId { get; set; }
        public int UserId { get; set; }

    }

    public partial class EntryApproveRej
    {
        public string SaveType { get; set; }
        public string ScreenId { get; set; }
        public string Comment { get; set; }
        public List<ApprRejPidf> PidfIds { get; set; }
    }
    public partial class ApprRejPidf
    {
        public long pidfId { get; set; }
    }

}