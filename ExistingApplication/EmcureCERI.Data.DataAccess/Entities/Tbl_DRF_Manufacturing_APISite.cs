using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_Manufacturing_APISite
    {
        [Key]
        public int? MAPIID { get; set; }
        public int ManufacturingSiteId { get; set; }
        public int APIId { get; set; }
        public string APISiteName { get; set; }
        public string APISite { get; set; }
        public string APIName { get; set; }
        public byte IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}