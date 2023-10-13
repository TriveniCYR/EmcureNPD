using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfIpdPatentDetailsExcel
    {
        public string PatentDetailsId { get; set; }
        public string Ipdid { get; set; }
        public string Type { get; set; }
        public string OriginalExpiryDate { get; set; }
        public string ExtensionExpiryDate { get; set; }
        public string Comments { get; set; }
        public string Strategy { get; set; }
        public string PatentNumber { get; set; }
        public string PatentType { get; set; }
        public string BasicPatentExpiry { get; set; }
        public string OtherLmitingPatentDate1 { get; set; }
        public string OtherLmitingPatentDate2 { get; set; }
        public string EarliestLaunchDate { get; set; }
        public string AnyPatentstobeFiled { get; set; }
        public string EarliestMarketEntry { get; set; }
        public string StimatedNumberofgenericsinthe { get; set; }
        public string Lawfirmbeingused { get; set; }
        public string CountryId { get; set; }
        public string PatentStrategy { get; set; }
        public string PatentStrategyOther { get; set; }
        public string PidfIpdGeneralId { get; set; }
        public string Column21 { get; set; }
        public string Column22 { get; set; }
    }
}
