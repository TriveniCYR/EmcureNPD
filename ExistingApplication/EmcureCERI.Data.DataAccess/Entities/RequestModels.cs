using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class GetPIDFApprovalListRequestModels
    {
        public Nullable<int> PidfID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> StatusId { get; set; }
    }

    public class UpdateApprovalPIDFStatusRequestModels
    {
        public Nullable<int> pidfID { get; set; }
        public string pIDFNo { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> pidfStatusID { get; set; }
        public string approvalRemark { get; set; }
    }
}
