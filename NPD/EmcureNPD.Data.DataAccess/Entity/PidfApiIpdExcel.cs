using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiIpdExcel
    {
        public string PidfApiIpdId { get; set; }
        public string Pidfid { get; set; }
        public string MarketDetailsFileName { get; set; }
        public string ProductTypeId { get; set; }
        public string DrugsCategory { get; set; }
        public string ProductStrength { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string BusinessUnitId { get; set; }
        public string Column11 { get; set; }
        public string Column12 { get; set; }
    }
}
