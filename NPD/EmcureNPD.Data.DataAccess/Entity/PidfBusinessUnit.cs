using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfBusinessUnit
    {
        public long PidfbusinessUnitId { get; set; }
        public int Pidfid { get; set; }
        public int? OralId { get; set; }
        public int? UnitofMeasurementId { get; set; }
        public int? DosageFormId { get; set; }
        public int? PackagingTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string BrandName { get; set; }
        public string ApprovedGenerics { get; set; }
        public string LaunchedGenerics { get; set; }
        public string Rfdbrand { get; set; }
        public string Rfdapplicant { get; set; }
        public int? RfdcountryId { get; set; }
        public string Rfdindication { get; set; }
        public string Rfdinnovators { get; set; }
        public string RfdinitialRevenuePotential { get; set; }
        public string RfdpriceDiscounting { get; set; }
        public string RfdcommercialBatchSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public int? MarketExtenstionId { get; set; }
        public int? Diaid { get; set; }
        public bool? TradeNameRequired { get; set; }
        public DateTime? TradeNameDate { get; set; }
        public int? IndicationId { get; set; }
    }
}
