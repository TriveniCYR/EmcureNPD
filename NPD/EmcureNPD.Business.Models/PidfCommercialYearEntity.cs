using EmcureNPD.Resource.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmcureNPD.Business.Models
{
    public partial class PidfCommercialYearEntity
    {
        public long PidfcommercialYearId { get; set; }
        public long PidfcommercialId { get; set; }
        public int YearIndex { get; set; }
        public int PackagingTypeId { get; set; }
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
        public string MarketShareUnitLow { get; set; }
        public string MarketShareUnitMedium { get; set; }
        public string MarketShareUnitHigh { get; set; }
        public string NspunitsLow { get; set; }
        public string NspunitsMedium { get; set; }
        public string NspunitsHigh { get; set; }
        public string NspLow { get; set; }
        public string NspMedium { get; set; }
        public string NspHigh { get; set; }

        public int BusinessUnitId { get; set; }
        public int packSizeId { get; set; }
        public long pidfProductStrengthId { get; set; }
        public string BrandPrice { get; set; }
        public string GenericPrice { get; set; }
    }
}