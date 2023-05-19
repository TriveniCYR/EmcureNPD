using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiRnD
    {
        public long PidfApiRnDId { get; set; }
        public long Pidfid { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public string ApimarketPrice { get; set; }
        public string ApitargetRmcCcpc { get; set; }
        public int? MarketExtenstionId { get; set; }
        public string Development { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string PlantQc { get; set; }
        public string Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
    }
}
