using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_DRF_FinalApprovelDetails
    {
        public int Id { get; set; }
        public bool ApprovedReject { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int InitializationID { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
}
