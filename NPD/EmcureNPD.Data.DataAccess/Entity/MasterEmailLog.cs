using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterEmailLog
    {
        public long EmailLogId { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public bool SentSuccessfully { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
