using System;
using System.Collections.Generic;

namespace EmcureNPD.Business.Models
{
    public class MasterAuditLogEntity
    {
        public int AuditLogId { get; set; }
        public int ModuleId { get; set; }
        public int EntityId { get; set; }
        public string ActionType { get; set; }
        public string Log { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class MasterAuditLogWrapperEntity<TLog>
    {
        public int AuditLogId { get; set; }
        public int? ModuleId { get; set; }

        public int? EntityId { get; set; }
        public string ActionType { get; set; }
        public IEnumerable<TLog> Log { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

}
