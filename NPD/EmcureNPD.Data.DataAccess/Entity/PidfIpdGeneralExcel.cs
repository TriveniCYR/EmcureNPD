using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdGeneralExcel
    {
        public string PidfIpdGeneralId { get; set; }
        public string Ipdid { get; set; }
        public string BusinessUnitId { get; set; }
        public string CountryId { get; set; }
        public string MarketName { get; set; }
        public string DataExclusivity { get; set; }
        public string MarketExclusivityDate { get; set; }
        public string ExpectedFilingDate { get; set; }
        public string ExpectedLaunchDate { get; set; }
        public string ApprovedGenetics { get; set; }
        public string LaunchedGenetics { get; set; }
        public string LegalStatus { get; set; }
        public string CostOfLitication { get; set; }
        public string Comments { get; set; }
        public string IsComment { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string Column19 { get; set; }
        public string Column20 { get; set; }
    }
}
