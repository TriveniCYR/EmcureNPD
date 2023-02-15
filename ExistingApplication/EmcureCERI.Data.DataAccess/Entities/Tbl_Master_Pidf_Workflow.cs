using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_Pidf_Workflow
    {
        public int Id { get; set; }
        public string WorkflowName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
