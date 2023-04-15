using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterException
    {
        public long ExceptionId { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string InnerException { get; set; }
        public string StrackTrace { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
