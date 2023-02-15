using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Pidf_Strength
    {
        public Int64 Id { get; set; }
        public Int64 PidfID { get; set; }
        public string PidfNo { get; set; }
        public string PidfStrength { get; set; }
        public int UnitID { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
