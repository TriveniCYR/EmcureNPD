using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterApiCharterAnalyticalDepartment
    {
        public int MasterApiCharterAnalyticalDepartmentId { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public string Ard { get; set; }
        public string Impurity { get; set; }
        public string Stability { get; set; }
        public string Amv { get; set; }
        public string Amt { get; set; }
    }
}
