using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfApiCharter
    {
        public int PidfApiCharterId { get; set; }
        public int Pidfid { get; set; }
        public string ApigroupLeader { get; set; }
        public int ProjectComplexityId { get; set; }
        public int ManHourRates { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
