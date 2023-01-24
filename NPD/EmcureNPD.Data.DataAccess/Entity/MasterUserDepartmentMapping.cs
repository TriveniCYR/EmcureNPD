using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterUserDepartmentMapping
    {
        public int UserDepartmentId { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual MasterDepartment Department { get; set; }
        public virtual MasterUser User { get; set; }
    }
}
