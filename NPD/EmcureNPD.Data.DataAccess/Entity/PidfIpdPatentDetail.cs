using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdPatentDetail
    {
        public long PatentDetailsId { get; set; }
        public long Ipdid { get; set; }
        public string Type { get; set; }
        public DateTime? OriginalExpiryDate { get; set; }
        public DateTime? ExtensionExpiryDate { get; set; }
        public string Comments { get; set; }
        public string Strategy { get; set; }
        public string PatentNumber { get; set; }
        public short? PatentType { get; set; }
        public DateTime? BasicPatentExpiry { get; set; }
        public DateTime? OtherLmitingPatentDate1 { get; set; }
        public DateTime? OtherLmitingPatentDate2 { get; set; }
        public DateTime? EarliestLaunchDate { get; set; }
        public bool? AnyPatentstobeFiled { get; set; }
        public DateTime? EarliestMarketEntry { get; set; }
        public string StimatedNumberofgenericsinthe { get; set; }
        public string Lawfirmbeingused { get; set; }
        public int? CountryId { get; set; }
        public int? PatentStrategy { get; set; }
        public string PatentStrategyOther { get; set; }

        public virtual PidfIpd Ipd { get; set; }
    }
}
