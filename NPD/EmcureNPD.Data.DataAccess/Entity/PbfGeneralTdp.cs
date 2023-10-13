using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PbfGeneralTdp
    {
        public long TradeDressProposalId { get; set; }
        public string Approch { get; set; }
        public long? Pidfid { get; set; }
        public long? PbfId { get; set; }
        public long? PidfpbfGeneralId { get; set; }
        public long? PidfproductStrngthId { get; set; }
        public string Description { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Engraving { get; set; }
        public string Packaging { get; set; }
        public bool? IsPrimaryPackaging { get; set; }
        public bool? IsSecondryPackaging { get; set; }
        public string ShelfLife { get; set; }
        public string StorageHandling { get; set; }
        public bool? IsEmcure { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string FormulaterResponsiblePerson { get; set; }
        public string PrimaryPackaging { get; set; }
        public string SecondryPackaging { get; set; }
    }
}
