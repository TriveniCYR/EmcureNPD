using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public class Tbl_DRF_Patent_Details
    {
        public int Id { get; set; }
        public int IPID { get; set; }
        public string PatentNumbers { get; set; }
        public string OriginalExpiryDate { get; set; }
        public string Type { get; set; }
        public string  ExtensionApplication { get; set; }
        public string ExtnExpiryDate { get; set; }
        public string Comment { get; set; }
        public string Strategy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
