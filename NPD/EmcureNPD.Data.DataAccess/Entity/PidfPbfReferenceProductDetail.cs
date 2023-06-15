using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfReferenceProductDetail
    {
        public long PidfpbfreferenceProductdetailId { get; set; }
        public long Pidfid { get; set; }
        public long BusinessUnitId { get; set; }
        public string Rfdbrand { get; set; }
        public string Rfdapplicant { get; set; }
        public int? RfdcountryId { get; set; }
        public string Rfdindication { get; set; }
        public string Rfdinnovators { get; set; }
        public string RfdinitialRevenuePotential { get; set; }
        public string RfdpriceDiscounting { get; set; }
        public string RfdcommercialBatchSize { get; set; }
    }
}
