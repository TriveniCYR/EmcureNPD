using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public class Tbl_Master_GMPAvailability
    {
        [Key]
        public int GMPAvailabilityID { get; set; }
        public string GMPAvailability { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
