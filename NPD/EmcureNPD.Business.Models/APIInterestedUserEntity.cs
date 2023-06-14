using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class APIInterestedUserEntity
    {
        //public List<AuditLogEntity> masterAuditLogEntities { get; set; }
        public string PIDFID { get; set; }
        public bool IsAPIIntrested { get; set; }
        public string ApiRemark { get; set; }
        public int AssignedAPIUser { get; set; }
    }
}
