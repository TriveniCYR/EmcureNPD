using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Data.DataAccess.Entity {
    public class MasterNotification {
        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
