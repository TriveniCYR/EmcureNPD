using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_Medical
    {
        public int Id { get; set; }
        public bool BeCtVitroAvailable { get; set; }
        public bool BioWaiver { get; set; }
        public bool CTWaiver { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InitializationId { get; set; }
        public decimal BECost { get; set; }
        public decimal BioCost { get; set; }
        public decimal CTCost { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
    }
}
