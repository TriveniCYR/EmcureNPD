using EmcureNPD.Resource.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class PIDFCommercialEntity
    {
        public long PidfcommercialId { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public long PidfproductStrengthId { get; set; }

        public int IsView { get; set; }
        public string MarketSizeInUnit { get; set; }
        
        public string ShelfLife { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public ICollection<PidfCommercialYearEntity> PidfCommercialYears { get; set; }
        /// <summary>
        /// //Newly added Entity
        /// </summary>
        public string encCreatedBy { get; set; }
        public string encPidfid { get; set; }
        public string SaveType { get; set; }
        public int? StatusId { get; set; }
        public List<MasterBusinessUnitEntity> MasterBusinessUnitEntities { get; set; }
        public List<PidfProductStregthEntity> MasterStrengthEntities { get; set; }
        public PidfCommercialYearEntity PidfCommercialYear { get; set; }
        public PIDFEntity pidfEntity { get; set; }
        public IPDEntity IPDFormEntity { get; set; }
        public string BusinessUnitsByUser { get; set; }
        //public virtual MasterBusinessUnit BusinessUnit { get; set; }
        //public virtual Pidf Pidf { get; set; }
        //public virtual PidfproductStrength PidfproductStrength { get; set; }\

        //Year Entities

        public int CommercialPackagingTypeId { get; set; }
        public int CommercialPackSizeId { get; set; }
        public int CurrencyId { get; set; }
        public string CommercialBatchSize { get; set; }
        public string PriceDiscounting { get; set; }
        public string TotalApireq { get; set; }
        public string Apireq { get; set; }
        public string Suimsvolume { get; set; }
        public string FreeOfCost { get; set; }
        public string MarketGrowth { get; set; }
        public string MarketSize { get; set; }
        public string PriceErosion { get; set; }
        public int? FinalSelectionId { get; set; }
        public string MarketSharePercentageLow { get; set; }
        public string MarketSharePercentageMedium { get; set; }
        public string MarketSharePercentageHigh { get; set; }
        [ReadOnly(true)]
        public string MarketShareUnitLow { get; set; }
        [ReadOnly(true)]
        public string MarketShareUnitMedium { get; set; }
        [ReadOnly(true)]
        public string MarketShareUnitHigh { get; set; }
        public string NspunitsLow { get; set; }
        public string NspunitsMedium { get; set; }
        public string NspunitsHigh { get; set; }
        [ReadOnly(true)]
        public string NspLow { get; set; }
        [ReadOnly(true)]
        public string NspMedium { get; set; }
        [ReadOnly(true)]
        public string NspHigh { get; set; }

        public bool _Partial { get; set; }
    }
}