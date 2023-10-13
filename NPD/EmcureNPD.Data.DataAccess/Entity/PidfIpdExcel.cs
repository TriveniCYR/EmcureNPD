using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdExcel
    {
        public string Ipdid { get; set; }
        public string Pidfid { get; set; }
        public string MarketName { get; set; }
        public string DataExclusivity { get; set; }
        public string FillingType { get; set; }
        public string ApprovedGenetics { get; set; }
        public string LaunchedGenetics { get; set; }
        public string Innovators { get; set; }
        public string LegalStatus { get; set; }
        public string CostOfLitication { get; set; }
        public string Comments { get; set; }
        public string IsActive { get; set; }
        public string BusinessUnitId { get; set; }
        public string IsComment { get; set; }
        public string PatentStatus { get; set; }
    }
}
