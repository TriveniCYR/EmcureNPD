using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterAuditLog
    {
        public int AuditLogId { get; set; }
        public int? ModuleId { get; set; }
        public int? EntityId { get; set; }
        public string ActionType { get; set; }
        public string Log { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
