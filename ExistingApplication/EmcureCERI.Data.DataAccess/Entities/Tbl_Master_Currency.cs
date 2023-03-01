using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_Currency
    {
        [Key]
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
