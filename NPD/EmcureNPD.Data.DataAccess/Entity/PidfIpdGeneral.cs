using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdGeneral
    {
        public long PidfIpdGeneralId { get; set; }
        public long Ipdid { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? CountryId { get; set; }
        public string MarketName { get; set; }
        public string DataExclusivity { get; set; }
        public DateTime? MarketExclusivityDate { get; set; }
        public DateTime? ExpectedFilingDate { get; set; }
        public DateTime? ExpectedLaunchDate { get; set; }
        public string ApprovedGenetics { get; set; }
        public string LaunchedGenetics { get; set; }
        public string LegalStatus { get; set; }
        public double? CostOfLitication { get; set; }
        public string Comments { get; set; }
        public bool? IsComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public virtual PidfIpd Ipd { get; set; }
    }
}
