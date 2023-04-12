using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class AuditLogEntity
    {
        //public List<AuditLogEntity> masterAuditLogEntities { get; set; }
        public int AuditLogId { get; set; }
        public string ModuleName { get; set; }
        public string ActionType { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string log { get; set; }
    }
}
